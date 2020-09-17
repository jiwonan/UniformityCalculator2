using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace UniformityCalculator2.DB
{
    public class DBMaster : DBManager
    {
        private static string INSERT_MASTER = "INSERT INTO uniform_master VALUES (NULL, now(), @pinlines, @light_s, @light_e, @light_g, @pupil_s, @pupil_e, @pupil_g, @pin_s, @pin_e, @pin_g, @pin_s2, @pin_e2, @pin_g2, 0, @innerPercent, @usetype);";
        private static string LAST_INSERT_ID = "SELECT LAST_INSERT_ID(); ";
        private static string FINISH_MASTER = "UPDATE uniform_master SET worktype = 1 WHERE idx = @idx";

        public int CreateMaster(int userType)
        {
            try
            {
                using (var con = GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(INSERT_MASTER, con))
                    {
                        cmd.Parameters.AddWithValue("@pinlines", Data.MasterInputValue.PinmirrorLines);
                        cmd.Parameters.AddWithValue("@light_s", Data.MasterInputValue.LightEfficiencyStart);
                        cmd.Parameters.AddWithValue("@light_e", Data.MasterInputValue.LightEfficiencyEnd);
                        cmd.Parameters.AddWithValue("@light_g", Data.MasterInputValue.LightEfficiencyGap);
                        cmd.Parameters.AddWithValue("@pupil_s", Data.MasterInputValue.PupilSizeStart);
                        cmd.Parameters.AddWithValue("@pupil_e", Data.MasterInputValue.PupilSizeEnd);
                        cmd.Parameters.AddWithValue("@pupil_g", Data.MasterInputValue.PupilSizeGap);
                        cmd.Parameters.AddWithValue("@pin_s", Data.MasterInputValue.PinMirrorHeightStart);
                        cmd.Parameters.AddWithValue("@pin_e", Data.MasterInputValue.PinMirrorHeightEnd);
                        cmd.Parameters.AddWithValue("@pin_g", Data.MasterInputValue.PinMirrorHeightGap);
                        cmd.Parameters.AddWithValue("@pin_s2", Data.MasterInputValue.PinMirrorWidthStart);
                        cmd.Parameters.AddWithValue("@pin_e2", Data.MasterInputValue.PinMirrorWidthEnd);
                        cmd.Parameters.AddWithValue("@pin_g2", Data.MasterInputValue.PinMirrorWidthGap);
                        cmd.Parameters.AddWithValue("@innerPercent", Data.MasterInputValue.InnerPercent);
                        cmd.Parameters.AddWithValue("@usetype", userType);

                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(LAST_INSERT_ID, con))
                    {
                        return int.Parse(cmd.ExecuteScalar().ToString()); // idx반환.
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                LogManager.SetLog(err.Message);
            }
            return -1;
        }


        public void FinishMaster(int idx)
        {
            try
            {
                using (var con = new MySqlConnection(SERVER_PATH))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(FINISH_MASTER, con))
                    {
                        cmd.Parameters.AddWithValue("@idx", idx);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                LogManager.SetLog(err.Message);
            }
        }

        private static string DELETE_MASTER = "DELETE FROM uniform_master WHERE idx = @idx;";
        private static string ALTER_INDEX_MASTER = "ALTER TABLE uniform_master AUTO_INCREMENT = @idx;";

        public void DeleteMaster(int idx)
        {
            try
            {
                using (var con = new MySqlConnection(SERVER_PATH))
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand(DELETE_MASTER, con))
                    {
                        cmd.Parameters.AddWithValue("@idx", idx);
                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(ALTER_INDEX_MASTER, con))
                    {
                        cmd.Parameters.AddWithValue("@idx", idx);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                LogManager.SetLog(e.Message);
            }
        }
    }
}
