using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using UniformityViewer2.Controls.Lines;

namespace UniformityViewer2.Charting
{
    public class MMperLightChartHorizon : MMperLightChartRenderer
    {
        public MMperLightChartHorizon(Chart chart, Mat lightImage) : base(chart, lightImage, LineView.LineType.Horizon) { }

        protected override IEnumerable<string> GetDetailData(int searchPos)
        {
            yield return $"Horizon Pos : {searchPos * UniformityCalculator2.Data.CalcValues.MMperPixel}mm";
            for (int col = 0; col < mLightImage.Cols; col++)
            {
                yield return $"{col * UniformityCalculator2.Data.CalcValues.MMperPixel}mm : {mLightImage.Get<byte>(searchPos, col)}";
            }
            yield break;
        }

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
