using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2
{
    public static class PsfManager
    {
        private const int DATA_SIZE = 820;

        private static Mat psfData { get; set; } = null;

        private static void LoadPsfData()
        {
            double[,] UniformatyData = new double[DATA_SIZE, DATA_SIZE];

            using (StreamReader reader = new StreamReader("psfdata.csv"))
            {
                int row = 0;

                while (!reader.EndOfStream)
                {
                    double[] arr = reader.ReadLine().Split(',').Select(double.Parse).ToArray();
                    Buffer.BlockCopy(arr, 0, UniformatyData, (row * DATA_SIZE) * sizeof(double), arr.Length * sizeof(double));
                    row++;
                }
            }
            psfData = new Mat(DATA_SIZE, DATA_SIZE, MatType.CV_64F, UniformatyData);
        }

        public static Mat LoadPsfData(double pupilSize)
        {
            int PupilSizeInPixel = (int)(pupilSize / Data.CalcValues.MMperPixel);

            if (PupilSizeInPixel > DATA_SIZE) PupilSizeInPixel = DATA_SIZE;

            using (Mat ret = new Mat(DATA_SIZE, DATA_SIZE, MatType.CV_64F))
            using (Mat mask = new Mat(DATA_SIZE, DATA_SIZE, MatType.CV_8U))
            {
                ret.SetTo(Scalar.Black);
                mask.SetTo(Scalar.Black);
                mask.Ellipse(new Point(DATA_SIZE / 2, DATA_SIZE / 2), new OpenCvSharp.Size(PupilSizeInPixel / 2, PupilSizeInPixel / 2), 0, 0, 360, Scalar.White, -1);

                if (psfData == null) LoadPsfData();

                psfData.CopyTo(ret, mask);

                return ret.Clone();
            }
        }
    }
}
