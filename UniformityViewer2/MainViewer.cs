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

namespace UniformityViewer2
{
    public partial class MainViewer : Form
    {
        DBMaster master = new DBMaster();
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
        }

        private void CtlHistLegend1_OnMinMaxValueChanged()
        {
            throw new NotImplementedException();
        }

        private void MirrorShapeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
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
            if (mirrorShapeComboBox.SelectedIndex == -1 || baseItemComboBox.SelectedIndex == -1 || baseValueComboBox.SelectedIndex == -1 || pinmirrorWidthValueComboBox.SelectedIndex == -1) return;
            Mat m = parser.GetChart(mirrorShapeComboBox.SelectedIndex, double.Parse(baseValueComboBox.Text), double.Parse(pinmirrorWidthValueComboBox.Text.Replace("+", "")));
            SelectedMat = chartRenderer.LoadChart(m, GetSelectedRadioIndex(), pictureBox1);
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

        Mat SelectedMat = null;

        
    }
}
