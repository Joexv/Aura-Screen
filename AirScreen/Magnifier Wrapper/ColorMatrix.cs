using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Magnifier
{
    //Credit to the creator of Negative screen for these default Matricies. Its a fucking life saver
    public static class Matrices
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

        public static float[,] NegativeHueShift180 { get; }
        public static float[,] NegativeHueShift180Variation1 { get; }
        public static float[,] NegativeHueShift180Variation2 { get; }
        public static float[,] NegativeHueShift180Variation3 { get; }
        public static float[,] NegativeHueShift180Variation4 { get; }

        static Matrices()
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
            HueShift180 = new float[,] {
                { -0.3333333f,  0.6666667f,  0.6666667f, 0.0f, 0.0f },
                {  0.6666667f, -0.3333333f,  0.6666667f, 0.0f, 0.0f },
                {  0.6666667f,  0.6666667f, -0.3333333f, 0.0f, 0.0f },
                {  0.0f,              0.0f,        0.0f, 1.0f, 0.0f },
                {  0.0f,              0.0f,        0.0f, 0.0f, 1.0f }
            };
            NegativeHueShift180 = Multiply(Negative, HueShift180);
            NegativeHueShift180Variation1 = new float[,] {
				{  1.0f, -1.0f, -1.0f, 0.0f, 0.0f },
                { -1.0f,  1.0f, -1.0f, 0.0f, 0.0f },
                { -1.0f, -1.0f,  1.0f, 0.0f, 0.0f },
                {  0.0f,  0.0f,  0.0f, 1.0f, 0.0f },
                {  1.0f,  1.0f,  1.0f, 0.0f, 1.0f }
            };
            NegativeHueShift180Variation2 = new float[,] {
				{  0.39f, -0.62f, -0.62f, 0.0f, 0.0f },
                { -1.21f, -0.22f, -1.22f, 0.0f, 0.0f },
                { -0.16f, -0.16f,  0.84f, 0.0f, 0.0f },
                {   0.0f,   0.0f,   0.0f, 1.0f, 0.0f },
                {   1.0f,   1.0f,   1.0f, 0.0f, 1.0f }
            };
            NegativeHueShift180Variation3 = new float[,] {
                {     1.089508f,   -0.9326327f, -0.932633042f,  0.0f,  0.0f },
                {  -1.81771779f,    0.1683074f,  -1.84169245f,  0.0f,  0.0f },
                { -0.244589478f, -0.247815639f,    1.7621845f,  0.0f,  0.0f },
                {          0.0f,          0.0f,          0.0f,  1.0f,  0.0f },
                {          1.0f,          1.0f,          1.0f,  0.0f,  1.0f }
            };
            NegativeHueShift180Variation4 = new float[,] {
                {  0.50f, -0.78f, -0.78f, 0.0f, 0.0f },
                { -0.56f,  0.72f, -0.56f, 0.0f, 0.0f },
                { -0.94f, -0.94f,  0.34f, 0.0f, 0.0f },
                {   0.0f,   0.0f,   0.0f, 1.0f, 0.0f },
                {   1.0f,   1.0f,   1.0f, 0.0f, 1.0f }
            };
        }

        public static string MatrixToString(float[,] matrix)
        {
            int maxDecimal = 0;
            foreach (var item in matrix)
            {
                string toString = item.ToString("0.#######", System.Globalization.NumberFormatInfo.InvariantInfo);
                int indexOfDot = toString.IndexOf('.');
                int currentMax = indexOfDot >= 0 ? toString.Length - indexOfDot - 1 : 0;
                if (currentMax > maxDecimal)
                {
                    maxDecimal = currentMax;
                }
            }
            string format = "0." + new string('0', maxDecimal);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                sb.Append("{ ");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] >= 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(matrix[i, j].ToString(format, System.Globalization.NumberFormatInfo.InvariantInfo));
                    if (j < matrix.GetLength(1) - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(" }\n");
            }
            return sb.ToString();
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
                    for (int k = 0; k < a.GetLength(1); k++)
                    {
                        c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }
            }
            return c;
        }

        public static bool ChangeColorEffect(float[,] matrix, bool FilterUsed = false)
        {
            NativeMethods.MagInitialize();
            ColorEffect colorEffect = new ColorEffect(matrix);
            NativeMethods.MagSetFullscreenColorEffect(ref colorEffect);
            return true;
        }

        public static void InterpolateColorEffect(float[,] fromMatrix, float[,] toMatrix, int timeBetweenFrames = 15)
        {
            List<float[,]> transitions = Interpolate(fromMatrix, toMatrix);
            foreach (float[,] item in transitions)
            {
                ChangeColorEffect(item);
                System.Threading.Thread.Sleep(timeBetweenFrames);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public static List<float[,]> Interpolate(float[,] A, float[,] B)
        {
            const int STEPS = 10;
            const int SIZE = 5;

            if (A.GetLength(0) != SIZE ||
                A.GetLength(1) != SIZE ||
                B.GetLength(0) != SIZE ||
                B.GetLength(1) != SIZE)
            {
                throw new ArgumentException();
            }

            List<float[,]> result = new List<float[,]>(STEPS);

            for (int i = 0; i < STEPS; i++)
            {
                result.Add(new float[SIZE, SIZE]);

                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        // f(x)=ya+(x-xa)*(yb-ya)/(xb-xa)
                        // calculate 10 steps, from 1 to 10 (we don't need 0, as we start from there)
                        result[i][x, y] = A[x, y] + (i + 1/*-0*/) * (B[x, y] - A[x, y]) / (STEPS/*-0*/);
                    }
                }
            }

            return result;
        }

        public static System.Drawing.Bitmap Transform(System.Drawing.Bitmap source, float[,] Matrix)
        {
            Console.WriteLine(MatrixToString(Matrix));
            System.Drawing.Bitmap newBitmap = new System.Drawing.Bitmap(source.Width, source.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBitmap);
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(Matrix.ToJaggedArray());
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(source, new System.Drawing.Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, System.Drawing.GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }
    }

    public static class NativeMethods
    {

        [DllImport("Magnification.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport("Magnification.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport("Magnification.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref ColorEffect pEffect);

        [DllImport("Magnification.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagSetColorEffect(IntPtr hwnd, ref ColorEffect pEffect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, [MarshalAs(UnmanagedType.Bool)] bool erase);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "CreateWindowExW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr CreateWindow(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string modName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("Magnification.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagSetWindowSource(IntPtr hwnd, RECT rect);

        [DllImport("Magnification.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagGetWindowSource(IntPtr hwnd, ref RECT pRect);

        [DllImport("Magnification.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagSetWindowTransform(IntPtr hwnd, ref Transformation pTransform);

        public const string WC_MAGNIFIER = "Magnifier";
        public static IntPtr HWND_TOPMOST = new IntPtr(-1);

        public const int USER_TIMER_MINIMUM = 0x0000000A;
        public const int SM_ARRANGE = 0x38;
        public const int SM_CLEANBOOT = 0x43;
        public const int SM_CMONITORS = 80;
        public const int SM_CMOUSEBUTTONS = 0x2b;
        public const int SM_CXBORDER = 5;
        public const int SM_CXCURSOR = 13;
        public const int SM_CXDOUBLECLK = 0x24;
        public const int SM_CXDRAG = 0x44;
        public const int SM_CXEDGE = 0x2d;
        public const int SM_CXFIXEDFRAME = 7;
        public const int SM_CXFOCUSBORDER = 0x53;
        public const int SM_CXFRAME = 0x20;
        public const int SM_CXHSCROLL = 0x15;
        public const int SM_CXHTHUMB = 10;
        public const int SM_CXICON = 11;
        public const int SM_CXICONSPACING = 0x26;
        public const int SM_CXMAXIMIZED = 0x3d;
        public const int SM_CXMAXTRACK = 0x3b;
        public const int SM_CXMENUCHECK = 0x47;
        public const int SM_CXMENUSIZE = 0x36;
        public const int SM_CXMIN = 0x1c;
        public const int SM_CXMINIMIZED = 0x39;
        public const int SM_CXMINSPACING = 0x2f;
        public const int SM_CXMINTRACK = 0x22;
        public const int SM_CXSCREEN = 0;
        public const int SM_CXSIZE = 30;
        public const int SM_CXSIZEFRAME = 0x20;
        public const int SM_CXSMICON = 0x31;
        public const int SM_CXSMSIZE = 0x34;
        public const int SM_CXVIRTUALSCREEN = 0x4e;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYBORDER = 6;
        public const int SM_CYCAPTION = 4;
        public const int SM_CYCURSOR = 14;
        public const int SM_CYDOUBLECLK = 0x25;
        public const int SM_CYDRAG = 0x45;
        public const int SM_CYEDGE = 0x2e;
        public const int SM_CYFIXEDFRAME = 8;
        public const int SM_CYFOCUSBORDER = 0x54;
        public const int SM_CYFRAME = 0x21;
        public const int SM_CYHSCROLL = 3;
        public const int SM_CYICON = 12;
        public const int SM_CYICONSPACING = 0x27;
        public const int SM_CYKANJIWINDOW = 0x12;
        public const int SM_CYMAXIMIZED = 0x3e;
        public const int SM_CYMAXTRACK = 60;
        public const int SM_CYMENU = 15;
        public const int SM_CYMENUCHECK = 0x48;
        public const int SM_CYMENUSIZE = 0x37;
        public const int SM_CYMIN = 0x1d;
        public const int SM_CYMINIMIZED = 0x3a;
        public const int SM_CYMINSPACING = 0x30;
        public const int SM_CYMINTRACK = 0x23;
        public const int SM_CYSCREEN = 1;
        public const int SM_CYSIZE = 0x1f;
        public const int SM_CYSIZEFRAME = 0x21;
        public const int SM_CYSMCAPTION = 0x33;
        public const int SM_CYSMICON = 50;
        public const int SM_CYSMSIZE = 0x35;
        public const int SM_CYVIRTUALSCREEN = 0x4f;
        public const int SM_CYVSCROLL = 20;
        public const int SM_CYVTHUMB = 9;
        public const int SM_DBCSENABLED = 0x2a;
        public const int SM_DEBUG = 0x16;
        public const int SM_MENUDROPALIGNMENT = 40;
        public const int SM_MIDEASTENABLED = 0x4a;
        public const int SM_MOUSEPRESENT = 0x13;
        public const int SM_MOUSEWHEELPRESENT = 0x4b;
        public const int SM_NETWORK = 0x3f;
        public const int SM_PENWINDOWS = 0x29;
        public const int SM_REMOTESESSION = 0x1000;
        public const int SM_SAMEDISPLAYFORMAT = 0x51;
        public const int SM_SECURE = 0x2c;
        public const int SM_SHOWSOUNDS = 70;
        public const int SM_SWAPBUTTON = 0x17;
        public const int SM_XVIRTUALSCREEN = 0x4c;
        public const int SM_YVIRTUALSCREEN = 0x4d;
    }

    internal static class ExtensionMethods
    {
        internal static T[][] ToJaggedArray<T>(this T[,] ddArray)
        {
            int rowsFirstIndex = ddArray.GetLowerBound(0);
            int rowsLastIndex = ddArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = ddArray.GetLowerBound(1);
            int columnsLastIndex = ddArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = ddArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}