using AuraScreen.Magnification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace AuraScreen
{
    //Big thanks to NegativeScreen
    public static class ColorMatrix
    {
        public static float[,] Identity { get; }
        public static float[,] Negative { get; }

        public static float[,] GrayScale { get; }
        public static float[,] Sepia { get; }
        public static float[,] Red { get; }
        public static float[,] HueShift180 { get; }

        public static float[,] NegativeGrayScale { get; }
        public static float[,] NegativeSepia { get; }
        public static float[,] NegativeRed { get; }
        public static float[,] NegativeHueShift180 { get;  }

        static ColorMatrix()
        {
            Identity = new float[,] {
                {  1.0f,  0.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  1.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  1.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
            };
            Negative = new float[,] {
                { -1.0f,  0.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f, -1.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f, -1.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
                {  1.0f,  1.0f,  1.0f,  0.0f,  1.0f }
            };
            GrayScale = new float[,] {
                {  0.3f,  0.3f,  0.3f,  0.0f,  0.0f },
                {  0.6f,  0.6f,  0.6f,  0.0f,  0.0f },
                {  0.1f,  0.1f,  0.1f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
            };
            NegativeGrayScale = Multiply(Negative, GrayScale);
            Red = new float[,] {
                {  1.0f,  0.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
                {  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
            };
            HueShift180 = new float[,] {
                { -0.3333333f,  0.6666667f,  0.6666667f, 0.0f, 0.0f },
                {  0.6666667f, -0.3333333f,  0.6666667f, 0.0f, 0.0f },
                {  0.6666667f,  0.6666667f, -0.3333333f, 0.0f, 0.0f },
                {  0.0f,              0.0f,        0.0f, 1.0f, 0.0f },
                {  0.0f,              0.0f,        0.0f, 0.0f, 1.0f }
            };
            Red = Multiply(GrayScale, Red);
            NegativeRed = Multiply(NegativeGrayScale, Red);
            Sepia = new float[,] {
                { .393f, .349f, .272f, 0.0f, 0.0f},
                { .769f, .686f, .534f, 0.0f, 0.0f},
                { .189f, .168f, .131f, 0.0f, 0.0f},
                {  0.0f,  0.0f,  0.0f, 1.0f, 0.0f},
                {  0.0f,  0.0f,  0.0f, 0.0f, 1.0f}
            };
            NegativeSepia = Multiply(Negative, Sepia);
            NegativeHueShift180 = Multiply(Negative, HueShift180);
        }

        public static Dictionary<string, float[,]> Matrix = new Dictionary<string, float[,]> {
            { "None", ColorMatrix.Identity },
            { "Negative", ColorMatrix.Negative },
            { "Negative Greyscale", ColorMatrix.NegativeGrayScale },
            { "Negative Hue Shift 180", ColorMatrix.NegativeHueShift180 },
            { "Negative Red", ColorMatrix.NegativeRed },
            { "Negative Sepia", ColorMatrix.NegativeSepia },
            { "Sepia", ColorMatrix.Sepia },
            { "Red", ColorMatrix.Red },
            { "Greyscale", ColorMatrix.GrayScale },
            { "Hue Shift 180", ColorMatrix.HueShift180 }
        };

        public static bool ChangeColorEffect(float[,] matrix)
        {
            ColorEffect colorEffect = new ColorEffect(matrix);
            NativeMethods.MagInitialize();
            NativeMethods.MagSetFullscreenColorEffect(ref colorEffect);
            return true;
        }

        public static float[,] Multiply(float[,] a, float[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
            {
                throw new Exception("a.GetLength(1) != b.GetLength(0)");
            }
            float[,] c = new float[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    for (int k = 0; k < a.GetLength(1); k++) // k<b.GetLength(0)
                    {
                        c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }
            }
            return c;
        }

        public static Bitmap Transform(Bitmap source, float[,] Matrix)
        {
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);
            Graphics g = Graphics.FromImage(newBitmap);
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(ToJaggedArray(Matrix));
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        public static float[][] ToJaggedArray(float[,] Matrix)
        {
            float[][] newArray = new float[][] { };

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; i < 5; i++)
                {
                    newArray[i][k] = Matrix[i, k];
                }
            }

            return newArray;
        }
    }
}