using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2
{
    public class MMperLightChartVertical : MMperLightChartRenderer
    {
        public MMperLightChartVertical(Chart chart, Mat lightImage) : base(chart, lightImage, LineView.LineType.Vertical) { }

        protected override int GetLength()
        {
            return mLightImage.Rows;
        }

        protected override Series GetSeriesData(Series data, int searchPos)
        {
            for (int row = 0; row < mLightImage.Rows; row++)
            {
                data.Points.AddXY(row * UniformityCalculator2.Data.CalcValues.MMperPixel, mLightImage.Get<byte>(row, searchPos));
            }

            return data;
        }
    }
}
