using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2
{
    public class DBMtfchart : DBManager
    {
        public void DrawChart(Chart targetChart, double mtfdistance, double mtfradius, string mtfType)
        {
            var con = GetConnection();

            var targetSeries = targetChart.Series["MTF_" + mtfType];
            targetSeries.Points.Clear();

            string qry = $"SELECT CPD, value FROM uniform_mtfchart WHERE mtf_d = {Math.Truncate(mtfdistance * 10) / 10} AND mtf_r = {Math.Truncate(mtfradius * 10) / 10} AND mtftype = '{mtfType}' ORDER BY CPD";

            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    targetSeries.Points.AddXY(reader.GetDouble(0), reader.GetDouble(1));
                }
            }

        }

        public void DrawChart(Chart chartWidth, SizeF pinmirrorSize, double pinmirrorGap)
        {
            DrawChart(chartWidth, pinmirrorGap, pinmirrorSize.Width, "S");
            DrawChart(chartWidth, pinmirrorGap, pinmirrorSize.Height, "T");
            //drawChart(chartHeight, pinmirrorGap, pinmirrorSize.Height, "S");
            //drawChart(chartHeight, pinmirrorGap, pinmirrorSize.Height, "T");
        }
    }
}
