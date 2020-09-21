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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.picPinmirror = new System.Windows.Forms.PictureBox();
            this.picPsf = new UniformityViewer2.LinePictureBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chartTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chartWidth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MMperLightChartHorizon = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MMperLightChartVertical = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.splitContainer3.Panel2.SuspendLayout();
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
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.textBox1);
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
            this.tabPage1.Controls.Add(this.chartWidth);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(406, 197);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MTF Chart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chartWidth
            // 
            chartArea4.AxisX.Title = "CPD";
            chartArea4.AxisY.Title = "MTF";
            chartArea4.Name = "ChartArea1";
            this.chartWidth.ChartAreas.Add(chartArea4);
            this.chartWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartWidth.Legends.Add(legend4);
            this.chartWidth.Location = new System.Drawing.Point(3, 3);
            this.chartWidth.Name = "chartWidth";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "MTF_S";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "MTF_T";
            this.chartWidth.Series.Add(series5);
            this.chartWidth.Series.Add(series6);
            this.chartWidth.Size = new System.Drawing.Size(400, 191);
            this.chartWidth.TabIndex = 0;
            this.chartWidth.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "가로기준";
            this.chartWidth.Titles.Add(title2);
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
            chartArea5.Name = "ChartArea1";
            this.MMperLightChartHorizon.ChartAreas.Add(chartArea5);
            this.MMperLightChartHorizon.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Name = "Legend1";
            this.MMperLightChartHorizon.Legends.Add(legend5);
            this.MMperLightChartHorizon.Location = new System.Drawing.Point(3, 3);
            this.MMperLightChartHorizon.Name = "MMperLightChartHorizon";
            this.MMperLightChartHorizon.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.MMperLightChartHorizon.Series.Add(series7);
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
            chartArea6.Name = "ChartArea1";
            this.MMperLightChartVertical.ChartAreas.Add(chartArea6);
            this.MMperLightChartVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "Legend1";
            this.MMperLightChartVertical.Legends.Add(legend6);
            this.MMperLightChartVertical.Location = new System.Drawing.Point(3, 3);
            this.MMperLightChartVertical.Name = "MMperLightChartVertical";
            this.MMperLightChartVertical.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.MMperLightChartVertical.Series.Add(series8);
            this.MMperLightChartVertical.Size = new System.Drawing.Size(400, 191);
            this.MMperLightChartVertical.TabIndex = 0;
            this.MMperLightChartVertical.Text = "chart1";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(414, 223);
            this.textBox1.TabIndex = 0;
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
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.chartTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
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
        private UniformityViewer2.LinePictureBox picPsf;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWidth;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart MMperLightChartHorizon;
        private System.Windows.Forms.DataVisualization.Charting.Chart MMperLightChartVertical;
        private System.Windows.Forms.TextBox textBox1;
    }
}