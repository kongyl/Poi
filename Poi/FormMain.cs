using OSGeo.OGR;
using Poi.Baidu;
using Poi.Dao;
using Poi.Model;
using Poi.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Poi
{
    public partial class FormMain : Form
    {
        private PlaceAPIHandler handler;
        private City city = null; // 用于分块的城市

        // 构造函数
        public FormMain()
        {
            InitializeComponent();

            // 获取省份列表
            List<Region> regionList = RegionDao.SelectByParentId(0);
            foreach (Region region in regionList)
            {
                comboBoxProvince.Items.Add(region);
            }
            comboBoxProvince.SelectedIndex = 0;

            // 获取一级行业分类
            List<KeyWord> keyWordList = KeyWordDao.SelectByParentId(0);
            foreach(KeyWord keyWord in keyWordList)
            {
                comboBoxKey1.Items.Add(keyWord);
            }
            comboBoxKey1.SelectedIndex = 0;

            // 初始化分块
            initComboBoxBlock();

            // 初始化GDAL
            Ogr.RegisterAll();
        }

        // 初始化分块的ComboBox
        private void initComboBoxBlock()
        {
            comboBoxBlockCol.Items.Clear();
            comboBoxBlockRow.Items.Clear();
            for (int i = 1; i <= 20; i++)
            {
                comboBoxBlockCol.Items.Add(i);
                comboBoxBlockRow.Items.Add(i);
            }
            comboBoxBlockCol.SelectedIndex = 0;
            comboBoxBlockRow.SelectedIndex = 0;
        }

        // 获取关键字
        private string getKeyWord()
        {
            string keyWord = null;
            if (checkBoxCustom.Checked)
            {
                keyWord = textBoxKeyWord.Text;
            }
            else
            {
                if (checkBoxKey2.Checked)
                {
                    keyWord = (comboBoxKey2.SelectedItem as KeyWord).Name;
                }
                else
                {
                    keyWord = (comboBoxKey1.SelectedItem as KeyWord).Name;
                }
            }
            return keyWord;
        }

        // 按 region 请求 POI
        private int requestRegionPoi()
        {
            int pageNum = handler.GetLimitedPageNum();
            for (int i = 0; i < pageNum; i++)
            {
                int res = handler.GetRegionPageData(i);
                if (res < 0) // 返回错误
                {
                    return res;
                }
            }
            return 0; // 完成所有请求
        }

        // 分块请求 POI
        private int requestBlockPoi(int rowNum, int colNum)
        {
            // 获取对应矢量
            int fid = Convert.ToInt32(city.Id);
            Geometry geometry = GisUtil.GetGeometry(fid);

            // 获取间隔
            Envelope envelope = new Envelope();
            geometry.GetEnvelope(envelope);
            double xInterval = (envelope.MaxX - envelope.MinX) / colNum;
            double yInterval = (envelope.MaxY - envelope.MinY) / rowNum;

            // 逐分块请求
            double minX = envelope.MinX;
            double minY = envelope.MinY;
            double maxX;
            double maxY;
            for (int i = 0; i < rowNum; i++)
            {
                maxY = minY + yInterval;
                for (int j = 0; j < colNum; j++)
                {
                    maxX = minX + xInterval;

                    int subType = GisUtil.GetEnvelopeRelationship(geometry, minX, maxX, minY, maxY);
                    if (subType > 0)
                    {
                        string bounds = string.Format("{0},{1},{2},{3}", minY, minX, maxY, maxX);
                        //if (subType > 1) // geometry 包含整个子块
                        //{
                        int page = 0;
                        int stopFlag = 0;
                        while (stopFlag == 0)
                        {
                            stopFlag = handler.GetBoundsPageData(bounds, page);
                            page++;
                        }
                        if (stopFlag < 0) // 返回错误
                        {
                            return stopFlag;
                        }
                        //}
                    }

                    minX = maxX;
                }
                minY = maxY;
            }

            return 0;
        }

        // 请求结果
        private void requestData(int rowNum, int colNum)
        {
            string msg = "请求结束";
            int res = 0;

            // 开始请求
            toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI", handler.GetNum,
                    handler.TotalNum);
            toolStripProgressBar1.Visible = true;

            if (rowNum * colNum > 1) // 分块获取数据   
            {
                res = requestBlockPoi(rowNum, colNum); 
            }
            else // 普通获取
            {
                res = requestRegionPoi();
            }
            if (res < 0) // 返回错误
            {
                msg = PlaceAPIHandler.GetErrMsg(res);
                MessageBox.Show(msg);
            }

            toolStripStatusLabelStatus.Text = "就绪";
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI —— {2}", handler.GetNum,
                    handler.TotalNum, msg);
        }

        // 点击开始按钮
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // 初始化
            dataGridView1.Rows.Clear();
            toolStripStatusLabelResult.Text = "";
            // 关键字
            string keyWord = getKeyWord();
            if (string.IsNullOrEmpty(keyWord))
            {
                MessageBox.Show("请输入自定义关键字");
                return;
            }
            // excel导出路径
            if (checkBoxExport.Checked)
            {
                if (string.IsNullOrEmpty(textBoxSave.Text.Trim()))
                {
                    MessageBox.Show("请选择数据导出路径");
                    return;
                }
            }            

            // 开始请求
            toolStripStatusLabelStatus.Text = "请求开始……";

            // 获取总数
            handler = new PlaceAPIHandler(comboBoxCity.SelectedItem as Region, keyWord, dataGridView1);
            int totalNum = handler.GetTotalNum((comboBoxProvince.SelectedItem as Region).Name);
            if (totalNum < 0) // 返回错误
            {
                string errMsg = PlaceAPIHandler.GetErrMsg(totalNum);
                MessageBox.Show(errMsg);
                toolStripStatusLabelStatus.Text = "就绪";
                toolStripStatusLabelResult.Text = errMsg;
                return;
            }
            else if (totalNum == 0)
            {
                string msg = "没有搜索结果";
                MessageBox.Show(msg);
                toolStripStatusLabelStatus.Text = "就绪";
                toolStripStatusLabelResult.Text = msg;
                return;
            }
            else // 有结果，开始请求
            {
                int rowNum = 1;
                int colNum = 1;
                if (totalNum > 400 && checkBoxBlock.Checked) // 分块获取
                {
                    rowNum = Convert.ToInt32(comboBoxBlockRow.SelectedItem);
                    colNum = Convert.ToInt32(comboBoxBlockCol.SelectedItem);

                    if (rowNum * colNum > 150) // 可能超过配额
                    {
                        if (MessageBox.Show("请求可能超过配额，是否继续？", "", MessageBoxButtons.OKCancel)
                                == DialogResult.Cancel) // 取消请求
                        {
                            toolStripStatusLabelStatus.Text = "就绪";
                            toolStripProgressBar1.Visible = false;
                            return;
                        }
                    }
                }

                // 请求
                requestData(rowNum, colNum);
            }
        }

        // 点击取消按钮
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 改变省下拉框
        private void comboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCity.Items.Clear();
            Region province = comboBoxProvince.SelectedItem as Region;
            int parentId = Convert.ToInt32(province.Id);

            List<Region> regionList = RegionDao.SelectByParentId(parentId);
            if (regionList.Count == 0)
            {
                comboBoxCity.Items.Add(province);
            }
            else
            {
                foreach (Region region in regionList)
                {
                    comboBoxCity.Items.Add(region);
                }
            }

            comboBoxCity.SelectedIndex = 0;

            // 关闭分块
            checkBoxBlock.Checked = false; 
            initComboBoxBlock();
            city = null;
        }

        // 改变市下拉框
        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 关闭分块
            checkBoxBlock.Checked = false;
            initComboBoxBlock();
            city = null;
        }

        // 改变一级分类
        private void comboBoxKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxKey2.Items.Clear();
            KeyWord keyWord1 = comboBoxKey1.SelectedItem as KeyWord;
            int parentId = Convert.ToInt32(keyWord1.Id);

            List<KeyWord> keyWordList = KeyWordDao.SelectByParentId(parentId);
            if (keyWordList.Count == 0)
            {
                comboBoxKey2.Items.Add(keyWord1);
            }
            else
            {
                foreach (KeyWord keyWord2 in keyWordList)
                {
                    comboBoxKey2.Items.Add(keyWord2);
                }
            }

            comboBoxKey2.SelectedIndex = 0;
        }

        // 自定义关键字选框
        private void checkBoxCustom_CheckedChanged(object sender, EventArgs e)
        {
            bool isCheck = checkBoxCustom.Checked;

            labelKey1.Enabled = !isCheck;            
            comboBoxKey1.Enabled = !isCheck;
            checkBoxKey2.Enabled = !isCheck;
            comboBoxKey2.Enabled = checkBoxKey2.Checked && !isCheck;

            textBoxKeyWord.Enabled = isCheck;
        }

        // excel 路径浏览按钮
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSave.Text = saveFileDialog1.FileName;
            }
        }

        // 二级分类选框
        private void checkBoxKey2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxKey2.Enabled = checkBoxKey2.Checked;
        }

        // 点击查看总数按钮
        private void buttonTotal_Click(object sender, EventArgs e)
        {
            // 关键字
            string keyWord = getKeyWord();
            if (string.IsNullOrEmpty(keyWord))
            {
                MessageBox.Show("请输入自定义关键字");
                return;
            }

            // 开始请求
            Region region = comboBoxCity.SelectedItem as Region;
            handler = new PlaceAPIHandler(region, keyWord, null);
            int totalNum = handler.GetTotalNum((comboBoxProvince.SelectedItem as Region).Name);
            if (totalNum < 0) // 返回错误
            {
                MessageBox.Show(PlaceAPIHandler.GetErrMsg(totalNum));
            }
            else // 有结果
            {
                MessageBox.Show(string.Format("区域：{0}，关键词：{1}，共有 {2} 个结果", region.Name, keyWord, totalNum));
            }
        }

        // 导出 excel 选框
        private void checkBoxExport_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBoxExport.Checked;
            textBoxSave.Enabled = check;
            buttonSave.Enabled = check;
        }

        // DataGredView 的 CurrentCell 发生改变
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI", handler.GetNum, handler.TotalNum);
            Refresh();
        }

        // 分块获取选框
        private void checkBoxBlock_CheckedChanged(object sender, EventArgs e)
        {
            bool isBlock = checkBoxBlock.Checked;
            buttonBlock.Enabled = isBlock;
            panelBlock.Enabled = isBlock;

            if (isBlock) // 选取
            {
                // 检查是否有对应矢量
                string cityName = comboBoxCity.SelectedItem.ToString();
                City city = CityDao.SelectByName(cityName);
                if (city == null)
                {
                    MessageBox.Show("此城市没有相应的矢量数据，无法分块获取POI");
                    checkBoxBlock.Checked = false;
                }
            }
        }

        // 点击分块建议按钮
        private void buttonBlock_Click(object sender, EventArgs e)
        {
            // 关键字
            string keyWord = getKeyWord();
            if (string.IsNullOrEmpty(keyWord))
            {
                MessageBox.Show("请输入自定义关键字");
                return;
            }

            // 获取对应矢量
            string cityName = comboBoxCity.SelectedItem.ToString();
            City city = CityDao.SelectByName(cityName);
            if (city == null)
            {
                MessageBox.Show("此城市没有相应的矢量数据，无法分块获取POI");
                return;
            }
            int fid = Convert.ToInt32(city.Id);

            // 开始请求
            Region region = comboBoxCity.SelectedItem as Region;
            handler = new PlaceAPIHandler(region, keyWord, null);
            int totalNum = handler.GetTotalNum((comboBoxProvince.SelectedItem as Region).Name);
            if (totalNum < 0) // 返回错误
            {
                MessageBox.Show(PlaceAPIHandler.GetErrMsg(totalNum));
            }
            else if (totalNum <= 400)
            {
                MessageBox.Show(string.Format("区域：{0}，关键词：{1}，共有 {2} 个结果，无需分块", region.Name, keyWord,
                        totalNum));
                checkBoxBlock.Checked = false;
            }
            else // 有结果
            {
                int blockNum = totalNum / 400 + 1; // 最少分块数

                Envelope envelope = GisUtil.GetEnvelope(fid);
                double x = envelope.MaxX - envelope.MinX;
                double y = envelope.MaxY - envelope.MinY;
                int xNum = 1;
                int yNum = 1;
                if (x <= y)
                {
                    double r = y / x;
                    xNum = Convert.ToInt32(Math.Sqrt(blockNum / r)) + 1;
                    yNum = Convert.ToInt32(xNum * r) + 1;
                }
                else
                {
                    double r = x / y;
                    yNum = Convert.ToInt32(Math.Sqrt(blockNum / r)) + 1;
                    xNum = Convert.ToInt32(yNum * r) + 1;
                }

                MessageBox.Show(string.Format("区域：{0}，关键词：{1}，共有 {2} 个结果。建议分块为 {3} 行，{4} 列",
                        region.Name, keyWord, totalNum, yNum, xNum));

                // 自动调整行列分块数
                if (xNum > 20)
                {
                    comboBoxBlockCol.Items.Clear();
                    for (int i = 1; i <= xNum + 10; i++)
                    {
                        comboBoxBlockCol.Items.Add(i);
                    }
                }
                comboBoxBlockCol.SelectedIndex = xNum - 1;
                if (yNum > 20)
                {
                    comboBoxBlockRow.Items.Clear();
                    for (int j = 1; j <= yNum + 10; j++)
                    {
                        comboBoxBlockRow.Items.Add(j);
                    }
                }
                comboBoxBlockRow.SelectedIndex = yNum - 1;
            }
        }

        // 点击配置密钥按钮
        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey formKey = new FormKey();
            formKey.ShowDialog();
        }
    }
}
