using OpenCvSharp;
using System;
using System.Drawing;
using Point = OpenCvSharp.Point;

namespace UniformityCalculator2.Image
{
    public static class KernelManager
    {
        private static Data.PinMirrorShape GetShapeType(int selectedIndex)
        {
            Data.PinMirrorShape type = Data.PinMirrorShape.Circle;

            switch (selectedIndex)
            {
                case 1:
                    type = Data.PinMirrorShape.Circle;
                    break;
                case 2:
                    type = Data.PinMirrorShape.Circle_Circle;
                    break;
                case 3:
                    type = Data.PinMirrorShape.Hexa;
                    break;
                case 4:
                    type = Data.PinMirrorShape.Hexa_Circle;
                    break;
            }

            return type;
        }

        public static Mat GetKernel(SizeF pinmirrorSize, decimal innerPercent, int type)
        {
            return GetKernel((decimal)pinmirrorSize.Height, (decimal)pinmirrorSize.Width, innerPercent, GetShapeType(type));
        }

        public static Mat GetKernel(decimal pinmirrorWidth, decimal pinmirrorHeight, decimal innerArea, int type)
        {
            return GetKernel(pinmirrorWidth, pinmirrorHeight, innerArea, GetShapeType(type));
        }

        public static Mat GetKernel(decimal pinmirrorWidth, decimal pinmirrorHeight, decimal innerArea, Data.PinMirrorShape kernelType)
        {

            double baseArea = Math.PI * Math.Pow(((double)pinmirrorHeight / 2), 2);

            decimal inner = pinmirrorHeight / 2 * innerArea / 100;

            Mat kernel = null;

            switch (kernelType)
            {
                case Data.PinMirrorShape.Circle:
                    kernel = CreateKernelByArea(Data.PinMirrorShape.Circle, baseArea, Data.PinMirrorShape.Circle, 0);
                    break;
                case Data.PinMirrorShape.Circle_Circle:
                    kernel = CreateKernelByArea(Data.PinMirrorShape.Circle, baseArea, Data.PinMirrorShape.Circle, (double)inner);
                    break;
                case Data.PinMirrorShape.Hexa:
                    kernel = CreateKernelByArea(Data.PinMirrorShape.Hexa, baseArea, Data.PinMirrorShape.Circle, 0);
                    break;
                case Data.PinMirrorShape.Hexa_Circle:
                    kernel = CreateKernelByArea(Data.PinMirrorShape.Hexa, baseArea, Data.PinMirrorShape.Circle, (double)inner);
                    break;
            }

            kernel = kernel.Resize(new OpenCvSharp.Size(Data.CalcValues.MMtoPixel((double)pinmirrorWidth), Data.CalcValues.MMtoPixel((double)pinmirrorHeight)));

            return kernel;
        }
        private static Mat CreateKernelByArea(Data.PinMirrorShape shapeType, double targetArea, Data.PinMirrorShape innerShape, double innerRadius = 0)
        {
            double innerArea = 0;
            if (shapeType == Data.PinMirrorShape.Circle)
            {
                innerArea = Math.PI * Math.Pow(innerRadius, 2);
            }
            else
            {
                innerArea = Math.Pow(innerRadius, 2) * Math.Sin(Math.PI / 3) * 3;
            }

            double totalArea = innerArea + targetArea;
            double outerRadius = 0;

            if (shapeType == Data.PinMirrorShape.Circle)
            {
                outerRadius = Math.Sqrt(totalArea / Math.PI);
            }
            else
            {
                outerRadius = Math.Sqrt(totalArea / 3 / Math.Sin(Math.PI / 3));
            }

            return CreateKernel(shapeType, outerRadius, innerShape, innerRadius);

        }

        /// <summary>
        /// 필터연산을 위한 미러 한개 형상
        /// </summary>
        /// <param name="shapeType">미러형상</param>
        /// <param name="radius">미러반지름 mm</param>
        /// <param name="innerRadius">내부반지름 mm</param>
        /// <returns></returns>
        private static Mat CreateKernel(Data.PinMirrorShape shapeType, double radius, Data.PinMirrorShape innerShape = Data.PinMirrorShape.Circle, double innerRadius = 0)
        {
            double radiusPixel = Data.CalcValues.MMtoPixel(radius);

            Mat m;

            if (shapeType == Data.PinMirrorShape.Circle)
            {
                m = new Mat((int)(radiusPixel * 2), (int)(radiusPixel * 2), MatType.CV_8U);

                m.SetTo(Scalar.Black);

                m.Circle(m.Width / 2, m.Height / 2, (int)radiusPixel, Scalar.White, -1);
            }
            else
            {
                double radiusPixelheight = radiusPixel * Math.Sqrt(3) / 2;

                m = new Mat((int)(radiusPixelheight * 2), (int)(radiusPixel * 2), MatType.CV_8U);

                m.SetTo(Scalar.Black);

                using (Mat rot = new Mat<double>(2, 2))
                {
                    Point[] pts = new Point[6];

                    for (int i = 0; i < 6; i++)
                    {
                        rot.Set(0, 0, Math.Cos(Math.PI / 3 * i)); rot.Set(0, 1, -Math.Sin(Math.PI / 3 * i));
                        rot.Set(1, 0, Math.Sin(Math.PI / 3 * i)); rot.Set(1, 1, Math.Cos(Math.PI / 3 * i));

                        Mat data = new Mat<double>(2, 1);
                        data.Set(0, 0, radiusPixel);
                        data.Set(1, 0, 0d);

                        data = rot * data;

                        pts[i] = new Point(data.At<double>(0, 0), data.At<double>(1, 0));

                        data.Dispose();
                    }
                    Cv2.FillPoly(m, new Point[][] { pts }, Scalar.White, LineTypes.Link8, 0, new Point(radiusPixel, radiusPixelheight));
                }
            }

            if (innerRadius == 0)
            {
                return m;
            }
            else
            {
                using (Mat subMat = CreateKernel(innerShape, innerRadius))
                {
                    Rect targetRect = new Rect(m.Width / 2 - subMat.Width / 2, m.Height / 2 - subMat.Height / 2, subMat.Width, subMat.Height);
                    Cv2.BitwiseNot(m[targetRect], m[targetRect], subMat);

                    return m;
                }
            }
        }

    }
}
