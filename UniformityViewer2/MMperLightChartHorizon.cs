using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2
{
    public class MMperLightChartHorizon : MMperLightChartRenderer
    {
        public MMperLightChartHorizon(Chart chart, Mat lightImage) : base(chart, lightImage, LineView.LineType.Horizon) { }

        protected override int GetLength()
        {
            return mLightImage.Cols;
        }

        protected override Series GetSeriesData(Series data, int searchPos)
        {
            for (int col = 0; col < mLightImage.Cols; col++)
            {
                data.Points.AddXY(col * UniformityCalculator2.Data.CalcValues.MMperPixel, mLightImage.Get<byte>(searchPos, col));
            }

            return data;
        }
    }
}
