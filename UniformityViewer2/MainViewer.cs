using MySql.Data.MySqlClient;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace UniformityViewer2
{
    public partial class MainViewer : Form
    {
        DBMaster master = new DBMaster();
        DBDetail detail = new DBDetail();
        DataParser parser = new DataParser();
        ChartRenderer chartRenderer;

        public MainViewer()
        {
            InitializeComponent();

            chartRenderer = new ChartRenderer(ctlHistLegend1);

            master.LoadMasterData(masterValueListView);

            mirrorShapeComboBox.SelectedIndex = 0;
            mirrorShapeComboBox.SelectedIndexChanged += MirrorShapeComboBox_SelectedIndexChanged;

            SetupContextMenu(); // ?

            ctlHistLegend1.OnMinMaxValueChanged += CtlHistLegend1_OnMinMaxValueChanged;

            // masterValueListView.Controls.Add(contextMenuStrip1);
        }

        private void CtlHistLegend1_OnMinMaxValueChanged()
        {
            throw new NotImplementedException();
        }

        private void MirrorShapeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;

            parser.ResetData();

            if (parser.SetData(SelectedMaster, mirrorShapeComboBox.SelectedIndex))
            {
                if (baseItemComboBox.SelectedIndex >= 0) baseItemComboBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                pictureBox1.Image = null;
            }
            groupBox1.Enabled = true;
        }

        #region setContexStrip1
        private void SetupContextMenu()
        {
            contextMenuStrip1.Items.Add("CSV Export");
            contextMenuStrip1.Items[0].Click += CSVExport_Click;
        }

        private ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();

        private void CSVExport_Click(object sender, EventArgs e)
        {
            Window win = (Window)contextMenuStrip1.Tag;

            if (win.Image == null)
            {
                MessageBox.Show("이미지가 없습니다");
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV파일|*.csv";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Mat resultMat = win.Image;

                    StringBuilder sb = new StringBuilder();

                    for (int row = 0; row < resultMat.Rows; row++)
                    {
                        for (int col = 0; col < resultMat.Cols; col++)
                        {
                            decimal a = (decimal)resultMat.Get<double>(row, col);
                            sb.Append(a.ToString());
                            sb.Append(",");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.AppendLine("");
                    }

                    File.WriteAllText(dlg.FileName, sb.ToString());
                    //MessageBox.Show("저장이 완료되었습니다");
                }
            }

        }
        #endregion


        private Mat SelectedMat = null;
        private int SelectedMaster = -1;

        private void masterValueListView_DoubleClick(object sender, EventArgs e)
        {
            if (masterValueListView.SelectedItems.Count == 0) return;

            pictureBox1.Image = null;

            groupBox1.Enabled = true;

            int masterIdx = int.Parse(masterValueListView.SelectedItems[0].Text);

            if (SelectedMaster == masterIdx) return;
            else SelectedMaster = masterIdx;

            foreach (ListViewItem item in masterValueListView.Items) item.BackColor = Color.White;

            masterValueListView.SelectedItems[0].BackColor = Color.Red;

            baseValueComboBox.Items.Clear();

            parser.ResetData();
            parser.SetData(masterIdx, 0);

            master.LoadPinHeight(pinmirrorWidthValueComboBox, SelectedMaster);
        }

        private void baseItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseValueComboBox.Items.Clear();
            if (SelectedMaster == -1) return;
            if (baseItemComboBox.SelectedIndex == -1) return;

            master.CallValueList(baseItemComboBox.SelectedIndex, SelectedMaster, baseValueComboBox);
            SetupChart(baseItemComboBox.SelectedIndex);

            pictureBox1.Image = null;
        }

        public void LoadChart()
        {
            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));
            
            SelectedMat = chartRenderer.LoadChart(m, GetSelectedRadioIndex(), pictureBox1);
        }

        private void SetupChart(int selectedIndex)
        {
            ListViewItem item = masterValueListView.Items.Find(SelectedMaster.ToString(), true).First();

            if (item == null)
            {
                MessageBox.Show("데이터를 찾을 수 없습니다");
                return;
            }

            switch (selectedIndex)
            {
                case 0: //light

                    xL.Text = "동공크기";
                    xS.Text = item.SubItems[6].Text.ToString();
                    xE.Text = item.SubItems[7].Text.ToString();

                    yL.Text = "핀미러크기";
                    yS.Text = item.SubItems[9].Text.ToString();
                    yE.Text = item.SubItems[10].Text.ToString();

                    break;
                case 1: //pupil

                    xL.Text = "광효율";
                    xS.Text = item.SubItems[3].Text.ToString();
                    xE.Text = item.SubItems[4].Text.ToString();

                    yL.Text = "핀미러크기";
                    yS.Text = item.SubItems[9].Text.ToString();
                    yE.Text = item.SubItems[10].Text.ToString();

                    break;
                case 2: //pinmr

                    xL.Text = "광효율";
                    xS.Text = item.SubItems[3].Text.ToString();
                    xE.Text = item.SubItems[4].Text.ToString();

                    yL.Text = "동공크기";
                    yS.Text = item.SubItems[6].Text.ToString();
                    yE.Text = item.SubItems[7].Text.ToString();

                    break;
            }
        }

        private void baseValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private int GetSelectedRadioIndex()
        {
            return radioButton1.Checked ? 0 : //Max-Avg
                radioButton2.Checked ? 1 : // Min-avg
                radioButton3.Checked ? 2 : //MeanDev
                radioButton4.Checked ? 3 : //MaxAvg-MeanDev Avg1 산술
                radioButton5.Checked ? 4 : //MaxAvg-MeanDev Avg2 기하
                radioButton6.Checked ? 5 : //LumDegree Max
                6; //LumDegree Avg
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point lastClickedPoint = e.Location;

            if (pictureBox1.Image == null) return;

            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            if (m == null) return;

            double widthValue, heightValue, xLoc, yLoc;

            pictureBox1.GetImageRatio(m, lastClickedPoint, out widthValue, out heightValue, out xLoc, out yLoc);

            DrawInfo(m, xLoc, yLoc, widthValue, heightValue);
            pictureBox1.DrawSelectedRect(m, (int)xLoc, (int)yLoc, widthValue, heightValue, SelectedMat);
        }

        private void DrawInfo(Mat m, double xLoc, double yLoc, double widthValue, double heightValue)
        {
            Mat[] channels = m.Split();

            int detailIdx = (int)channels[DataParser.CHANNEL_DETAIL_IDX].At<double>((int)yLoc, (int)xLoc);
            double MaxAvg = channels[DataParser.CHANNEL_MAX_AVG].At<double>((int)yLoc, (int)xLoc);
            double MinAvg = channels[DataParser.CHANNEL_MIN_AVG].At<double>((int)yLoc, (int)xLoc);
            double MeanDev = channels[DataParser.CHANNEL_MEAN_DEV].At<double>((int)yLoc, (int)xLoc);
            double LumDegreeMax = channels[DataParser.CHANNEL_LUMPER_MAX].At<double>((int)yLoc, (int)xLoc);
            double LumDegreeAvg = channels[DataParser.CHANNEL_LUMPER_AVG].At<double>((int)yLoc, (int)xLoc);

            DetailInfo detailInfo = detail.GetDetailInfo(SelectedMaster, detailIdx);


            if (detailInfo.IsFilled == false)
            {
                toolStripStatusLabel1.Text = "";
                return;
            }

            if (baseItemComboBox.SelectedIndex == 0)
            {
                xLabel.Text = detailInfo.Pupil.ToString();
                yLabel.Text = detailInfo.pinMirrorSize.Height.ToString();
            }
            else if (baseItemComboBox.SelectedIndex == 1)
            {
                xLabel.Text = detailInfo.Light.ToString();
                yLabel.Text = detailInfo.pinMirrorSize.Height.ToString();
            }
            else if (baseItemComboBox.SelectedIndex == 2)
            {
                xLabel.Text = detailInfo.Light.ToString();
                yLabel.Text = detailInfo.Pupil.ToString();
            }


            xLabel.Location = new System.Drawing.Point((int)(xLoc / widthValue) + pictureBox1.Left, xE.Top);
            yLabel.Location = new System.Drawing.Point(yE.Right - yLabel.Width, (int)(yLoc / heightValue) + pictureBox1.Top);


            double pinmirrorGap = CalcValues.GetPinMirrorGap(detailInfo.Light, detailInfo.pinMirrorSize.Height);

            toolStripStatusLabel1.Text = $"광효율:{detailInfo.Light}% 동공크기:{detailInfo.Pupil}mm 핀미러크기:{detailInfo.pinMirrorSize}mm  핀미러간격:{pinmirrorGap}mm Max-Avg:{MaxAvg:0.0000} Min-Avg:{MinAvg:0.0000} 표준편차:{MeanDev:0.0000} 각도당휘도(Max):{LumDegreeMax:0.0000} 각도당휘도(Avg):{LumDegreeAvg:0.0000}";
        }

        private void pinmirrorWidthValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private System.Drawing.Point LastSelectedCoord;
        
        

        private void radioButton_Click(object sender, EventArgs e)
        {
            baseValueComboBox_SelectedIndexChanged(sender, e);
        }

        DetailInfo tmpContextData;

        private void ShowDetail(MouseButtons button)
        {
            ShowDetail(LastSelectedCoord.X, LastSelectedCoord.Y, button);
        }

        private void ShowDetail(int xLoc, int yLoc)
        {
            ShowDetail(xLoc, yLoc, MouseButtons.Left, false);
        }

        private void ShowDetail(int xLoc, int yLoc, MouseButtons button, bool show = true)
        {
            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1)
            {
                return;
            }

            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            if (m == null) return;

            Mat[] channels = m.Split();

            if (xLoc < 0 || xLoc >= m.Width || yLoc < 0 || yLoc >= m.Height)
            {
                return;
            }
            int detailIdx = (int)channels[DataParser.CHANNEL_DETAIL_IDX].At<double>(yLoc, xLoc);
            LastSelectedCoord = new System.Drawing.Point(xLoc, yLoc);

            DetailInfo detailInfo = detail.GetDetailInfo(SelectedMaster, detailIdx);

            if (detailInfo.IsFilled == false) return;


            if (button == MouseButtons.Right)
            {
                int lines = master.GetSelectedPinLinesAndInnerPercent(SelectedMaster).Item1;

                if (lines == -1)
                {
                    MessageBox.Show("오류!!");
                    return;
                }

                tmpContextData = detailInfo;
                contextMenuStrip2.Show(pictureBox1, System.Drawing.Point.Empty);
            }
            else if (button == MouseButtons.Left)
            {
                frmDataViewer resultViewer;

                if (show)
                {
                    if (viewer를새창으로ToolStripMenuItem.Checked)
                    {
                        resultViewer = new frmDataViewer();
                    }
                    else
                    {
                        resultViewer = frmDataViewer.GetInstance();
                    }
                    resultViewer.LoadResultData(detailInfo, master.GetSelectedPinLinesAndInnerPercent(SelectedMaster), mirrorShapeComboBox.SelectedIndex == 0 ? "Circle" :
                                                                                                                       mirrorShapeComboBox.SelectedIndex == 1 ? "Circle_Circle" : 
                                                                                                                       mirrorShapeComboBox.SelectedIndex == 2 ? "Hexa" : "Hexa_Circle");
                    resultViewer.Show();
                }
                else
                {
                    if (frmDataViewer.HasInstance)
                    {
                        resultViewer = frmDataViewer.GetInstance();
                        resultViewer.LoadResultData(detailInfo, master.GetSelectedPinLinesAndInnerPercent(SelectedMaster), mirrorShapeComboBox.SelectedIndex == 0 ? "Circle" :
                                                                                                                           mirrorShapeComboBox.SelectedIndex == 1 ? "Circle_Circle" : 
                                                                                                                           mirrorShapeComboBox.SelectedIndex == 2 ? "Hexa" : "Hexa_Circle");
                        resultViewer.Show();
                    }
                }
            }

        }

        private void MainViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
                Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

                int xLoc = LastSelectedCoord.X;
                int yLoc = LastSelectedCoord.Y;

                switch (e.KeyCode)
                {
                    case Keys.A:
                        xLoc--;
                        break;
                    case Keys.D:
                        xLoc++;
                        break;
                    case Keys.W:
                        yLoc--;
                        break;
                    case Keys.S:
                        yLoc++;
                        break;
                }
                ShowDetail(xLoc, yLoc);

                double widthValue, heightValue;

                pictureBox1.GetImageRatio(m, xLoc, yLoc, out widthValue, out heightValue);

                DrawInfo(m, xLoc, yLoc, widthValue, heightValue);
                pictureBox1.DrawSelectedRect(m, (int)xLoc, (int)yLoc, widthValue, heightValue, SelectedMat);
                //DrawSelectedRect(m, xLoc, yLoc);
            }
            else
            {
                return;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            double xLoc, yLoc;

            if (pictureBox1.Image == null) return;

            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            pictureBox1.GetXYLocation(m, e.Location, out xLoc, out yLoc);
            LastSelectedCoord = new System.Drawing.Point((int)xLoc, (int)yLoc);
            ShowDetail(e.Button);

        }

        private void viewer를새창으로ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = !item.Checked;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTester.Instance.Show();
        }
        private void goTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tmpContextData.IsFilled == false) return;
            frmTester.Instance.SetData(tmpContextData.ShapeType, tmpContextData.Light, master.GetSelectedPinLinesAndInnerPercent(tmpContextData.MasterIdx).Item1, tmpContextData.Pupil, tmpContextData.pinMirrorSize);
            frmTester.Instance.Show();
        }
    }
}
