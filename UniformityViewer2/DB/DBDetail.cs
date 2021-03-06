﻿using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Drawing;

namespace UniformityViewer2.DB
{
    public class DBDetail : UniformityCalculator2.DB.DBManager
    {
        private static string GET_DETAIL_INFO = "SELECT * FROM uniform_detail WHERE master_idx = @selectedMaster AND idx = @detailIdx";

        public DetailInfo GetDetailInfo(int selectedMaster, int detailIdx)
        {
            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_INFO, con))
            {
                cmd.Parameters.AddWithValue("@selectedMaster", selectedMaster);
                cmd.Parameters.AddWithValue("@detailIdx", detailIdx);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //light, pupil, pinWidth, pinHeight, shapetype
                    return new DetailInfo(reader);
                }
            }

            return new DetailInfo();
        }

        private static string GET_DETAIL_DATA = "SELECT * FROM uniform_detail WHERE master_idx = @idx AND shapetype = @shapetype ORDER BY (pinWidth - pinHeight), idx";

        public IEnumerable<DataParser.ResultData> GetDetailData(int masterIdx, int shapeType)
        {
            var con = GetConnection();

            MySqlDataReader reader;
            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_DATA, con))
            {
                cmd.Parameters.AddWithValue("@idx", masterIdx);
                cmd.Parameters.AddWithValue("@shapetype", shapeType);
                reader = cmd.ExecuteReader();
            }

            while (reader.Read())
            {
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

                DataParser.ResultData data = new DataParser.ResultData(detail, master);
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

                yield return data;
            }
            yield break;
        }
    }


    public struct DetailInfo
    {
        public int Idx;
        public int MasterIdx;
        public double Light;
        public double Pupil;
        public SizeF pinMirrorSize;
        public double maxAvg;
        public double minAvg;
        public double meanDev;
        public double lumperdegree;
        public double lumperdegree_Avg;
        public int ShapeType;

        public DetailInfo(MySqlDataReader reader)
        {
            Idx = reader.GetInt32(0);
            MasterIdx = reader.GetInt32(1);
            Light = reader.GetDouble(2);
            Pupil = reader.GetDouble(3);
            pinMirrorSize = new SizeF((float)reader.GetDouble(11), (float)reader.GetDouble(4));
            maxAvg = reader.GetDouble(5);
            minAvg = reader.GetDouble(6);
            meanDev = reader.GetDouble(7);
            lumperdegree = reader.GetDouble(8);
            lumperdegree_Avg = reader.GetDouble(9);
            ShapeType = reader.GetInt32(10);

            IsFilled = true;
        }
        public readonly bool IsFilled;
    }
}
