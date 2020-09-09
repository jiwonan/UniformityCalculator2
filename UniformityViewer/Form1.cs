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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace UniformityViewer
{
    public partial class Form1 : Form
    {
        private static MySqlConnection conn = new MySqlConnection("Server = 192.168.29.20; Port=3939; Database = letinar_uniform; Uid = user; Pwd = QqN3y29JrK1nPtlk#;Connection Timeout=15");

        public static MySqlConnection GetConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }

            return conn;
        }
        DataParser parser = new DataParser();
        public Form1()
        {
            InitializeComponent();
            LoadMasterData();

            mirrorShapeComboBox.SelectedIndex = 0;

            mirrorShapeComboBox.SelectedIndexChanged += comboBox3_SelectedIndexChanged;


            SetupContextMenu();

            ctlHistLegend1.OnMinMaxValueChanged += new ctlHistLegend.OnMinMaxValueChangedDelegate(LoadChart);

        }

        private static string SELECT_MASTER = "SELECT * FROM uniform_master WHERE worktype = 1 ORDER BY idx desc";
        private static string SELECT_MASTER_ONE = "SELECT * FROM uniform_master WHERE idx = @idx ORDER BY idx";
        private void LoadMasterData()
        {
            masterValueListView.Items.Clear();

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER, con))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idx = reader.GetInt32(0);
                    DateTime dtm = reader.GetDateTime(1);
                    int pinline = reader.GetInt32(2);
                    double light_s = reader.GetDouble(3);
                    double light_e = reader.GetDouble(4);
                    double light_g = reader.GetDouble(5);
                    double pupil_s = reader.GetDouble(6);
                    double pupil_e = reader.GetDouble(7);
                    double pupil_g = reader.GetDouble(8);
                    double pin_s = reader.GetDouble(9);
                    double pin_e = reader.GetDouble(10);
                    double pin_g = reader.GetDouble(11);

                    ListViewItem item = masterValueListView.Items.Add(idx.ToString(), idx.ToString(), 0);
                    item.SubItems.Add(dtm.ToString());
                    item.SubItems.Add(pinline.ToString());
                    item.SubItems.Add(light_s.ToString());
                    item.SubItems.Add(light_e.ToString());
                    item.SubItems.Add(light_g.ToString());
                    item.SubItems.Add(pupil_s.ToString());
                    item.SubItems.Add(pupil_e.ToString());
                    item.SubItems.Add(pupil_g.ToString());
                    item.SubItems.Add(pin_s.ToString());
                    item.SubItems.Add(pin_e.ToString());
                    item.SubItems.Add(pin_g.ToString());


                }


            }

        }

        private void baseItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseValueComboBox.Items.Clear();
            if (SelectedMaster == -1) return;
            if (baseItemComboBox.SelectedIndex == -1) return;

            CallValueList(baseItemComboBox.SelectedIndex);
            SetupChart(baseItemComboBox.SelectedIndex);

            pictureBox1.Image = null;
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

        private void CallValueList(int selectedIndex)
        {
            if (SelectedMaster == -1) return;

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER_ONE, con))
            {
                cmd.Parameters.AddWithValue("@idx", SelectedMaster);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idx = reader.GetInt32(0);
                    DateTime dtm = reader.GetDateTime(1);
                    int pinline = reader.GetInt32(2);
                    double light_s = reader.GetDouble(3);
                    double light_e = reader.GetDouble(4);
                    double light_g = reader.GetDouble(5);
                    double pupil_s = reader.GetDouble(6);
                    double pupil_e = reader.GetDouble(7);
                    double pupil_g = reader.GetDouble(8);
                    double pin_s = reader.GetDouble(9);
                    double pin_e = reader.GetDouble(10);
                    double pin_g = reader.GetDouble(11);

                    double s = 0, e = 0, g = 0;

                    switch (selectedIndex)
                    {
                        case 0: //광효율
                            s = light_s;
                            e = light_e;
                            g = light_g;
                            break;
                        case 1: //동공사이즈
                            s = pupil_s;
                            e = pupil_e;
                            g = pupil_g;
                            break;
                        case 2: //핀미러직경
                            s = pin_s;
                            e = pin_e;
                            g = pin_g;
                            break;
                    }

                    for (decimal v = (decimal)s; v <= (decimal)e; v += (decimal)g)
                    {
                        baseValueComboBox.Items.Add(v.ToString());
                    }
                }


            }
        }

        private int SelectedMaster = -1;
        private void masterValueListView_DoubleClick(object sender, EventArgs e)
        {
            if (masterValueListView.SelectedItems.Count == 0) return;

            groupBox1.Enabled = true;

            int masterIdx = int.Parse(masterValueListView.SelectedItems[0].Text);

            if (SelectedMaster == masterIdx) return;
            else SelectedMaster = masterIdx;

            foreach (ListViewItem item in masterValueListView.Items) item.BackColor = Color.White;

            masterValueListView.SelectedItems[0].BackColor = Color.Red;

            baseValueComboBox.Items.Clear();

            parser.ResetData();
            parser.SetData(SelectedMaster, 0);


            LoadPinHeight();
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

        private void baseValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
            LoadChart();
        }

        private void LoadChart()
        {
            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            if (m == null) return;

            int gubun = GetSelectedRadioIndex();

            Mat[] channels = m.Split();

            Mat origin = new Mat();

            Mat targetMat = new Mat(); // = channels[idx];

            if (gubun == 0)
            {
                targetMat = channels[DataParser.CHANNEL_MAX_AVG];
            }
            else if (gubun == 1)
            {
                targetMat = channels[DataParser.CHANNEL_MIN_AVG];
            }
            else if (gubun == 2)
            {
                targetMat = channels[DataParser.CHANNEL_MEAN_DEV];
            }
            else if (gubun == 3)
            {
                Mat value1 = channels[DataParser.CHANNEL_MAX_AVG];
                Mat value2 = channels[DataParser.CHANNEL_MEAN_DEV];

                targetMat = (value1 + value2) / 2;
            }
            else if (gubun == 4)
            {
                Mat value1 = channels[DataParser.CHANNEL_MAX_AVG];
                Mat value2 = channels[DataParser.CHANNEL_MEAN_DEV];

                Cv2.Sqrt(value1.Mul(value2), targetMat);
            }
            else if (gubun == 5)
            {
                targetMat = channels[DataParser.CHANNEL_LUMPER_MAX];
            }
            else if (gubun == 6)
            {
                targetMat = channels[DataParser.CHANNEL_LUMPER_AVG];
            }

            double minVal, maxVal;

            int orginHeight = targetMat.Height;

            if (ctlHistLegend1.IsCustomMinMaxValue)
            {
                targetMat = targetMat.Threshold(ctlHistLegend1.MaxVal, ctlHistLegend1.MaxVal, ThresholdTypes.Trunc);
                targetMat = targetMat.Threshold(ctlHistLegend1.MinVal, ctlHistLegend1.MinVal, ThresholdTypes.Tozero);

                //CustomValue를 사용하는경우
                //Colormap을 적용했을때 색상 일관성을 위해
                //맨아래 한줄을 추가하고 MaxValue값을 지정해둔다
                targetMat = targetMat.Resize(new OpenCvSharp.Size(targetMat.Width, targetMat.Height + 1));
                targetMat[new Rect(0, orginHeight, targetMat.Width, 1)].SetTo(new Scalar(ctlHistLegend1.MaxVal));
            }

            targetMat.Normalize(255, 0, NormTypes.MinMax);
            targetMat.ConvertTo(origin, MatType.CV_32FC1);

            Cv2.MinMaxLoc(targetMat, out minVal, out maxVal);

            if (minVal == 0 && maxVal == 0)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
                return;
            }

            ctlHistLegend1.MaxVal = maxVal;
            ctlHistLegend1.MinVal = minVal;

            Mat hist = new Mat();
            int[] histSize = { 100 };

            minVal = Math.Min(ctlHistLegend1.MinVal, ctlHistLegend1.MaxVal);
            maxVal = Math.Max(ctlHistLegend1.MinVal, ctlHistLegend1.MaxVal);

            Rangef[] ranges = { new Rangef((float)minVal, (float)maxVal + 0.001f) };

            Cv2.CalcHist(new Mat[] { origin }, new int[] { 0 }, null, hist, 1, histSize, ranges);

            ctlHistLegend1.HistMat = hist;

            ctlHistLegend1.Invalidate();

            Mat target = new Mat();

            targetMat.Normalize(255, 0, NormTypes.MinMax).ConvertTo(origin, MatType.CV_8U);

            Cv2.ApplyColorMap(origin, target, ColormapTypes.Jet);

            target = target[new Rect(0, 0, target.Width, orginHeight)].Resize(new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height), 0, 0, InterpolationFlags.Area);

            selectedMat = target.Clone();
            pictureBox1.Image = target.ToBitmap();
        }

        private void LoadPinHeight()
        {
            pinmirrorWidthValueComboBox.Items.Clear();

            string qry = $"SELECT pin_s2, pin_e2, pin_g2 FROM letinar_uniform.uniform_master where idx = {SelectedMaster}";

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                var reader = cmd.ExecuteReader();

                reader.Read();

                double s = reader.GetDouble(0);
                double e = reader.GetDouble(1);
                double g = reader.GetDouble(2);

                if (g == 0)
                {
                    pinmirrorWidthValueComboBox.Items.Add($"+{s}");
                }
                else
                {
                    for (double v = s; v <= e; v += g)
                    {
                        pinmirrorWidthValueComboBox.Items.Add($"+{v}");
                    }
                }
            }

            if (pinmirrorWidthValueComboBox.Items.Count > 0) pinmirrorWidthValueComboBox.SelectedIndex = 0;
        }

        private void pinmirrorWidthValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
            LoadChart();

            //if (!lastClickedPoint.IsEmpty)
            //{
            //    ShowDetail(MouseButtons.Left);
            //}
        }

        Mat selectedMat = null;

        private static string GET_DETAIL_DATA = "SELECT * FROM uniform_detail WHERE master_idx = @selectedMaster AND idx = @detailIdx";
        //private static string GET_MASTER_DATA = "SELECT * FROM uniform_master WHERE idx = @selectedMaster";

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point lastClickedPoint = e.Location;

            if (baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || mirrorShapeComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "";
                return;
            }

            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            if (m == null)
            {
                toolStripStatusLabel1.Text = "";
                return;
            }

            Mat[] channels = m.Split();

            double widthValue = (double)channels.First().Width / pictureBox1.ClientRectangle.Width;
            double heightValue = (double)channels.First().Height / pictureBox1.ClientRectangle.Height;

            double xLoc = lastClickedPoint.X * widthValue;
            double yLoc = lastClickedPoint.Y * heightValue;


            DrawBoxAndInfo(m, channels, xLoc, yLoc);
        }

        private void DrawBoxAndInfo(Mat m, Mat[] channels, double xLoc, double yLoc)
        {
            int detailIdx = (int)channels[DataParser.CHANNEL_DETAIL_IDX].At<double>((int)yLoc, (int)xLoc);
            double MaxAvg = channels[DataParser.CHANNEL_MAX_AVG].At<double>((int)yLoc, (int)xLoc);
            double MinAvg = channels[DataParser.CHANNEL_MIN_AVG].At<double>((int)yLoc, (int)xLoc);
            double MeanDev = channels[DataParser.CHANNEL_MEAN_DEV].At<double>((int)yLoc, (int)xLoc);
            double LumDegreeMax = channels[DataParser.CHANNEL_LUMPER_MAX].At<double>((int)yLoc, (int)xLoc);
            double LumDegreeAvg = channels[DataParser.CHANNEL_LUMPER_AVG].At<double>((int)yLoc, (int)xLoc);

            double widthValue = (double)m.Width / pictureBox1.ClientRectangle.Width;
            double heightValue = (double)m.Height / pictureBox1.ClientRectangle.Height;

            DetailInfo detailInfo = GetDetailInfo(SelectedMaster, detailIdx);


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


            double pinmirrorGap = UniformityCalculator2.ImageManager.Instance.GetPinMirrorGap(detailInfo.Light, detailInfo.pinMirrorSize.Height);

            DrawSelectedRect(m, (int)xLoc, (int)yLoc);

            toolStripStatusLabel1.Text = $"광효율:{detailInfo.Light}% 동공크기:{detailInfo.Pupil}mm 핀미러크기:{detailInfo.pinMirrorSize}mm  핀미러간격:{pinmirrorGap}mm Max-Avg:{MaxAvg:0.0000} Min-Avg:{MinAvg:0.0000} 표준편차:{MeanDev:0.0000} 각도당휘도(Max):{LumDegreeMax:0.0000} 각도당휘도(Avg):{LumDegreeAvg:0.0000}";
        }

        private void DrawSelectedRect(Mat m, int xLoc, int yLoc)
        {
            if (selectedMat == null) return;

            Mat tmpMat = selectedMat.Clone();

            double widthValue = (double)m.Width / pictureBox1.ClientRectangle.Width;
            double heightValue = (double)m.Height / pictureBox1.ClientRectangle.Height;

            tmpMat.Line(new Point(0, ((int)yLoc) / heightValue), new Point(((int)xLoc + 1) / widthValue, ((int)yLoc) / heightValue), Scalar.Red, 2);
            tmpMat.Line(new Point(0, ((int)yLoc + 1) / heightValue), new Point(((int)xLoc + 1) / widthValue, ((int)yLoc + 1) / heightValue), Scalar.Red, 2);

            tmpMat.Line(new Point(((int)xLoc) / widthValue, ((int)yLoc) / heightValue), new Point(((int)xLoc) / widthValue, pictureBox1.Height), Scalar.Red, 2);
            tmpMat.Line(new Point(((int)xLoc + 1) / widthValue, ((int)yLoc) / heightValue), new Point(((int)xLoc + 1) / widthValue, pictureBox1.Height), Scalar.Red, 2);

            pictureBox1.Image = tmpMat.ToBitmap();
            tmpMat.Dispose();
        }

        private Tuple<int, double> GetSelectedPinLinesAndInnerPercent(int master)
        {

            var con = GetConnection();

            int lines = -1;
            double innerPercent = 0;

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER_ONE, con))
            {
                cmd.Parameters.AddWithValue("@idx", master);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lines = reader.GetInt32(2);
                    innerPercent = reader.GetDouble(16);
                    break;
                }
            }

            return new Tuple<int, double>(lines, innerPercent);
        }

        DetailInfo tmpContextData;

        private void ShowDetail(MouseButtons button)
        {
            ShowDetail(LastSelectedCoord.X, LastSelectedCoord.Y, button);
        }
        private void GetXYLocation(System.Drawing.Point clicked, out double xLoc, out double yLoc)
        {
            Mat m = parser.GetChart(baseItemComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));

            if (m == null) throw new Exception("GetXYLocation 오류");

            Mat[] channels = m.Split();

            double widthValue = (double)channels.First().Width / pictureBox1.ClientRectangle.Width;
            double heightValue = (double)channels.First().Height / pictureBox1.ClientRectangle.Height;

            xLoc = clicked.X * widthValue;
            yLoc = clicked.Y * heightValue;
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

            if(xLoc < 0 || xLoc >= m.Width || yLoc < 0 || yLoc >= m.Height)
            {
                return;
            }
            int detailIdx = (int)channels[DataParser.CHANNEL_DETAIL_IDX].At<double>(yLoc, xLoc);
            LastSelectedCoord = new System.Drawing.Point(xLoc, yLoc);

            DetailInfo detailInfo = GetDetailInfo(SelectedMaster, detailIdx);

            if (detailInfo.IsFilled == false) return;


            if (button == MouseButtons.Right)
            {
                int lines = GetSelectedPinLinesAndInnerPercent(SelectedMaster).Item1;

                if (lines == -1)
                {
                    MessageBox.Show("오류!!");
                    return;
                }

                tmpContextData = detailInfo;
                contextMenuStrip2.Show(pictureBox1, lastClickedPoint);
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
                    resultViewer.LoadResultData(detailInfo, GetSelectedPinLinesAndInnerPercent(SelectedMaster), mirrorShapeComboBox.SelectedIndex == 0 ? "Circle" : mirrorShapeComboBox.SelectedIndex == 1 ? "Circle_Circle" : mirrorShapeComboBox.SelectedIndex == 2 ? "Hexa" : "Hexa_Circle");
                    resultViewer.Show();
                }
                else
                {
                    if (frmDataViewer.HasInstance)
                    {
                        resultViewer = frmDataViewer.GetInstance();
                        resultViewer.LoadResultData(detailInfo, GetSelectedPinLinesAndInnerPercent(SelectedMaster), mirrorShapeComboBox.SelectedIndex == 0 ? "Circle" : mirrorShapeComboBox.SelectedIndex == 1 ? "Circle_Circle" : mirrorShapeComboBox.SelectedIndex == 2 ? "Hexa" : "Hexa_Circle");
                        resultViewer.Show();
                    }
                }
            }

        }

        private System.Drawing.Point LastSelectedCoord = System.Drawing.Point.Empty;
        private System.Drawing.Point lastClickedPoint = System.Drawing.Point.Empty;

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //lastClickedPoint = e.Location;
            double xLoc, yLoc;
            GetXYLocation(e.Location, out xLoc, out yLoc);
            LastSelectedCoord = new System.Drawing.Point((int)xLoc, (int)yLoc);
            ShowDetail(e.Button);
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        private Window mViewer = null;
        private Window mViewer2 = null;
        public Window Viewer
        {
            get
            {
                if (mViewer == null)
                {
                    mViewer = new Window("Original Data", WindowMode.KeepRatio);
                }

                return mViewer;
            }
        }

        private ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
        private void SetupContextMenu()
        {
            contextMenuStrip1.Items.Add("CSV Export");
            contextMenuStrip1.Items[0].Click += CSVExport_Click;
        }

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

        public Window Viewer2
        {
            get
            {
                if (mViewer2 == null)
                {
                    mViewer2 = new Window("Mirror Image", WindowMode.KeepRatio);
                }

                return mViewer2;
            }
        }

        public struct DetailInfo
        {
            public int Idx;
            public int MasterIdx;
            public double Light;
            public double Pupil;
            public SizeF pinMirrorSize;
            public double maxAvg;
            public double minAvg;
            public double meanDev;
            public double lumperdegree;
            public double lumperdegree_Avg;
            public int ShapeType;
            public DetailInfo(MySqlDataReader reader)
            {
                Idx = reader.GetInt32(0);
                MasterIdx = reader.GetInt32(1);
                Light = reader.GetDouble(2);
                Pupil = reader.GetDouble(3);
                pinMirrorSize = new SizeF((float)reader.GetDouble(11), (float)reader.GetDouble(4));
                maxAvg = reader.GetDouble(5);
                minAvg = reader.GetDouble(6);
                meanDev = reader.GetDouble(7);
                lumperdegree = reader.GetDouble(8);
                lumperdegree_Avg = reader.GetDouble(9);
                ShapeType = reader.GetInt32(10);

                IsFilled = true;
            }
            public readonly bool IsFilled;
        }

        private DetailInfo GetDetailInfo(int selectedMaster, int detailIdx)
        {
            var con = GetConnection();
            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_DATA, con))
            {
                cmd.Parameters.AddWithValue("@selectedMaster", selectedMaster);
                cmd.Parameters.AddWithValue("@detailIdx", detailIdx);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //light, pupil, pinWidth, pinHeight, shapetype
                    return new DetailInfo(reader);
                }
            }

            return new DetailInfo();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            baseValueComboBox_SelectedIndexChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;

            parser.ResetData();

            if (parser.SetData(SelectedMaster, mirrorShapeComboBox.SelectedIndex))
            {
                //comboBox1.SelectedIndex = -1;
                if (baseValueComboBox.SelectedIndex >= 0) baseValueComboBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                pictureBox1.Image = null;
            }
            groupBox1.Enabled = true;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTester.Instance.Show();
        }

        private void goTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tmpContextData.IsFilled == false) return;
            frmTester.Instance.SetData(tmpContextData.ShapeType, tmpContextData.Light, GetSelectedPinLinesAndInnerPercent(tmpContextData.MasterIdx).Item1, tmpContextData.Pupil, tmpContextData.pinMirrorSize);
            frmTester.Instance.Show();
        }

        private void viewer를새창으로ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = !item.Checked;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
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
                DrawBoxAndInfo(m, m.Split(), xLoc, yLoc);
                //DrawSelectedRect(m, xLoc, yLoc);
            }
            else
            {
                return;
            }
        }
    }
}
