using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using UniformityViewer2.Controls.Lines;

namespace UniformityViewer2.Charting
{
    public class MMperLightChartVertical : MMperLightChartRenderer
    {
        public MMperLightChartVertical(Chart chart, Mat lightImage) : base(chart, lightImage, LineView.LineType.Vertical) { }

        protected override IEnumerable<string> GetDetailData(int searchPos)
        {
            yield return $"Vertical Pos : {searchPos * UniformityCalculator2.Data.CalcValues.MMperPixel}mm";
            for (int row = 0; row < mLightImage.Rows; row++)
            {
                yield return $"{row * UniformityCalculator2.Data.CalcValues.MMperPixel} : {mLightImage.Get<byte>(row, searchPos)}";
            }
            yield break;
        }

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
