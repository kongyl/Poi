namespace Poi
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBoxMap = new System.Windows.Forms.GroupBox();
            this.radioButtonBaidu = new System.Windows.Forms.RadioButton();
            this.groupBoxRegion = new System.Windows.Forms.GroupBox();
            this.comboBoxCity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxProvince = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCoor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxKeyWord = new System.Windows.Forms.GroupBox();
            this.checkBoxKey2 = new System.Windows.Forms.CheckBox();
            this.checkBoxCustom = new System.Windows.Forms.CheckBox();
            this.textBoxKeyWord = new System.Windows.Forms.TextBox();
            this.comboBoxKey2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKey1 = new System.Windows.Forms.ComboBox();
            this.labelKey1 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.panelBlock = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBlockCol = new System.Windows.Forms.ComboBox();
            this.comboBoxBlockRow = new System.Windows.Forms.ComboBox();
            this.checkBoxBlock = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxSave = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxExport = new System.Windows.Forms.CheckBox();
            this.buttonTotal = new System.Windows.Forms.Button();
            this.buttonBlock = new System.Windows.Forms.Button();
            this.buttonKey = new System.Windows.Forms.Button();
            this.groupBoxMap.SuspendLayout();
            this.groupBoxRegion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxKeyWord.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.panelBlock.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Controls.Add(this.radioButtonBaidu);
            this.groupBoxMap.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(250, 45);
            this.groupBoxMap.TabIndex = 0;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "地图选择";
            // 
            // radioButtonBaidu
            // 
            this.radioButtonBaidu.AutoSize = true;
            this.radioButtonBaidu.Checked = true;
            this.radioButtonBaidu.Location = new System.Drawing.Point(6, 20);
            this.radioButtonBaidu.Name = "radioButtonBaidu";
            this.radioButtonBaidu.Size = new System.Drawing.Size(71, 16);
            this.radioButtonBaidu.TabIndex = 0;
            this.radioButtonBaidu.TabStop = true;
            this.radioButtonBaidu.Text = "百度地图";
            this.radioButtonBaidu.UseVisualStyleBackColor = true;
            // 
            // groupBoxRegion
            // 
            this.groupBoxRegion.Controls.Add(this.comboBoxCity);
            this.groupBoxRegion.Controls.Add(this.label2);
            this.groupBoxRegion.Controls.Add(this.comboBoxProvince);
            this.groupBoxRegion.Controls.Add(this.label1);
            this.groupBoxRegion.Location = new System.Drawing.Point(12, 63);
            this.groupBoxRegion.Name = "groupBoxRegion";
            this.groupBoxRegion.Size = new System.Drawing.Size(250, 80);
            this.groupBoxRegion.TabIndex = 1;
            this.groupBoxRegion.TabStop = false;
            this.groupBoxRegion.Text = "区域选取";
            // 
            // comboBoxCity
            // 
            this.comboBoxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCity.FormattingEnabled = true;
            this.comboBoxCity.Location = new System.Drawing.Point(86, 46);
            this.comboBoxCity.Name = "comboBoxCity";
            this.comboBoxCity.Size = new System.Drawing.Size(158, 20);
            this.comboBoxCity.TabIndex = 3;
            this.comboBoxCity.SelectedIndexChanged += new System.EventHandler(this.comboBoxCity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "城市";
            // 
            // comboBoxProvince
            // 
            this.comboBoxProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProvince.FormattingEnabled = true;
            this.comboBoxProvince.Location = new System.Drawing.Point(86, 20);
            this.comboBoxProvince.Name = "comboBoxProvince";
            this.comboBoxProvince.Size = new System.Drawing.Size(158, 20);
            this.comboBoxProvince.TabIndex = 1;
            this.comboBoxProvince.SelectedIndexChanged += new System.EventHandler(this.comboBoxProvince_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "省";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnTitle,
            this.ColumnCoor,
            this.ColumnAddr});
            this.dataGridView1.Location = new System.Drawing.Point(268, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(728, 294);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // ColumnId
            // 
            this.ColumnId.FillWeight = 38.64104F;
            this.ColumnId.HeaderText = "编号";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            // 
            // ColumnTitle
            // 
            this.ColumnTitle.FillWeight = 60.9137F;
            this.ColumnTitle.HeaderText = "标题";
            this.ColumnTitle.Name = "ColumnTitle";
            this.ColumnTitle.ReadOnly = true;
            // 
            // ColumnCoor
            // 
            this.ColumnCoor.FillWeight = 106.7219F;
            this.ColumnCoor.HeaderText = "经纬度";
            this.ColumnCoor.Name = "ColumnCoor";
            this.ColumnCoor.ReadOnly = true;
            // 
            // ColumnAddr
            // 
            this.ColumnAddr.FillWeight = 193.7234F;
            this.ColumnAddr.HeaderText = "地址";
            this.ColumnAddr.Name = "ColumnAddr";
            this.ColumnAddr.ReadOnly = true;
            // 
            // groupBoxKeyWord
            // 
            this.groupBoxKeyWord.Controls.Add(this.checkBoxKey2);
            this.groupBoxKeyWord.Controls.Add(this.checkBoxCustom);
            this.groupBoxKeyWord.Controls.Add(this.textBoxKeyWord);
            this.groupBoxKeyWord.Controls.Add(this.comboBoxKey2);
            this.groupBoxKeyWord.Controls.Add(this.comboBoxKey1);
            this.groupBoxKeyWord.Controls.Add(this.labelKey1);
            this.groupBoxKeyWord.Location = new System.Drawing.Point(12, 149);
            this.groupBoxKeyWord.Name = "groupBoxKeyWord";
            this.groupBoxKeyWord.Size = new System.Drawing.Size(250, 106);
            this.groupBoxKeyWord.TabIndex = 3;
            this.groupBoxKeyWord.TabStop = false;
            this.groupBoxKeyWord.Text = "关键词";
            // 
            // checkBoxKey2
            // 
            this.checkBoxKey2.AutoSize = true;
            this.checkBoxKey2.Location = new System.Drawing.Point(8, 48);
            this.checkBoxKey2.Name = "checkBoxKey2";
            this.checkBoxKey2.Size = new System.Drawing.Size(72, 16);
            this.checkBoxKey2.TabIndex = 7;
            this.checkBoxKey2.Text = "二级分类";
            this.checkBoxKey2.UseVisualStyleBackColor = true;
            this.checkBoxKey2.CheckedChanged += new System.EventHandler(this.checkBoxKey2_CheckedChanged);
            // 
            // checkBoxCustom
            // 
            this.checkBoxCustom.AutoSize = true;
            this.checkBoxCustom.Location = new System.Drawing.Point(8, 74);
            this.checkBoxCustom.Name = "checkBoxCustom";
            this.checkBoxCustom.Size = new System.Drawing.Size(60, 16);
            this.checkBoxCustom.TabIndex = 6;
            this.checkBoxCustom.Text = "自定义";
            this.checkBoxCustom.UseVisualStyleBackColor = true;
            this.checkBoxCustom.CheckedChanged += new System.EventHandler(this.checkBoxCustom_CheckedChanged);
            // 
            // textBoxKeyWord
            // 
            this.textBoxKeyWord.Enabled = false;
            this.textBoxKeyWord.Location = new System.Drawing.Point(86, 72);
            this.textBoxKeyWord.Name = "textBoxKeyWord";
            this.textBoxKeyWord.Size = new System.Drawing.Size(158, 21);
            this.textBoxKeyWord.TabIndex = 5;
            // 
            // comboBoxKey2
            // 
            this.comboBoxKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKey2.Enabled = false;
            this.comboBoxKey2.FormattingEnabled = true;
            this.comboBoxKey2.Location = new System.Drawing.Point(86, 46);
            this.comboBoxKey2.Name = "comboBoxKey2";
            this.comboBoxKey2.Size = new System.Drawing.Size(158, 20);
            this.comboBoxKey2.TabIndex = 3;
            // 
            // comboBoxKey1
            // 
            this.comboBoxKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKey1.FormattingEnabled = true;
            this.comboBoxKey1.Location = new System.Drawing.Point(86, 20);
            this.comboBoxKey1.Name = "comboBoxKey1";
            this.comboBoxKey1.Size = new System.Drawing.Size(158, 20);
            this.comboBoxKey1.TabIndex = 1;
            this.comboBoxKey1.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey1_SelectedIndexChanged);
            // 
            // labelKey1
            // 
            this.labelKey1.AutoSize = true;
            this.labelKey1.Location = new System.Drawing.Point(6, 23);
            this.labelKey1.Name = "labelKey1";
            this.labelKey1.Size = new System.Drawing.Size(53, 12);
            this.labelKey1.TabIndex = 0;
            this.labelKey1.Text = "一级分类";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(840, 341);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(921, 341);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this.panelBlock);
            this.groupBoxOther.Controls.Add(this.checkBoxBlock);
            this.groupBoxOther.Location = new System.Drawing.Point(12, 261);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(250, 103);
            this.groupBoxOther.TabIndex = 6;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "分块设置";
            // 
            // panelBlock
            // 
            this.panelBlock.Controls.Add(this.label4);
            this.panelBlock.Controls.Add(this.label3);
            this.panelBlock.Controls.Add(this.comboBoxBlockCol);
            this.panelBlock.Controls.Add(this.comboBoxBlockRow);
            this.panelBlock.Enabled = false;
            this.panelBlock.Location = new System.Drawing.Point(0, 42);
            this.panelBlock.Name = "panelBlock";
            this.panelBlock.Size = new System.Drawing.Size(250, 52);
            this.panelBlock.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "列：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "行：";
            // 
            // comboBoxBlockCol
            // 
            this.comboBoxBlockCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlockCol.FormattingEnabled = true;
            this.comboBoxBlockCol.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBoxBlockCol.Location = new System.Drawing.Point(86, 29);
            this.comboBoxBlockCol.Name = "comboBoxBlockCol";
            this.comboBoxBlockCol.Size = new System.Drawing.Size(158, 20);
            this.comboBoxBlockCol.TabIndex = 1;
            // 
            // comboBoxBlockRow
            // 
            this.comboBoxBlockRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlockRow.FormattingEnabled = true;
            this.comboBoxBlockRow.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBoxBlockRow.Location = new System.Drawing.Point(86, 3);
            this.comboBoxBlockRow.Name = "comboBoxBlockRow";
            this.comboBoxBlockRow.Size = new System.Drawing.Size(158, 20);
            this.comboBoxBlockRow.TabIndex = 0;
            // 
            // checkBoxBlock
            // 
            this.checkBoxBlock.AutoSize = true;
            this.checkBoxBlock.Location = new System.Drawing.Point(6, 20);
            this.checkBoxBlock.Name = "checkBoxBlock";
            this.checkBoxBlock.Size = new System.Drawing.Size(72, 16);
            this.checkBoxBlock.TabIndex = 2;
            this.checkBoxBlock.Text = "分块获取";
            this.checkBoxBlock.UseVisualStyleBackColor = true;
            this.checkBoxBlock.CheckedChanged += new System.EventHandler(this.checkBoxBlock_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(921, 312);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "浏览";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxSave
            // 
            this.textBoxSave.Enabled = false;
            this.textBoxSave.Location = new System.Drawing.Point(352, 314);
            this.textBoxSave.Name = "textBoxSave";
            this.textBoxSave.ReadOnly = true;
            this.textBoxSave.Size = new System.Drawing.Size(563, 21);
            this.textBoxSave.TabIndex = 8;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel 工作簿 (*.xlsx)|*.xlsx";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelResult});
            this.statusStrip1.Location = new System.Drawing.Point(0, 372);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "就绪";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabelStatus.Text = "就绪";
            // 
            // toolStripStatusLabelResult
            // 
            this.toolStripStatusLabelResult.Name = "toolStripStatusLabelResult";
            this.toolStripStatusLabelResult.Size = new System.Drawing.Size(0, 17);
            // 
            // checkBoxExport
            // 
            this.checkBoxExport.AutoSize = true;
            this.checkBoxExport.Location = new System.Drawing.Point(268, 316);
            this.checkBoxExport.Name = "checkBoxExport";
            this.checkBoxExport.Size = new System.Drawing.Size(78, 16);
            this.checkBoxExport.TabIndex = 11;
            this.checkBoxExport.Text = "导出Excel";
            this.checkBoxExport.UseVisualStyleBackColor = true;
            this.checkBoxExport.CheckedChanged += new System.EventHandler(this.checkBoxExport_CheckedChanged);
            // 
            // buttonTotal
            // 
            this.buttonTotal.Location = new System.Drawing.Point(678, 341);
            this.buttonTotal.Name = "buttonTotal";
            this.buttonTotal.Size = new System.Drawing.Size(75, 23);
            this.buttonTotal.TabIndex = 12;
            this.buttonTotal.Text = "查看总数";
            this.buttonTotal.UseVisualStyleBackColor = true;
            this.buttonTotal.Click += new System.EventHandler(this.buttonTotal_Click);
            // 
            // buttonBlock
            // 
            this.buttonBlock.Enabled = false;
            this.buttonBlock.Location = new System.Drawing.Point(759, 341);
            this.buttonBlock.Name = "buttonBlock";
            this.buttonBlock.Size = new System.Drawing.Size(75, 23);
            this.buttonBlock.TabIndex = 13;
            this.buttonBlock.Text = "分块建议";
            this.buttonBlock.UseVisualStyleBackColor = true;
            this.buttonBlock.Click += new System.EventHandler(this.buttonBlock_Click);
            // 
            // buttonKey
            // 
            this.buttonKey.Location = new System.Drawing.Point(268, 341);
            this.buttonKey.Name = "buttonKey";
            this.buttonKey.Size = new System.Drawing.Size(75, 23);
            this.buttonKey.TabIndex = 14;
            this.buttonKey.Text = "配置密钥";
            this.buttonKey.UseVisualStyleBackColor = true;
            this.buttonKey.Click += new System.EventHandler(this.buttonKey_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 394);
            this.Controls.Add(this.buttonKey);
            this.Controls.Add(this.buttonBlock);
            this.Controls.Add(this.buttonTotal);
            this.Controls.Add(this.checkBoxExport);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxSave);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxKeyWord);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBoxRegion);
            this.Controls.Add(this.groupBoxMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POI获取";
            this.groupBoxMap.ResumeLayout(false);
            this.groupBoxMap.PerformLayout();
            this.groupBoxRegion.ResumeLayout(false);
            this.groupBoxRegion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxKeyWord.ResumeLayout(false);
            this.groupBoxKeyWord.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.panelBlock.ResumeLayout(false);
            this.panelBlock.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.RadioButton radioButtonBaidu;
        private System.Windows.Forms.GroupBox groupBoxRegion;
        private System.Windows.Forms.ComboBox comboBoxCity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxProvince;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBoxKeyWord;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxKey2;
        private System.Windows.Forms.ComboBox comboBoxKey1;
        private System.Windows.Forms.Label labelKey1;
        private System.Windows.Forms.TextBox textBoxKeyWord;
        private System.Windows.Forms.CheckBox checkBoxCustom;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox checkBoxKey2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelResult;
        private System.Windows.Forms.CheckBox checkBoxExport;
        private System.Windows.Forms.Button buttonTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCoor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddr;
        private System.Windows.Forms.CheckBox checkBoxBlock;
        private System.Windows.Forms.Panel panelBlock;
        private System.Windows.Forms.Button buttonBlock;
        private System.Windows.Forms.ComboBox comboBoxBlockCol;
        private System.Windows.Forms.ComboBox comboBoxBlockRow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonKey;
    }
}

