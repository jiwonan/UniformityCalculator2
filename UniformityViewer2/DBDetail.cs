using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2
{
    public class DBDetail : DBManager
    {
        private static string GET_DETAIL_DATA = "SELECT * FROM uniform_detail WHERE master_idx = @selectedMaster AND idx = @detailIdx";

        public DetailInfo GetDetailInfo(int selectedMaster, int detailIdx)
        {
            var con = GetConnection();
            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_DATA, con))
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
