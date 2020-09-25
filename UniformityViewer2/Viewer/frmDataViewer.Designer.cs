namespace UniformityViewer2.Viewer
{
    partial class frmDataViewer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.picPinmirror = new System.Windows.Forms.PictureBox();
            this.picPsf = new UniformityViewer2.Controls.LinePictureBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chartTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mtf_r = new System.Windows.Forms.Label();
            this.mtf_d = new System.Windows.Forms.Label();
            this.chartWidth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MMperLightChartHorizon = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MMperLightChartVertical = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPinmirror)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.chartTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWidth)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MMperLightChartHorizon)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MMperLightChartVertical)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 382;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.picPinmirror);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picPsf);
            this.splitContainer2.Size = new System.Drawing.Size(382, 450);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 0;
            // 
            // picPinmirror
            // 
            this.picPinmirror.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPinmirror.Location = new System.Drawing.Point(0, 0);
            this.picPinmirror.Name = "picPinmirror";
            this.picPinmirror.Size = new System.Drawing.Size(382, 223);
            this.picPinmirror.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPinmirror.TabIndex = 0;
            this.picPinmirror.TabStop = false;
            // 
            // picPsf
            // 
            this.picPsf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPsf.Image = null;
            this.picPsf.Location = new System.Drawing.Point(0, 0);
            this.picPsf.Name = "picPsf";
            this.picPsf.Size = new System.Drawing.Size(382, 223);
            this.picPsf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPsf.TabIndex = 1;
            this.picPsf.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chartTabControl);
            this.splitContainer3.Size = new System.Drawing.Size(414, 450);
            this.splitContainer3.SplitterDistance = 223;
            this.splitContainer3.TabIndex = 0;
            // 
            // chartTabControl
            // 
            this.chartTabControl.Controls.Add(this.tabPage1);
            this.chartTabControl.Controls.Add(this.tabPage2);
            this.chartTabControl.Controls.Add(this.tabPage3);
            this.chartTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTabControl.Location = new System.Drawing.Point(0, 0);
            this.chartTabControl.Name = "chartTabControl";
            this.chartTabControl.SelectedIndex = 0;
            this.chartTabControl.Size = new System.Drawing.Size(414, 223);
            this.chartTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mtf_r);
            this.tabPage1.Controls.Add(this.mtf_d);
            this.tabPage1.Controls.Add(this.chartWidth);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(406, 197);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MTF Chart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mtf_r
            // 
            this.mtf_r.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mtf_r.AutoSize = true;
            this.mtf_r.Location = new System.Drawing.Point(315, 175);
            this.mtf_r.Name = "mtf_r";
            this.mtf_r.Size = new System.Drawing.Size(29, 12);
            this.mtf_r.TabIndex = 2;
            this.mtf_r.Text = "크기";
            // 
            // mtf_d
            // 
            this.mtf_d.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mtf_d.AutoSize = true;
            this.mtf_d.Location = new System.Drawing.Point(315, 156);
            this.mtf_d.Name = "mtf_d";
            this.mtf_d.Size = new System.Drawing.Size(29, 12);
            this.mtf_d.TabIndex = 1;
            this.mtf_d.Text = "간격";
            // 
            // chartWidth
            // 
            chartArea1.AxisX.Title = "CPD";
            chartArea1.AxisY.Title = "MTF";
            chartArea1.Name = "ChartArea1";
            this.chartWidth.ChartAreas.Add(chartArea1);
            this.chartWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartWidth.Legends.Add(legend1);
            this.chartWidth.Location = new System.Drawing.Point(3, 3);
            this.chartWidth.Name = "chartWidth";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "MTF_S";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "MTF_T";
            this.chartWidth.Series.Add(series1);
            this.chartWidth.Series.Add(series2);
            this.chartWidth.Size = new System.Drawing.Size(400, 191);
            this.chartWidth.TabIndex = 0;
            this.chartWidth.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "가로기준";
            this.chartWidth.Titles.Add(title1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MMperLightChartHorizon);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(406, 197);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Horizon Chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MMperLightChartHorizon
            // 
            chartArea2.Name = "ChartArea1";
            this.MMperLightChartHorizon.ChartAreas.Add(chartArea2);
            this.MMperLightChartHorizon.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.MMperLightChartHorizon.Legends.Add(legend2);
            this.MMperLightChartHorizon.Location = new System.Drawing.Point(3, 3);
            this.MMperLightChartHorizon.Name = "MMperLightChartHorizon";
            this.MMperLightChartHorizon.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.MMperLightChartHorizon.Series.Add(series3);
            this.MMperLightChartHorizon.Size = new System.Drawing.Size(400, 191);
            this.MMperLightChartHorizon.TabIndex = 0;
            this.MMperLightChartHorizon.Text = "chart2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.MMperLightChartVertical);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(406, 197);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Vertical Chart";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MMperLightChartVertical
            // 
            chartArea3.Name = "ChartArea1";
            this.MMperLightChartVertical.ChartAreas.Add(chartArea3);
            this.MMperLightChartVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.MMperLightChartVertical.Legends.Add(legend3);
            this.MMperLightChartVertical.Location = new System.Drawing.Point(3, 3);
            this.MMperLightChartVertical.Name = "MMperLightChartVertical";
            this.MMperLightChartVertical.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.MMperLightChartVertical.Series.Add(series4);
            this.MMperLightChartVertical.Size = new System.Drawing.Size(400, 191);
            this.MMperLightChartVertical.TabIndex = 0;
            this.MMperLightChartVertical.Text = "chart1";
            // 
            // frmDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmDataViewer";
            this.Text = "frmDataViewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPinmirror)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsf)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.chartTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWidth)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MMperLightChartHorizon)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MMperLightChartVertical)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.PictureBox picPinmirror;
        private UniformityViewer2.Controls.LinePictureBox picPsf;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWidth;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart MMperLightChartHorizon;
        private System.Windows.Forms.DataVisualization.Charting.Chart MMperLightChartVertical;
        private System.Windows.Forms.Label mtf_r;
        private System.Windows.Forms.Label mtf_d;
    }
}