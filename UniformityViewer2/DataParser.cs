using MySql.Data.MySqlClient;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2
{
    public class DataParser : DBManager
    {
        private static string GET_DETAIL_DATA = "SELECT * FROM uniform_detail WHERE master_idx = @idx AND shapetype = @shapetype ORDER BY (pinWidth - pinHeight), idx";

        //private List<ResultData> datas = new List<ResultData>();

        public bool SetData(int masterIdx, int shapeType)
        {
            if (masterIdx == -1) return false;

            bool ret = false;

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_DATA, con))
            {
                cmd.Parameters.AddWithValue("@idx", masterIdx);
                cmd.Parameters.AddWithValue("@shapetype", shapeType);
                var reader = cmd.ExecuteReader();

                double lastPinWidth = -1;

                List<ResultData> datas = new List<ResultData>();

                while (reader.Read())
                {
                    ret = true;

                    int detail = reader.GetInt32(0);
                    int master = reader.GetInt32(1);
                    double light = reader.GetDouble(2);
                    double pupil = reader.GetDouble(3);
                    double pinmrHeight = reader.GetDouble(4);
                    double maxavg = reader.GetDouble(5);
                    double minavg = reader.GetDouble(6);
                    double meandev = reader.GetDouble(7);
                    double lumdegreeMax = reader.GetDouble(8);
                    double lumdegreeAvg = reader.GetDouble(9);
                    int shapetype = reader.GetInt32(10);
                    double pinmrWidth = reader.GetDouble(11);

                    ResultData data = new ResultData(detail, master);
                    data.Light = light;
                    data.Pupil = pupil;
                    data.PinMrWidthDiff = (double)((decimal)pinmrWidth - (decimal)pinmrHeight);
                    data.PinMrHeight = pinmrHeight;
                    data.Maxavg = maxavg;
                    data.Minavg = minavg;
                    data.Meandev = meandev;
                    data.LumDegreeMax = lumdegreeMax;
                    data.LumDegreeAvg = lumdegreeAvg;
                    data.ShapeType = shapetype;

                    if (lastPinWidth == -1)
                    {
                        lastPinWidth = data.PinMrWidthDiff;
                    }
                    else if (lastPinWidth != data.PinMrWidthDiff)
                    {
                        ProcessMat(datas, lastPinWidth);
                        lastPinWidth = data.PinMrWidthDiff;
                        datas.Clear();
                    }

                    datas.Add(data);
                }
                if (datas.Count > 0) ProcessMat(datas, lastPinWidth);
            }

            return ret;
            //ParseBitmap();
        }

        private void ProcessMat(List<ResultData> datas, double width)
        {
            //for (int i = 0; i < 4; i++)
            {
                var lightList =
                    from data in datas
                    group data by data.Light into groupValue
                    orderby groupValue.Key ascending
                    select groupValue.Key;

                var pupilList =
                    from data in datas
                    group data by data.Pupil into groupValue
                    orderby groupValue.Key ascending
                    select groupValue.Key;

                var pinmrList =
                    from data in datas
                    group data by data.PinMrHeight into groupValue
                    orderby groupValue.Key ascending
                    select groupValue.Key;

                if (!DataDictionary[IDX_LIGHT/* + (i * 3)*/].ContainsKey(width)) DataDictionary[IDX_LIGHT/* + (i * 3)*/].Add(width, new Dictionary<double, Mat>());
                if (!DataDictionary[IDX_PUPIL/* + (i * 3)*/].ContainsKey(width)) DataDictionary[IDX_PUPIL/* + (i * 3)*/].Add(width, new Dictionary<double, Mat>());
                if (!DataDictionary[IDX_PINMR/* + (i * 3)*/].ContainsKey(width)) DataDictionary[IDX_PINMR/* + (i * 3)*/].Add(width, new Dictionary<double, Mat>());


                //각각 갯수에 해당하는 Mat를 가져야 한다

                //lightList

                foreach (double v in lightList)
                {
                    //가로축 pupil , 세로축 pinmr
                    var yList =
                        from data in datas
                        where data.Light == v
                        group data by data.PinMrHeight into pinValue
                        orderby pinValue.Key descending
                        select pinValue;

                    DataDictionary[IDX_LIGHT][width].Add(v, CreateMat(yList));
                }

                foreach (double v in pupilList)
                {
                    //가로축 light , 세로축 pinmr
                    var yList =
                        from data in datas
                        where data.Pupil == v// && data.ShapeType == i//수정
                        group data by data.PinMrHeight into pinValue
                        orderby pinValue.Key descending
                        select pinValue;

                    DataDictionary[IDX_PUPIL][width].Add(v, CreateMat(yList));
                }

                foreach (double v in pinmrList)
                {
                    //가로축 light , 세로축 pupil
                    var yList =
                        from data in datas
                        where data.PinMrHeight == v //&& data.ShapeType == i
                        group data by data.Pupil into pinValue
                        orderby pinValue.Key descending
                        select pinValue;

                    DataDictionary[IDX_PINMR][width].Add(v, CreateMat(yList));
                }

            }

        }

        private Mat CreateMat(IOrderedEnumerable<IGrouping<double, ResultData>> yList)
        {

            int idx = 0;

            double[][] matDataMaxAvg = new double[yList.Count()][];
            double[][] matDataMinAvg = new double[yList.Count()][];
            double[][] matDataMeanDev = new double[yList.Count()][];
            double[][] matDataLumDegreeMax = new double[yList.Count()][];
            double[][] matDataLumDegreeAvg = new double[yList.Count()][];

            double[][] detailData = new double[yList.Count()][];

            foreach (var xList in yList)
            {

                matDataMaxAvg[idx] = xList.OrderBy(x => x.Light).Select(x => x.Maxavg).ToArray();
                matDataMinAvg[idx] = xList.OrderBy(x => x.Light).Select(x => x.Minavg).ToArray();
                matDataMeanDev[idx] = xList.OrderBy(x => x.Light).Select(x => x.Meandev).ToArray();
                matDataLumDegreeMax[idx] = xList.OrderBy(x => x.Light).Select(x => x.LumDegreeMax).ToArray();
                matDataLumDegreeAvg[idx] = xList.OrderBy(x => x.Light).Select(x => x.LumDegreeAvg).ToArray();

                detailData[idx] = xList.OrderBy(x => x.Light).Select(x => (double)x.DetailIdx).ToArray();
                idx++;
            }

            Mat matMaxAvg = new Mat(matDataMaxAvg.Length, matDataMaxAvg[0].Length, MatType.CV_64F, matDataMaxAvg.SelectMany(x => x).ToArray());
            Mat matMinAvg = new Mat(matDataMinAvg.Length, matDataMinAvg[0].Length, MatType.CV_64F, matDataMinAvg.SelectMany(x => x).ToArray());
            Mat matMeanDev = new Mat(matDataMeanDev.Length, matDataMeanDev[0].Length, MatType.CV_64F, matDataMeanDev.SelectMany(x => x).ToArray());
            Mat matLumDegreeMax = new Mat(matDataLumDegreeMax.Length, matDataLumDegreeMax[0].Length, MatType.CV_64F, matDataLumDegreeMax.SelectMany(x => x).ToArray());
            Mat matLumDegreeAvg = new Mat(matDataLumDegreeAvg.Length, matDataLumDegreeAvg[0].Length, MatType.CV_64F, matDataLumDegreeAvg.SelectMany(x => x).ToArray());
            Mat matDetail = new Mat(detailData.Length, detailData[0].Length, MatType.CV_64F, detailData.SelectMany(x => x).ToArray());

            Mat total = new Mat(matDataMaxAvg.Length, matDataMaxAvg[0].Length, MatType.CV_64FC4);

            Cv2.Merge(new Mat[] { matMaxAvg, matMinAvg, matMeanDev, matLumDegreeMax, matLumDegreeAvg, matDetail }, total);

            return total;
        }

        public static int CHANNEL_MAX_AVG = 0;
        public static int CHANNEL_MIN_AVG = 1;
        public static int CHANNEL_MEAN_DEV = 2;
        public static int CHANNEL_LUMPER_MAX = 3;
        public static int CHANNEL_LUMPER_AVG = 4;
        public static int CHANNEL_DETAIL_IDX = 5;

        //GetChart의 gubun과 맞아야 한다
        private static int IDX_LIGHT = 0;
        private static int IDX_PUPIL = 1;
        private static int IDX_PINMR = 2;

        //IDX Count 만큼 존재해야 함

        Dictionary<double, Dictionary<double, Mat>>[] DataDictionary = new Dictionary<double, Dictionary<double, Mat>>[3]
        {
            new Dictionary<double, Dictionary<double, Mat>>(),
            new Dictionary<double, Dictionary<double, Mat>>(),
            new Dictionary<double, Dictionary<double, Mat>>()
        };

        /// <summary>
        /// Check Nullable !!!
        /// </summary>
        /// <param name="gubun"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        public Mat GetChart(int gubun, double baseValue, double pinWidthGap)
        {
            Dictionary<double, Mat> databag = null;

            if (gubun > IDX_PINMR)
            {
                return null;
            }
            else
            {
                if (!DataDictionary[gubun].ContainsKey(pinWidthGap)) DataDictionary[gubun].Add(pinWidthGap, new Dictionary<double, Mat>());
                //if (DataDictionary[gubun][pinHeight] == null) DataDictionary[gubun][pinHeight] = new Dictionary<double, Mat>();
                databag = DataDictionary[gubun][pinWidthGap];
            }

            if (databag == null) return null;
            if (!databag.ContainsKey(baseValue)) return null;

            return databag[baseValue];
        }


        private class ResultData
        {
            public int DetailIdx { get; private set; }
            public int MasterIdx { get; private set; }
            public double Light { get; set; } = 0;
            public double Pupil { get; set; } = 0;
            public double PinMrWidthDiff { get; set; } = 0;
            public double PinMrHeight { get; set; } = 0;
            public int ShapeType { get; set; } = 0;
            public double Meandev { get; set; } = 0;
            public double Maxavg { get; set; } = 0;
            public double Minavg { get; set; } = 0;
            public double LumDegreeMax { get; set; } = 0;
            public double LumDegreeAvg { get; set; } = 0;

            public ResultData(int detail, int master)
            {
                this.MasterIdx = master;
                this.DetailIdx = detail;
            }
        }

        internal void ResetData()
        {
            DataDictionary = new Dictionary<double, Dictionary<double, Mat>>[3]
        {
            new Dictionary<double, Dictionary<double, Mat>>(),
            new Dictionary<double, Dictionary<double, Mat>>(),
            new Dictionary<double, Dictionary<double, Mat>>()
        };
        }
    }
}
