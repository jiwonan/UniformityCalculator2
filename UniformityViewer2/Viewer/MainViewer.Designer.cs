namespace UniformityViewer2.Viewer
{
    partial class MainViewer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.masterValueListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.baseItemComboBox = new System.Windows.Forms.ComboBox();
            this.baseValueComboBox = new System.Windows.Forms.ComboBox();
            this.yL = new System.Windows.Forms.Label();
            this.yE = new System.Windows.Forms.Label();
            this.yS = new System.Windows.Forms.Label();
            this.xS = new System.Windows.Forms.Label();
            this.xE = new System.Windows.Forms.Label();
            this.xL = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtfInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.mtf_rLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mtf_dLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.ctlHistLegend1 = new UniformityViewer2.Controls.ctlHistLegend();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new UniformityViewer2.Controls.TempPictureBox();
            this.pinmirrorWidthValueComboBox = new System.Windows.Forms.ComboBox();
            this.mirrorShapeComboBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.옵션ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewer를새창으로ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.mtfInfoGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // masterValueListView
            // 
            this.masterValueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.masterValueListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.masterValueListView.FullRowSelect = true;
            this.masterValueListView.HideSelection = false;
            this.masterValueListView.Location = new System.Drawing.Point(0, 24);
            this.masterValueListView.MultiSelect = false;
            this.masterValueListView.Name = "masterValueListView";
            this.masterValueListView.Size = new System.Drawing.Size(1203, 97);
            this.masterValueListView.TabIndex = 0;
            this.masterValueListView.UseCompatibleStateImageBehavior = false;
            this.masterValueListView.View = System.Windows.Forms.View.Details;
            this.masterValueListView.DoubleClick += new System.EventHandler(this.masterValueListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Idx";
            this.columnHeader1.Width = 37;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "생성일시";
            this.columnHeader2.Width = 132;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "핀미러행수";
            this.columnHeader3.Width = 79;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "광효율시작값";
            this.columnHeader4.Width = 91;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "광효율끝값";
            this.columnHeader5.Width = 77;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "광효율간격";
            this.columnHeader6.Width = 76;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "동공크기시작값";
            this.columnHeader7.Width = 102;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "동공크기끝값";
            this.columnHeader8.Width = 91;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "동공크기간격";
            this.columnHeader9.Width = 88;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "핀미러크기시작값";
            this.columnHeader10.Width = 114;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "핀미러크기끝값";
            this.columnHeader11.Width = 102;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "핀미러크기간격";
            this.columnHeader12.Width = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "기준항목";
            // 
            // baseItemComboBox
            // 
            this.baseItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseItemComboBox.FormattingEnabled = true;
            this.baseItemComboBox.Items.AddRange(new object[] {
            "광효율",
            "동공사이즈",
            "핀미러 직경"});
            this.baseItemComboBox.Location = new System.Drawing.Point(77, 58);
            this.baseItemComboBox.Name = "baseItemComboBox";
            this.baseItemComboBox.Size = new System.Drawing.Size(117, 20);
            this.baseItemComboBox.TabIndex = 2;
            this.baseItemComboBox.SelectedIndexChanged += new System.EventHandler(this.baseItemComboBox_SelectedIndexChanged);
            // 
            // baseValueComboBox
            // 
            this.baseValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseValueComboBox.FormattingEnabled = true;
            this.baseValueComboBox.Items.AddRange(new object[] {
            "광효율",
            "동공사이즈",
            "핀미러 직경"});
            this.baseValueComboBox.Location = new System.Drawing.Point(77, 84);
            this.baseValueComboBox.Name = "baseValueComboBox";
            this.baseValueComboBox.Size = new System.Drawing.Size(117, 20);
            this.baseValueComboBox.TabIndex = 2;
            this.baseValueComboBox.SelectedIndexChanged += new System.EventHandler(this.baseValueComboBox_SelectedIndexChanged);
            // 
            // yL
            // 
            this.yL.Location = new System.Drawing.Point(198, 302);
            this.yL.Name = "yL";
            this.yL.Size = new System.Drawing.Size(88, 12);
            this.yL.TabIndex = 1;
            this.yL.Text = "label111";
            this.yL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // yE
            // 
            this.yE.Location = new System.Drawing.Point(198, 17);
            this.yE.Name = "yE";
            this.yE.Size = new System.Drawing.Size(88, 12);
            this.yE.TabIndex = 1;
            this.yE.Text = "label1";
            this.yE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // yS
            // 
            this.yS.Location = new System.Drawing.Point(198, 605);
            this.yS.Name = "yS";
            this.yS.Size = new System.Drawing.Size(88, 12);
            this.yS.TabIndex = 1;
            this.yS.Text = "label1";
            this.yS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xS
            // 
            this.xS.Location = new System.Drawing.Point(286, 620);
            this.xS.Name = "xS";
            this.xS.Size = new System.Drawing.Size(94, 10);
            this.xS.TabIndex = 1;
            this.xS.Text = "label1";
            this.xS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xE
            // 
            this.xE.AutoEllipsis = true;
            this.xE.Location = new System.Drawing.Point(798, 621);
            this.xE.Name = "xE";
            this.xE.Size = new System.Drawing.Size(94, 10);
            this.xE.TabIndex = 1;
            this.xE.Text = "label1";
            this.xE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xL
            // 
            this.xL.Location = new System.Drawing.Point(551, 620);
            this.xL.Name = "xL";
            this.xL.Size = new System.Drawing.Size(94, 10);
            this.xL.TabIndex = 1;
            this.xL.Text = "label1";
            this.xL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "기준값";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(77, 145);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(75, 16);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Max-Avg";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(77, 167);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "Min-Avg";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(77, 189);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 16);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "표준편차";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.mtfInfoGroupBox);
            this.groupBox1.Controls.Add(this.xLabel);
            this.groupBox1.Controls.Add(this.yLabel);
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.ctlHistLegend1);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.yL);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.yE);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.yS);
            this.groupBox1.Controls.Add(this.pinmirrorWidthValueComboBox);
            this.groupBox1.Controls.Add(this.baseValueComboBox);
            this.groupBox1.Controls.Add(this.xS);
            this.groupBox1.Controls.Add(this.mirrorShapeComboBox);
            this.groupBox1.Controls.Add(this.baseItemComboBox);
            this.groupBox1.Controls.Add(this.xE);
            this.groupBox1.Controls.Add(this.xL);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(0, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1203, 665);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // mtfInfoGroupBox
            // 
            this.mtfInfoGroupBox.Controls.Add(this.mtf_rLabel);
            this.mtfInfoGroupBox.Controls.Add(this.label7);
            this.mtfInfoGroupBox.Controls.Add(this.mtf_dLabel);
            this.mtfInfoGroupBox.Controls.Add(this.label4);
            this.mtfInfoGroupBox.Location = new System.Drawing.Point(10, 532);
            this.mtfInfoGroupBox.Name = "mtfInfoGroupBox";
            this.mtfInfoGroupBox.Size = new System.Drawing.Size(200, 100);
            this.mtfInfoGroupBox.TabIndex = 7;
            this.mtfInfoGroupBox.TabStop = false;
            this.mtfInfoGroupBox.Text = "MTF Info";
            // 
            // mtf_rLabel
            // 
            this.mtf_rLabel.AutoSize = true;
            this.mtf_rLabel.Location = new System.Drawing.Point(6, 81);
            this.mtf_rLabel.Name = "mtf_rLabel";
            this.mtf_rLabel.Size = new System.Drawing.Size(38, 12);
            this.mtf_rLabel.TabIndex = 3;
            this.mtf_rLabel.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "크기";
            // 
            // mtf_dLabel
            // 
            this.mtf_dLabel.AutoSize = true;
            this.mtf_dLabel.Location = new System.Drawing.Point(6, 39);
            this.mtf_dLabel.Name = "mtf_dLabel";
            this.mtf_dLabel.Size = new System.Drawing.Size(38, 12);
            this.mtf_dLabel.TabIndex = 1;
            this.mtf_dLabel.Text = "label6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "간격";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(323, 620);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(38, 12);
            this.xLabel.TabIndex = 1;
            this.xLabel.Text = "label1";
            this.xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(244, 66);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(38, 12);
            this.yLabel.TabIndex = 1;
            this.yLabel.Text = "label1";
            this.yLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(77, 277);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(118, 16);
            this.radioButton7.TabIndex = 12;
            this.radioButton7.Text = "각도당휘도(AVG)";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(77, 255);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(120, 16);
            this.radioButton6.TabIndex = 12;
            this.radioButton6.Text = "각도당휘도(MAX)";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // ctlHistLegend1
            // 
            this.ctlHistLegend1.HistMat = null;
            this.ctlHistLegend1.Location = new System.Drawing.Point(894, 17);
            this.ctlHistLegend1.MaxVal = 0D;
            this.ctlHistLegend1.MinimumSize = new System.Drawing.Size(100, 100);
            this.ctlHistLegend1.MinVal = 0D;
            this.ctlHistLegend1.Name = "ctlHistLegend1";
            this.ctlHistLegend1.Size = new System.Drawing.Size(100, 600);
            this.ctlHistLegend1.TabIndex = 11;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(77, 233);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(183, 16);
            this.radioButton5.TabIndex = 10;
            this.radioButton5.Text = "Max-Avg, 표준편차 기하평균";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(77, 211);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(183, 16);
            this.radioButton4.TabIndex = 10;
            this.radioButton4.Text = "Max-Avg, 표준편차 산술평균";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1197, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel1.Text = "TEXT HERE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "미러형상";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "핀미러가로크기";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(288, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // pinmirrorWidthValueComboBox
            // 
            this.pinmirrorWidthValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pinmirrorWidthValueComboBox.FormattingEnabled = true;
            this.pinmirrorWidthValueComboBox.Items.AddRange(new object[] {
            "광효율",
            "동공사이즈",
            "핀미러 직경"});
            this.pinmirrorWidthValueComboBox.Location = new System.Drawing.Point(107, 115);
            this.pinmirrorWidthValueComboBox.Name = "pinmirrorWidthValueComboBox";
            this.pinmirrorWidthValueComboBox.Size = new System.Drawing.Size(117, 20);
            this.pinmirrorWidthValueComboBox.TabIndex = 2;
            this.pinmirrorWidthValueComboBox.SelectedIndexChanged += new System.EventHandler(this.pinmirrorWidthValueComboBox_SelectedIndexChanged);
            // 
            // mirrorShapeComboBox
            // 
            this.mirrorShapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mirrorShapeComboBox.FormattingEnabled = true;
            this.mirrorShapeComboBox.Items.AddRange(new object[] {
            "원형",
            "원형-내부원형",
            "육각형",
            "육각형-내부원형"});
            this.mirrorShapeComboBox.Location = new System.Drawing.Point(75, 20);
            this.mirrorShapeComboBox.Name = "mirrorShapeComboBox";
            this.mirrorShapeComboBox.Size = new System.Drawing.Size(117, 20);
            this.mirrorShapeComboBox.TabIndex = 2;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goTesterToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // goTesterToolStripMenuItem
            // 
            this.goTesterToolStripMenuItem.Name = "goTesterToolStripMenuItem";
            this.goTesterToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.goTesterToolStripMenuItem.Text = "Go Tester";
            this.goTesterToolStripMenuItem.Click += new System.EventHandler(this.goTesterToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.tESTToolStripMenuItem,
            this.옵션ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1203, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitXToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // exitXToolStripMenuItem
            // 
            this.exitXToolStripMenuItem.Name = "exitXToolStripMenuItem";
            this.exitXToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitXToolStripMenuItem.Text = "Exit(&X)";
            this.exitXToolStripMenuItem.Click += new System.EventHandler(this.exitXToolStripMenuItem_Click);
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customToolStripMenuItem});
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.tESTToolStripMenuItem.Text = "Custom";
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.customToolStripMenuItem.Text = "Custom Test";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // 옵션ToolStripMenuItem
            // 
            this.옵션ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewer를새창으로ToolStripMenuItem});
            this.옵션ToolStripMenuItem.Name = "옵션ToolStripMenuItem";
            this.옵션ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.옵션ToolStripMenuItem.Text = "옵션";
            // 
            // viewer를새창으로ToolStripMenuItem
            // 
            this.viewer를새창으로ToolStripMenuItem.Name = "viewer를새창으로ToolStripMenuItem";
            this.viewer를새창으로ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.viewer를새창으로ToolStripMenuItem.Text = "Viewer를 새창으로";
            this.viewer를새창으로ToolStripMenuItem.Click += new System.EventHandler(this.viewer를새창으로ToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 12F);
            this.label6.Location = new System.Drawing.Point(1152, 620);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ver.2";
            // 
            // MainViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 786);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.masterValueListView);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1219, 825);
            this.Name = "MainViewer";
            this.Text = "뷰어뷰어뷰어뷰어~";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainViewer_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mtfInfoGroupBox.ResumeLayout(false);
            this.mtfInfoGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView masterValueListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox baseItemComboBox;
        private Controls.TempPictureBox pictureBox1;
        private System.Windows.Forms.ComboBox baseValueComboBox;
        private System.Windows.Forms.Label yL;
        private System.Windows.Forms.Label yE;
        private System.Windows.Forms.Label yS;
        private System.Windows.Forms.Label xS;
        private System.Windows.Forms.Label xE;
        private System.Windows.Forms.Label xL;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private Controls.ctlHistLegend ctlHistLegend1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mirrorShapeComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem goTesterToolStripMenuItem;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox pinmirrorWidthValueComboBox;
        private System.Windows.Forms.ToolStripMenuItem 옵션ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewer를새창으로ToolStripMenuItem;
        private System.Windows.Forms.GroupBox mtfInfoGroupBox;
        private System.Windows.Forms.Label mtf_rLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label mtf_dLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}

