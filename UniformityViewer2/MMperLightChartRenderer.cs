using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2
{
    public abstract class MMperLightChartRenderer : ChartRenderer
    {
        public MMperLightChartRenderer(Chart chart, Mat lightImage, LineView.LineType lineType) : base(chart, lightImage, lineType) { }

        // public override int AxisInterval { get; set; } = 15;
        protected override void SetChartMinMax()
        {
            double minVal, maxVal;

            mLightImage.MinMaxLoc(out minVal, out maxVal);
            minVal = Math.Round(minVal);
            maxVal = Math.Round(maxVal);

            int length = GetLength();

            mChart.ChartAreas["Draw"].AxisX.Minimum = 0;
            mChart.ChartAreas["Draw"].AxisX.Maximum = length * UniformityCalculator2.Data.CalcValues.MMperPixel;

            mChart.ChartAreas["Draw"].AxisY.Minimum = minVal - 10;
            mChart.ChartAreas["Draw"].AxisY.Maximum = maxVal + 10;
        }
    }
}
