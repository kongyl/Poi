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

        // 开始抓取数据
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // 初始化
            dataGridView1.Rows.Clear();
            toolStripStatusLabelResult.Text = "";
            // 关键字
            string keyWord = null;
            if (checkBoxCustom.Checked)
            {
                keyWord = textBoxKeyWord.Text;
                if (string.IsNullOrEmpty(keyWord))
                {
                    MessageBox.Show("请输入自定义关键字");
                    return;
                }
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

            handler = new PlaceAPIHandler(comboBoxCity.SelectedItem as Region, keyWord, dataGridView1);
            int totalNum = handler.GetTotalNum((comboBoxProvince.SelectedItem as Region).Name);
            if (totalNum < 0) // 返回错误
            {
                string errMsg = PlaceAPIHandler.GetErrMsg(totalNum);
                MessageBox.Show(errMsg);
                toolStripStatusLabelStatus.Text = "就绪";
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabelResult.Text = errMsg;
                return;
            }
            else if (totalNum == 0)
            {
                string msg = "没有搜索结果";
                MessageBox.Show(msg);
                toolStripStatusLabelStatus.Text = "就绪";
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabelResult.Text = msg;
                return;
            }
            else // 有结果
            {
                if (checkBoxBlock.Checked) // 分块获取
                {
                    int rowNum = Convert.ToInt32(comboBoxBlockRow.SelectedItem);
                    int colNum = Convert.ToInt32(comboBoxBlockCol.SelectedItem);

                    if (rowNum == 1 && colNum == 1) // 分块数为 1 
                    {
                        if (MessageBox.Show("分块数量为 1，转为普通获取？", "", MessageBoxButtons.OKCancel)
                            == DialogResult.Cancel) // 点击“取消”
                        {
                            toolStripStatusLabelStatus.Text = "就绪";
                            toolStripProgressBar1.Visible = false;
                            return; // 终止获取
                        } // 点击“确定”进入普通获取
                    }
                    else // 进入分块获取
                    {
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

                        // 开始请求
                        toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI", handler.GetNum,
                                totalNum);
                        toolStripProgressBar1.Visible = true;

                        int res = handler.GetBoundsData(rowNum, colNum, dataGridView1); // 分块获取数据
                        toolStripStatusLabelStatus.Text = "就绪";
                        toolStripProgressBar1.Visible = false;
                        string resultMsg = "请求结束";

                        if (res < 0) // 返回错误
                        {
                            string errMsg = PlaceAPIHandler.GetErrMsg(res);
                            MessageBox.Show(errMsg);
                            resultMsg = errMsg;
                        }

                        toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI —— {2}", handler.GetNum,
                                        totalNum, resultMsg);
                        return; // 分块获取结束
                    }
                }

                // 普通获取
                // 初始化状态栏显示
                toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI", handler.GetNum, totalNum);
                toolStripProgressBar1.Visible = true;

                // 普通获取
                requestRegionPoi();
            }
        }

        // 按 region 请求 POI
        private void requestRegionPoi()
        {
            int pageNum = handler.GetLimitedPageNum();
            for (int i = 0; i < pageNum; i++)
            {
                int res = handler.GetRegionPageData(i);
                if (res < 0) // 返回错误
                {
                    string errMsg = PlaceAPIHandler.GetErrMsg(res);
                    MessageBox.Show(errMsg);
                    toolStripStatusLabelStatus.Text = "就绪";
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI —— {2}", handler.GetNum,
                            handler.TotalNum, errMsg);
                    return;
                }
            }
            toolStripStatusLabelStatus.Text = "就绪";
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI —— {2}", handler.GetNum,
                            handler.TotalNum, "请求结束");
        }

        // 取消
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 改变省
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
        }

        // 改变市
        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 关闭分块
            checkBoxBlock.Checked = false;
            initComboBoxBlock();
        }

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

        private void checkBoxCustom_CheckedChanged(object sender, EventArgs e)
        {
            bool isCheck = checkBoxCustom.Checked;

            labelKey1.Enabled = !isCheck;            
            comboBoxKey1.Enabled = !isCheck;
            checkBoxKey2.Enabled = !isCheck;
            comboBoxKey2.Enabled = checkBoxKey2.Checked && !isCheck;

            textBoxKeyWord.Enabled = isCheck;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSave.Text = saveFileDialog1.FileName;
            }
        }

        private void checkBoxKey2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxKey2.Enabled = checkBoxKey2.Checked;
        }

        // 查看总数
        private void buttonTotal_Click(object sender, EventArgs e)
        {
            string keyWord = null;
            if (checkBoxCustom.Checked)
            {
                keyWord = textBoxKeyWord.Text;
                if (string.IsNullOrEmpty(keyWord))
                {
                    MessageBox.Show("请输入自定义关键字");
                    return;
                }
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

            // 开始请求
            Region region = comboBoxCity.SelectedItem as Region;
            handler = new PlaceAPIHandler(region, keyWord);
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

        private void checkBoxExport_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBoxExport.Checked;
            textBoxSave.Enabled = check;
            buttonSave.Enabled = check;
        }

        // 刷新 DataGredView
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            toolStripStatusLabelResult.Text = string.Format("已获取 {0} / {1} 个POI", handler.GetNum, handler.TotalNum);
            Refresh();
        }

        // 分块获取
        private void checkBoxBlock_CheckedChanged(object sender, EventArgs e)
        {
            bool isBlock = checkBoxBlock.Checked;
            buttonBlock.Enabled = isBlock;
            panelBlock.Enabled = isBlock;

            if (isBlock)
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

        // 分块建议
        private void buttonBlock_Click(object sender, EventArgs e)
        {
            // 获取关键字
            string keyWord = null;
            if (checkBoxCustom.Checked)
            {
                keyWord = textBoxKeyWord.Text;
                if (string.IsNullOrEmpty(keyWord))
                {
                    MessageBox.Show("请输入自定义关键字");
                    return;
                }
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
            handler = new PlaceAPIHandler(region, keyWord);
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

                // 调整行列分块数
                if (xNum <= 20)
                {
                    comboBoxBlockCol.SelectedIndex = xNum - 1;
                }
                else
                {
                    comboBoxBlockCol.Items.Add(xNum);
                    comboBoxBlockCol.SelectedIndex = 20;
                }
                if (yNum <= 20)
                {
                    comboBoxBlockRow.SelectedIndex = yNum - 1;
                }
                else
                {
                    comboBoxBlockRow.Items.Add(yNum);
                    comboBoxBlockRow.SelectedIndex = 20;
                }
            }
        }

        // 配置密钥
        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey formKey = new FormKey();
            formKey.ShowDialog();
        }
    }
}
