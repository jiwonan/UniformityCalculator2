using OpenCvSharp;
using System;
using UniformityCalculator2;

namespace UniformityViewer2
{
    public class ImageProcessor
    {
        private static ImageProcessor mInstance;

        public static ImageProcessor Instance {
            get {
                if(mInstance == null)
                {
                    mInstance = new ImageProcessor();
                }
                return mInstance;
            }
        }

        public Mat GetResultMat(ImageManager.ProcessImageResult ret, double eyeRelief, double horizonFov, double verticalFov)
        {
            Mat resultMat = CreateAlphaChannel(ret.Result, (double)eyeRelief, (double)horizonFov, (double)verticalFov);

            double multi = GetMulti(resultMat);

            return resultMat.Resize(new Size(resultMat.Width * multi, resultMat.Height * multi));
        }

        public Mat GetMirrorMat(ImageManager.ProcessImageResult ret)
        {
            Mat mirrorMat = ret.MirrorImage;

            double multi = GetMulti(mirrorMat);
            return mirrorMat.Resize(new Size(mirrorMat.Width * multi, mirrorMat.Height * multi));
        }

        public Mat GetImageMat(Mat resultMat, string filepath)
        {
            Mat img = Cv2.ImRead(filepath, ImreadModes.Color);
            img = img.Resize(new Size(resultMat.Width, resultMat.Height));

            Mat m = resultMat.ExtractChannel(0);

            Mat[] channels = img.Split();
            for (int i = 0; i < channels.Length; i++)
            {
                Mat target = new Mat();

                channels[i].ConvertTo(target, MatType.CV_64FC1);

                target = target.Mul(m).ToMat();

                target.ConvertTo(target, MatType.CV_8UC1);

                channels[i] = target;
            }

            Cv2.Merge(channels, img);

            return img;
        }
        private double GetMulti(Mat m)
        {
            double multi;

            if (m.Width > m.Height)
            {
                multi = 600d / m.Width;
            }
            else if (m.Height > m.Width)
            {
                multi = 600d / m.Height;
            }
            else
            {
                multi = 600d / m.Width;
            }

            return multi;
        }
        private Mat CreateAlphaChannel(Mat samplingMat, double eyeRelief, double h_fov, double v_fov)
        {
            //이거는 결과화면으로 나갈 Mat의 크기다
            double TotalWidth = 2 * (Math.Tan((h_fov / 2) * Math.PI / 180) * eyeRelief) / CalcValues.MMperPixel;
            double TotalHeight = 2 * (Math.Tan((v_fov / 2) * Math.PI / 180) * eyeRelief) / CalcValues.MMperPixel;

            //ret보다 크면서 가로/세로가 samplingMat의 홀수배인 Mat를 만든다
            int widthMultiplier = (int)(TotalWidth / samplingMat.Width) + 1;
            if (widthMultiplier % 2 == 0) widthMultiplier++;

            int heightMultiplier = (int)(TotalHeight / samplingMat.Height) + 1;
            if (heightMultiplier % 2 == 0) heightMultiplier++;

            using (Mat tmp = new Mat(samplingMat.Height * heightMultiplier, samplingMat.Width * widthMultiplier, samplingMat.Type()))
            {

                Rect r = new Rect(0, 0, samplingMat.Width, samplingMat.Height);
                for (int row = 0; row < heightMultiplier; row++)
                {
                    r.Y = samplingMat.Height * row;
                    for (int col = 0; col < widthMultiplier; col++)
                    {
                        r.X = samplingMat.Width * col;
                        Cv2.CopyTo(samplingMat, tmp[r]);
                    }
                }

                r = new Rect((int)((tmp.Width - TotalWidth) / 2), (int)((tmp.Height - TotalHeight) / 2), (int)TotalWidth, (int)TotalHeight);

                return tmp[r].Clone();
            }
        }

    }
}
