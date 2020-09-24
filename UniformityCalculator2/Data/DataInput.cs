namespace UniformityCalculator2.Data
{
    public struct DataInput
    {
        /// <summary>
        /// 광효율
        /// </summary>
        public double LightEffi { get; set; }
        /// <summary>
        /// 동공사이즈
        /// </summary>
        public double PupilSize { get; set; }
        /// <summary>
        /// 핀미러세로
        /// </summary>
        public double PinHeightStart { get; set; }
        public double PinHeightEnd { get; set; }
        public double PinHeightGap { get; set; }
        /// <summary>
        /// 핀미러가로
        /// </summary>
        public double PinWidthStart { get; set; }
        public double PinWidthEnd { get; set; }
        public double PinWidthGap { get; set; }
        /// <summary>
        /// 핀미러형상
        /// </summary>
        public PinMirrorShape MirrorShape { get; set; }
        /// <summary>
        /// 핀미러 내부공간비율(%), mirrorShape이 _Circle인경우에 한함
        /// </summary>
        public double InnerArea { get; set; }

        public int PinLines { get; set; }
        public DataInput(int master, double lightEffi, double pupilSize
            , double pinHeightStart, double pinHeightEnd, double pinHeightGap
            , double pinWidthStart, double pinWidthEnd, double pinWidthGap
            , PinMirrorShape shape, double inner, int lines)
        {
            this.LightEffi = lightEffi;
            this.PupilSize = pupilSize;
            this.PinHeightStart = pinHeightStart;
            this.PinHeightEnd = pinHeightEnd;
            this.PinHeightGap = pinHeightGap;
            this.PinWidthStart = pinWidthStart;
            this.PinWidthEnd = pinWidthEnd;
            this.PinWidthGap = pinWidthGap;
            this.MirrorShape = shape;
            this.InnerArea = inner;
            this.MasterIdx = master;
            this.PinLines = lines;

            this.MaxAvg = 0;
            this.MinAvg = 0;
            this.MeanDev = 0;
            this.LumperDegree = 0;
            this.LumperDegree_Avg = 0;
        }

        public int MasterIdx { get; set; }

        public double MaxAvg { get; set; }
        public double MinAvg { get; set; }
        public double MeanDev { get; set; }
        public double LumperDegree { get; set; }
        public double LumperDegree_Avg { get; set; }
    }
    public enum PinMirrorShape
    {
        Circle,
        Circle_Circle,
        Hexa,
        Hexa_Circle
    }
}
