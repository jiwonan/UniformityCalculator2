namespace UniformityViewer2
{
    partial class frmTester
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pisS = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.psS = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.leS = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pinLines = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mirrorShapeCB = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.lg = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnImageOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.doubleNumericTextBox1 = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.doubleNumericTextBox2 = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.doubleNumericTextBox3 = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.doubleNumericTextBox4 = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pisE = new LetinARUtils.Winforms.DoubleNumericTextBox();
            this.chkInvert = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pisS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pinLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pisE)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(12, 171);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(424, 25);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "확인";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pisS
            // 
            this.pisS.DecimalPlaces = 0;
            this.pisS.InterceptArrowKeys = false;
            this.pisS.Location = new System.Drawing.Point(336, 63);
            this.pisS.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pisS.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pisS.Name = "pisS";
            this.pisS.Size = new System.Drawing.Size(100, 21);
            this.pisS.TabIndex = 7;
            this.pisS.Text = "0.4";
            this.pisS.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.pisS.TextChanged += new System.EventHandler(this.pisS_TextChanged);
            // 
            // psS
            // 
            this.psS.DecimalPlaces = 0;
            this.psS.InterceptArrowKeys = false;
            this.psS.Location = new System.Drawing.Point(115, 63);
            this.psS.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.psS.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.psS.Name = "psS";
            this.psS.Size = new System.Drawing.Size(100, 21);
            this.psS.TabIndex = 6;
            this.psS.Text = "2.4";
            this.psS.Value = new decimal(new int[] {
            24,
            0,
            0,
            65536});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "핀미러 줄수";
            // 
            // leS
            // 
            this.leS.DecimalPlaces = 0;
            this.leS.InterceptArrowKeys = false;
            this.leS.Location = new System.Drawing.Point(336, 5);
            this.leS.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.leS.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.leS.Name = "leS";
            this.leS.Size = new System.Drawing.Size(100, 21);
            this.leS.TabIndex = 2;
            this.leS.Text = "10";
            this.leS.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "핀미러직경";
            // 
            // pinLines
            // 
            this.pinLines.DecimalPlaces = 0;
            this.pinLines.InterceptArrowKeys = false;
            this.pinLines.Location = new System.Drawing.Point(115, 30);
            this.pinLines.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pinLines.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pinLines.Name = "pinLines";
            this.pinLines.Size = new System.Drawing.Size(100, 21);
            this.pinLines.TabIndex = 3;
            this.pinLines.Text = "7";
            this.pinLines.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "동공사이즈";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "미러형상";
            // 
            // mirrorShapeCB
            // 
            this.mirrorShapeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mirrorShapeCB.FormattingEnabled = true;
            this.mirrorShapeCB.Items.AddRange(new object[] {
            "원형",
            "원형-내부원형",
            "육각형",
            "육각형-내부원형"});
            this.mirrorShapeCB.Location = new System.Drawing.Point(115, 4);
            this.mirrorShapeCB.Name = "mirrorShapeCB";
            this.mirrorShapeCB.Size = new System.Drawing.Size(117, 20);
            this.mirrorShapeCB.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(259, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "광효율";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(259, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "미러간격";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // lg
            // 
            this.lg.DecimalPlaces = 0;
            this.lg.Enabled = false;
            this.lg.InterceptArrowKeys = false;
            this.lg.Location = new System.Drawing.Point(336, 32);
            this.lg.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lg.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lg.Name = "lg";
            this.lg.Size = new System.Drawing.Size(100, 21);
            this.lg.TabIndex = 5;
            this.lg.Text = "2";
            this.lg.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "이미지로보기";
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(115, 90);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(286, 21);
            this.txtImagePath.TabIndex = 8;
            // 
            // btnImageOpen
            // 
            this.btnImageOpen.Location = new System.Drawing.Point(407, 90);
            this.btnImageOpen.Name = "btnImageOpen";
            this.btnImageOpen.Size = new System.Drawing.Size(29, 21);
            this.btnImageOpen.TabIndex = 9;
            this.btnImageOpen.Text = "...";
            this.btnImageOpen.UseVisualStyleBackColor = true;
            this.btnImageOpen.Click += new System.EventHandler(this.btnImageOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "아이릴리프(mm)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "가로FOV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(191, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "세로FOV";
            // 
            // doubleNumericTextBox1
            // 
            this.doubleNumericTextBox1.DecimalPlaces = 0;
            this.doubleNumericTextBox1.InterceptArrowKeys = false;
            this.doubleNumericTextBox1.Location = new System.Drawing.Point(115, 117);
            this.doubleNumericTextBox1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox1.Name = "doubleNumericTextBox1";
            this.doubleNumericTextBox1.Size = new System.Drawing.Size(100, 21);
            this.doubleNumericTextBox1.TabIndex = 10;
            this.doubleNumericTextBox1.Text = "17";
            this.doubleNumericTextBox1.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // doubleNumericTextBox2
            // 
            this.doubleNumericTextBox2.DecimalPlaces = 0;
            this.doubleNumericTextBox2.InterceptArrowKeys = false;
            this.doubleNumericTextBox2.Location = new System.Drawing.Point(115, 144);
            this.doubleNumericTextBox2.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox2.Name = "doubleNumericTextBox2";
            this.doubleNumericTextBox2.Size = new System.Drawing.Size(47, 21);
            this.doubleNumericTextBox2.TabIndex = 11;
            this.doubleNumericTextBox2.Text = "20";
            this.doubleNumericTextBox2.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.doubleNumericTextBox2.ValueChanged += new System.EventHandler(this.doubleNumericTextBox2_ValueChanged);
            // 
            // doubleNumericTextBox3
            // 
            this.doubleNumericTextBox3.DecimalPlaces = 0;
            this.doubleNumericTextBox3.InterceptArrowKeys = false;
            this.doubleNumericTextBox3.Location = new System.Drawing.Point(250, 144);
            this.doubleNumericTextBox3.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox3.Name = "doubleNumericTextBox3";
            this.doubleNumericTextBox3.Size = new System.Drawing.Size(50, 21);
            this.doubleNumericTextBox3.TabIndex = 12;
            this.doubleNumericTextBox3.Text = "20";
            this.doubleNumericTextBox3.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(327, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "대각FOV";
            // 
            // doubleNumericTextBox4
            // 
            this.doubleNumericTextBox4.DecimalPlaces = 0;
            this.doubleNumericTextBox4.Enabled = false;
            this.doubleNumericTextBox4.InterceptArrowKeys = false;
            this.doubleNumericTextBox4.Location = new System.Drawing.Point(386, 144);
            this.doubleNumericTextBox4.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.doubleNumericTextBox4.Name = "doubleNumericTextBox4";
            this.doubleNumericTextBox4.Size = new System.Drawing.Size(50, 21);
            this.doubleNumericTextBox4.TabIndex = 13;
            this.doubleNumericTextBox4.Text = "20";
            this.doubleNumericTextBox4.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 202);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(424, 25);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Blended 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pisE
            // 
            this.pisE.DecimalPlaces = 0;
            this.pisE.InterceptArrowKeys = false;
            this.pisE.Location = new System.Drawing.Point(442, 63);
            this.pisE.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pisE.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.pisE.Name = "pisE";
            this.pisE.Size = new System.Drawing.Size(100, 21);
            this.pisE.TabIndex = 7;
            this.pisE.Text = "0.4";
            this.pisE.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            // 
            // chkInvert
            // 
            this.chkInvert.AutoSize = true;
            this.chkInvert.Location = new System.Drawing.Point(350, 122);
            this.chkInvert.Name = "chkInvert";
            this.chkInvert.Size = new System.Drawing.Size(96, 16);
            this.chkInvert.TabIndex = 19;
            this.chkInvert.Text = "미러영역반전";
            this.chkInvert.UseVisualStyleBackColor = true;
            // 
            // frmTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 236);
            this.Controls.Add(this.chkInvert);
            this.Controls.Add(this.btnImageOpen);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mirrorShapeCB);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.pisE);
            this.Controls.Add(this.pisS);
            this.Controls.Add(this.doubleNumericTextBox4);
            this.Controls.Add(this.doubleNumericTextBox3);
            this.Controls.Add(this.doubleNumericTextBox2);
            this.Controls.Add(this.doubleNumericTextBox1);
            this.Controls.Add(this.psS);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lg);
            this.Controls.Add(this.leS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pinLines);
            this.Controls.Add(this.label3);
            this.Name = "frmTester";
            this.Text = "임의생성";
            ((System.ComponentModel.ISupportInitialize)(this.pisS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pinLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleNumericTextBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pisE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private LetinARUtils.Winforms.DoubleNumericTextBox pisS;
        private LetinARUtils.Winforms.DoubleNumericTextBox psS;
        private System.Windows.Forms.Label label9;
        private LetinARUtils.Winforms.DoubleNumericTextBox leS;
        private System.Windows.Forms.Label label4;
        private LetinARUtils.Winforms.DoubleNumericTextBox pinLines;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mirrorShapeCB;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private LetinARUtils.Winforms.DoubleNumericTextBox lg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnImageOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private LetinARUtils.Winforms.DoubleNumericTextBox doubleNumericTextBox1;
        private LetinARUtils.Winforms.DoubleNumericTextBox doubleNumericTextBox2;
        private LetinARUtils.Winforms.DoubleNumericTextBox doubleNumericTextBox3;
        private System.Windows.Forms.Label label8;
        private LetinARUtils.Winforms.DoubleNumericTextBox doubleNumericTextBox4;
        private System.Windows.Forms.Button btnSave;
        private LetinARUtils.Winforms.DoubleNumericTextBox pisE;
        private System.Windows.Forms.CheckBox chkInvert;
    }
}