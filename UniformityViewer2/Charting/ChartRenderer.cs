using OpenCvSharp;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using UniformityViewer2.Controls.Lines;

namespace UniformityViewer2.Charting
{
    public abstract class ChartRenderer
    {
        protected Mat mLightImage;
        protected Chart mChart;
        protected Controls.Lines.LineView.LineType mLineType;

        protected Dictionary<int, Series> seriesData = new Dictionary<int, Series>();

        public ChartRenderer(Chart chart, Mat lightImage, Controls.Lines.LineView.LineType lineType)
        {
            this.mChart = chart;
            this.mLightImage = lightImage;
            this.mLineType = lineType;
        }

        public void CreateChart()
        {
            mChart.ChartAreas.Clear();
            mChart.Series.Clear();

            mChart.ChartAreas.Add("Draw");

            SetChartMinMax();

            mChart.ChartAreas["Draw"].AxisX.Interval = 1;
            mChart.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            mChart.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            mChart.ChartAreas["Draw"].AxisY.Interval = 10;
            mChart.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            mChart.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        }

        public void DrawChart(object sender, int position, LineView.LineType type)
        {
            if (mLineType != type)
            {
                return;
            }

            int searchPos = CalcSearchPos(sender, position);

            if (!seriesData.ContainsKey(searchPos))
            {
                Series data;

                data = new Series();
                data.ChartType = SeriesChartType.Line;

                seriesData.Add(searchPos, GetSeriesData(data, searchPos));
            }

            mChart.Series.Clear();
            mChart.Series.Add(seriesData[searchPos]);
        }

        public void ClearChart()
        {
            mChart.Series.Clear();
            seriesData.Clear();
        }

        public string GetFileData(object sender, LineView.LineType type, int position)
        {
            if (type != mLineType) return null;

            StringBuilder sb = new StringBuilder();
            foreach (string data in GetDetailData(position))
            {
                sb.Append(data).Append("\r\n");
            }

            return sb.ToString();
        }

        protected abstract void SetChartMinMax();

        protected abstract Series GetSeriesData(Series data, int searchPos);

        protected abstract int GetLength();

        protected abstract IEnumerable<string> GetDetailData(int searchPos);

        public int CalcSearchPos(object sender, int position)
        {
            Rectangle rect = ((Controls.LinePictureBox)sender).GetImageRect();

            if (mLineType == LineView.LineType.Vertical)
            {
                int originalCols = mLightImage.Cols - 1;
                int resizedCols = rect.Width;

                return (int)((double)originalCols / resizedCols * (position - rect.X));
            }
            else
            {
                int originalRows = mLightImage.Rows - 1;
                int resizedRows = rect.Height;

                return (int)((double)originalRows / resizedRows * (position - rect.Y));
            }
        }
    }
}
