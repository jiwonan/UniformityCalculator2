using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniformityViewer2
{
    public class DBMaster : DBManager
    {
        private static string SELECT_MASTER = "SELECT * FROM uniform_master WHERE worktype = 1 ORDER BY idx desc";
        private static string SELECT_MASTER_ONE = "SELECT * FROM uniform_master WHERE idx = @idx ORDER BY idx";
        public void LoadMasterData(ListView listView)
        {
            listView.Items.Clear();

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER, con))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idx = reader.GetInt32(0);
                    DateTime dtm = reader.GetDateTime(1);
                    int pinline = reader.GetInt32(2);
                    double light_s = reader.GetDouble(3);
                    double light_e = reader.GetDouble(4);
                    double light_g = reader.GetDouble(5);
                    double pupil_s = reader.GetDouble(6);
                    double pupil_e = reader.GetDouble(7);
                    double pupil_g = reader.GetDouble(8);
                    double pin_s = reader.GetDouble(9);
                    double pin_e = reader.GetDouble(10);
                    double pin_g = reader.GetDouble(11);

                    ListViewItem item = listView.Items.Add(idx.ToString(), idx.ToString(), 0);
                    item.SubItems.Add(dtm.ToString());
                    item.SubItems.Add(pinline.ToString());
                    item.SubItems.Add(light_s.ToString());
                    item.SubItems.Add(light_e.ToString());
                    item.SubItems.Add(light_g.ToString());
                    item.SubItems.Add(pupil_s.ToString());
                    item.SubItems.Add(pupil_e.ToString());
                    item.SubItems.Add(pupil_g.ToString());
                    item.SubItems.Add(pin_s.ToString());
                    item.SubItems.Add(pin_e.ToString());
                    item.SubItems.Add(pin_g.ToString());

                }
            }
        }

        public void LoadPinHeight(ComboBox comboBox, int SelectedMaster)
        {
            comboBox.Items.Clear();

            string qry = $"SELECT pin_s2, pin_e2, pin_g2 FROM letinar_uniform.uniform_master where idx = {SelectedMaster}";

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                var reader = cmd.ExecuteReader();

                reader.Read();

                double s = reader.GetDouble(0);
                double e = reader.GetDouble(1);
                double g = reader.GetDouble(2);

                if (g == 0)
                {
                    comboBox.Items.Add($"+{s}");
                }
                else
                {
                    for (double v = s; v <= e; v += g)
                    {
                        comboBox.Items.Add($"+{v}");
                    }
                }
            }

            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
            con.Close();
        }

        public void CallValueList(int selectedIndex, int SelectedMaster, ComboBox comboBox)
        {
            if (SelectedMaster == -1) return;

            var con = GetConnection();

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER_ONE, con))
            {
                cmd.Parameters.AddWithValue("@idx", SelectedMaster);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idx = reader.GetInt32(0);
                    DateTime dtm = reader.GetDateTime(1);
                    int pinline = reader.GetInt32(2);
                    double light_s = reader.GetDouble(3);
                    double light_e = reader.GetDouble(4);
                    double light_g = reader.GetDouble(5);
                    double pupil_s = reader.GetDouble(6);
                    double pupil_e = reader.GetDouble(7);
                    double pupil_g = reader.GetDouble(8);
                    double pin_s = reader.GetDouble(9);
                    double pin_e = reader.GetDouble(10);
                    double pin_g = reader.GetDouble(11);

                    double s = 0, e = 0, g = 0;

                    switch (selectedIndex)
                    {
                        case 0: //광효율
                            s = light_s;
                            e = light_e;
                            g = light_g;
                            break;
                        case 1: //동공사이즈
                            s = pupil_s;
                            e = pupil_e;
                            g = pupil_g;
                            break;
                        case 2: //핀미러직경
                            s = pin_s;
                            e = pin_e;
                            g = pin_g;
                            break;
                    }

                    for (decimal v = (decimal)s; v <= (decimal)e; v += (decimal)g)
                    {
                        comboBox.Items.Add(v.ToString());
                    }
                }


            }
        }

        public Tuple<int, double> GetSelectedPinLinesAndInnerPercent(int master)
        {

            var con = GetConnection();

            int lines = -1;
            double innerPercent = 0;

            using (MySqlCommand cmd = new MySqlCommand(SELECT_MASTER_ONE, con))
            {
                cmd.Parameters.AddWithValue("@idx", master);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lines = reader.GetInt32(2);
                    innerPercent = reader.GetDouble(16);
                    break;
                }
            }

            return new Tuple<int, double>(lines, innerPercent);
        }
    }
}
