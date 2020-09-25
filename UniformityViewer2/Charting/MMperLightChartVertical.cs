﻿using OpenCvSharp;
using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2.Charting
{
    public class MMperLightChartVertical : MMperLightChartRenderer
    {
        public MMperLightChartVertical(Chart chart, Mat lightImage) : base(chart, lightImage, Controls.Lines.LineView.LineType.Vertical) { }

        protected override void GetDetailData(int searchPos, Controls.Lines.LineView.LineType type)
        {
            if (type == mLineType)
            {
                for (int row = 0; row < mLightImage.Rows; row++)
                {
                    Console.WriteLine($"{row * UniformityCalculator2.Data.CalcValues.MMperPixel} : {mLightImage.Get<byte>(row, searchPos)}");
                }
            }
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
