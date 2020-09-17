using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2.Data
{
    public static class CalcValues
    {
        public const double MMperPixel = 0.009992; //픽셀크기

        public static int MMtoPixel(double mm)
        {
            return (int)(mm / MMperPixel);
        }

        public static double PixeltoMM(int pixel)
        {
            return pixel * MMperPixel;
        }

        public static double GetPinMirrorGap(double lightEffi, SizeF pinmrSize) // CalcValues
        {
            return GetPinMirrorGap(lightEffi, pinmrSize.Height);
        }

        public static double GetPinMirrorGap(double lightEffi, double pinmrHeight)
        {
            return (pinmrHeight / 2) * Math.Sqrt((2 / Math.Sqrt(3) * (Math.PI / (lightEffi / 100))));
        }

        //lightEffi는 퍼센트
        //2 / Math.Sqrt(3) * (Math.PI / (lightEffi / 100))
        public static double GetLightEffi(double pinmrSize, double pinmrGap)
        {
            return Math.PI / ((Math.Sqrt(3) / 2) * Math.Pow(pinmrGap / (pinmrSize / 2), 2)) * 100;
        }
    }
}
