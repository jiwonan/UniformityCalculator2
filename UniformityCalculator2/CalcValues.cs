namespace UniformityCalculator2
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
    }
}
