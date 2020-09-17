namespace UniformityCalculator2.Data
{
    public static class MasterInputValue
    {
        public static double LightEfficiencyStart;
        public static double LightEfficiencyEnd;
        public static double LightEfficiencyGap;

        public static double PupilSizeStart;
        public static double PupilSizeEnd;
        public static double PupilSizeGap;

        public static double PinMirrorWidthStart;
        public static double PinMirrorWidthEnd;
        public static double PinMirrorWidthGap;

        public static double PinMirrorHeightStart;
        public static double PinMirrorHeightEnd;
        public static double PinMirrorHeightGap;

        public static int PinmirrorLines;
        public static double InnerPercent;

        public static void SetValues(double lightEfficiencyStart, double lightEfficiencyEnd, double lightEfficiencyGap
            , double pupilSizeStart, double pupilSizeEnd, double pupilSizeGap
            , double pinMirrorWidthStart, double pinMirrorWidthEnd, double pinMirrorWidthGap
            , double pinMirrorHeightStart, double pinMirrorHeightEnd, double pinMirrorHeightGap
            , int pinmirrorLines, double innerPercent)
        {
            LightEfficiencyStart = lightEfficiencyStart;
            LightEfficiencyEnd = lightEfficiencyEnd;
            LightEfficiencyGap = lightEfficiencyGap;

            PupilSizeStart = pupilSizeStart;
            PupilSizeEnd = pupilSizeEnd;
            PupilSizeGap = pupilSizeGap;

            PinMirrorWidthStart = pinMirrorWidthStart;
            PinMirrorWidthEnd = pinMirrorWidthEnd;
            PinMirrorWidthGap = pinMirrorWidthGap;

            PinMirrorHeightStart = pinMirrorHeightStart;
            PinMirrorHeightEnd = pinMirrorHeightEnd;
            PinMirrorHeightGap = pinMirrorHeightGap;

            PinmirrorLines = pinmirrorLines;
            InnerPercent = innerPercent;

        }
    }
}
