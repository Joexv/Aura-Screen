using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace MouseHover
{
    using static NativeMethods;

    class Filters
    {
        public bool FilterApplied = false;

		public void Test2()
        {
			SetFilter(BuiltinMatrices.NegativeGrayScale);
        }
        public void SetFilter(float[] ColorEffect)
        {
            if (FilterApplied)
                MagUninitialize();
			var magEffectInvert = new MAGCOLOREFFECT
			{
				transform = ColorEffect
			};
			MagInitialize();
            MagSetFullscreenColorEffect(ref magEffectInvert);
            FilterApplied = true;
        }

		// Copyright 2011-2017 Melvyn Laïly
		// https://zerowidthjoiner.net

		// This code is part of NegativeScreen.

		// This program is free software: you can redistribute it and/or modify
		// it under the terms of the GNU General Public License as published by
		// the Free Software Foundation, either version 3 of the License, or
		// (at your option) any later version.

		// This program is distributed in the hope that it will be useful,
		// but WITHOUT ANY WARRANTY; without even the implied warranty of
		// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
		// GNU General Public License for more details.

		// You should have received a copy of the GNU General Public License
		// along with this program.  If not, see <http://www.gnu.org/licenses/>.
		/// <summary>
		/// Store various built in ColorMatrix
		/// </summary>
		public static class BuiltinMatrices
		{
			/// <summary>
			/// no color transformation
			/// </summary>
			public static float[] Identity { get; }
			/// <summary>
			/// simple colors transformations
			/// </summary>
			public static float[] Negative { get; }
			public static float[] GrayScale { get; }
			public static float[] Sepia { get; }
			public static float[] Red { get; }
			public static float[] HueShift180 { get; }

			public static float[] NegativeGrayScale { get; }
			public static float[] NegativeSepia { get; }
			public static float[] NegativeRed { get; }

			/// <summary>
			/// theoretical optimal transfomation (but ugly desaturated pure colors due to "overflows"...)
			/// Many thanks to Tom MacLeod who gave me the idea for these inversion modes
			/// </summary>
			public static float[] NegativeHueShift180 { get; }
			/// <summary>
			/// high saturation, good pure colors
			/// </summary>
			public static float[] NegativeHueShift180Variation1 { get; }
			/// <summary>
			/// overall desaturated, yellows and blue plain bad. actually relaxing and very usable
			/// </summary>
			public static float[] NegativeHueShift180Variation2 { get; }
			/// <summary>
			/// high saturation. yellows and blues plain bad. actually quite readable
			/// </summary>
			public static float[] NegativeHueShift180Variation3 { get; }
			/// <summary>
			/// not so readable, good colors (CMY colors a bit desaturated, still more saturated than normal)
			/// </summary>
			public static float[] NegativeHueShift180Variation4 { get; }

			static BuiltinMatrices()
			{
				Identity = new float[] {
				  1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  1.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  1.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  0.0f,  1.0f
			};
				Negative = new float[] {
				 -1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f, -1.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f, -1.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
				  1.0f,  1.0f,  1.0f,  0.0f,  1.0f
			};
				GrayScale = new float[] {
				  0.3f,  0.3f,  0.3f,  0.0f,  0.0f ,
				  0.6f,  0.6f,  0.6f,  0.0f,  0.0f ,
				  0.1f,  0.1f,  0.1f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  0.0f,  1.0f
			};
				NegativeGrayScale = Multiply(Negative, GrayScale);
				Red = new float[] {
				  1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
				  0.0f,  0.0f,  0.0f,  0.0f,  1.0f
			};
				Red = Multiply(GrayScale, Red);
				NegativeRed = Multiply(NegativeGrayScale, Red);
				Sepia = new float[] {
				 .393f, .349f, .272f, 0.0f, 0.0f,
				 .769f, .686f, .534f, 0.0f, 0.0f,
				 .189f, .168f, .131f, 0.0f, 0.0f,
				  0.0f,  0.0f,  0.0f, 1.0f, 0.0f,
				  0.0f,  0.0f,  0.0f, 0.0f, 1.0f
			};
				NegativeSepia = Multiply(Negative, Sepia);
				HueShift180 = new float[] {
				 -0.3333333f,  0.6666667f,  0.6666667f, 0.0f, 0.0f ,
				  0.6666667f, -0.3333333f,  0.6666667f, 0.0f, 0.0f ,
				  0.6666667f,  0.6666667f, -0.3333333f, 0.0f, 0.0f ,
				  0.0f,              0.0f,        0.0f, 1.0f, 0.0f ,
				  0.0f,              0.0f,        0.0f, 0.0f, 1.0f
			};
				NegativeHueShift180 = Multiply(Negative, HueShift180);
				NegativeHueShift180Variation1 = new float[] {
				// most simple working method for shifting hue 180deg.
				  1.0f, -1.0f, -1.0f, 0.0f, 0.0f ,
				 -1.0f,  1.0f, -1.0f, 0.0f, 0.0f ,
				 -1.0f, -1.0f,  1.0f, 0.0f, 0.0f ,
				  0.0f,  0.0f,  0.0f, 1.0f, 0.0f ,
				  1.0f,  1.0f,  1.0f, 0.0f, 1.0f
			};
				NegativeHueShift180Variation2 = new float[] {
				// generated with QColorMatrix http://www.codeguru.com/Cpp/G-M/gdi/gdi/article.php/c3667
				  0.39f, -0.62f, -0.62f, 0.0f, 0.0f ,
				 -1.21f, -0.22f, -1.22f, 0.0f, 0.0f ,
				 -0.16f, -0.16f,  0.84f, 0.0f, 0.0f ,
				   0.0f,   0.0f,   0.0f, 1.0f, 0.0f ,
				   1.0f,   1.0f,   1.0f, 0.0f, 1.0f
			};
				NegativeHueShift180Variation3 = new float[] {
					 1.089508f,   -0.9326327f, -0.932633042f,  0.0f,  0.0f ,
				  -1.81771779f,    0.1683074f,  -1.84169245f,  0.0f,  0.0f ,
				 -0.244589478f, -0.247815639f,    1.7621845f,  0.0f,  0.0f ,
						  0.0f,          0.0f,          0.0f,  1.0f,  0.0f ,
						  1.0f,          1.0f,          1.0f,  0.0f,  1.0f
			};
				NegativeHueShift180Variation4 = new float[] {
				  0.50f, -0.78f, -0.78f, 0.0f, 0.0f ,
				 -0.56f,  0.72f, -0.56f, 0.0f, 0.0f ,
				 -0.94f, -0.94f,  0.34f, 0.0f, 0.0f ,
				   0.0f,   0.0f,   0.0f, 1.0f, 0.0f ,
				   1.0f,   1.0f,   1.0f, 0.0f, 1.0f
			};
			}

			public static float[] Multiply(float[] a, float[] b)
			{
				float[] c = new float[a.Length];
				for(int i = 0; i < a.Length; i++)
                {
					c[i] = a[i] * b[i];
                }
				Console.WriteLine($"Input 1 Length{a.Length}, Output Length{c.Length}");
				return c;
			}
		}


		[Serializable]
		public class CannotChangeColorEffectException : Exception
		{
			public CannotChangeColorEffectException() { }
			public CannotChangeColorEffectException(string message) : base(message) { }
			public CannotChangeColorEffectException(string message, Exception inner) : base(message, inner) { }
			protected CannotChangeColorEffectException(
			  System.Runtime.Serialization.SerializationInfo info,
			  System.Runtime.Serialization.StreamingContext context) : base(info, context)
			{ }
		}
	}



	static class NativeMethods
    {
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }
    }
}

