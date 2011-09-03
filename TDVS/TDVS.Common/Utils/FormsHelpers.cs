using System;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Utils
{
	public static class FormsHelpers
	{
		public static void TintBitmap( Bitmap bmp, System.Drawing.Color color, float percent )
		{
			float tH = 0, tS = 0, tV = 0;
			RGBtoHSV( ( float )color.R / 255, ( float )color.G / 255, ( float )color.B / 255, ref tH, ref tS, ref tV );
			for ( int i = 0; i < bmp.Width; i++ )
			{
				for ( int j = 0; j < bmp.Height; j++ )
				{
					System.Drawing.Color cc = bmp.GetPixel( i, j );
					int a = cc.A;
					float h = 0, s = 0, v = 0;
					RGBtoHSV( ( float )cc.R / 255, ( float )cc.G / 255, ( float )cc.B / 255, ref h, ref s, ref v );
					h = tH;

					float r = 0, g = 0, b = 0;
					HSVtoRGB( ref r, ref g, ref b, h, s, v );

					cc = System.Drawing.Color.FromArgb( a, ( int )( r * 255 ), ( int )( g * 255 ), ( int )( b * 255 ) );
					bmp.SetPixel( i, j, cc );
					/*
					if ( cc.A > 0 )
					{
						var red = Convert.ToInt32( ( ( color.R - cc.R ) * ( cc.R / 255 ) + cc.R ) );
						var blue = Convert.ToInt32( ( ( color.B - cc.B ) * ( cc.B / 255 ) + cc.B ) );
						var green = Convert.ToInt32( ( ( color.G - cc.G ) * ( cc.G / 255 ) + cc.G ) );
						bmp.SetPixel( i, j, System.Drawing.Color.FromArgb( cc.A, red, green, blue ) );
					}
					 * */
				}
			}
		}

		public static Vector4 RGBtoHSV( System.Drawing.Color color )
		{
			var res = new Vector4();
			res.W = ( float )color.A / 255;
			float r, g, b;
			r = ( float )color.R / 255;
			g = ( float )color.G / 255;
			b = ( float )color.B / 255;
			float min, max, delta;
			min = Math.Min( Math.Min( r, g ), b );
			max = Math.Max( Math.Max( r, g ), b );
			res.Z = max;				// v
			delta = max - min;

			res.Y = 0;
			if ( max != 0 )
				if ( delta == 0 )
					res.Y = 1;
				else
					res.Y = delta / max;		// s
			else
			{
				// r = g = b = 0		// s = 0, v is undefined
				res.Y = 0;
				res.X = -1;
				return res;
			}

			res.X = 0;
			if ( delta > 0 )
			{
				if ( r == max )
					res.X = ( g - b ) / delta;		// between yellow & magenta
				else if ( g == max )
					res.X = 2 + ( b - r ) / delta;	// between cyan & yellow
				else
					res.X = 4 + ( r - g ) / delta;	// between magenta & cyan
				res.X *= 60;				// degrees
				if ( res.X < 0 )
					res.X += 360;
			}

			return res;
		}
		public static System.Drawing.Color HSVtoRGB( Vector4 hsv )
		{
			var res = new System.Drawing.Color();
			int i;
			float f, p, q, t;
			if ( hsv.Y == 0 )
			{
				// achromatic (grey)
				int v = ( int )hsv.Z * 255;
				var c = System.Drawing.Color.FromArgb( ( int )hsv.W * 255, v, v, v );
				return res;
			}
			hsv.X /= 60;			// sector 0 to 5
			i = ( int )Math.Floor( hsv.X );
			f = hsv.X - i;			// factorial part of h
			p = hsv.Z * ( 1 - hsv.Y );
			q = hsv.Z * ( 1 - hsv.Y * f );
			t = hsv.Z * ( 1 - hsv.Y * ( 1 - f ) );
			switch ( i )
			{
				case 0:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )hsv.Z * 255, ( int )t * 255, ( int )p * 255 );
					break;
				case 1:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )q * 255, ( int )hsv.Z * 255, ( int )p * 255 );
					break;
				case 2:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )p * 255, ( int )hsv.Z * 255, ( int )t * 255 );
					break;
				case 3:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )p * 255, ( int )q * 255, ( int )hsv.Z * 255 );
					break;
				case 4:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )t * 255, ( int )p * 255, ( int )hsv.Z * 255 );
					break;
				default:		// case 5:
					res = System.Drawing.Color.FromArgb( ( int )hsv.W, ( int )hsv.Z * 255, ( int )p * 255, ( int )q * 255 );
					break;
			}

			return res;
		}
		public static void RGBtoHSV( float r, float g, float b, ref float h, ref float s, ref float v )
		{
			var min = Math.Min( Math.Min( r, g ), b );
			var max = Math.Max( Math.Max( r, g ), b );
			v = max;				// v
			var delta = max - min;

			s = 0;
			if ( max != 0 )
				if ( delta == 0 )
					s = 1;
				else
					s = delta / max;		// s
			else
			{
				// r = g = b = 0		// s = 0, v is undefined
				s = 0;
				h = -1;
				return;
			}
			h = 0;
			if ( delta > 0 )
			{
				if ( r == max )
					h = ( g - b ) / delta;		// between yellow & magenta
				else if ( g == max )
					h = 2 + ( b - r ) / delta;	// between cyan & yellow
				else
					h = 4 + ( r - g ) / delta;	// between magenta & cyan
				h *= 60;				// degrees
				if ( h < 0 )
					h += 360;
			}
		}
		public static void HSVtoRGB( ref float r, ref float g, ref float b, float h, float s, float v )
		{
			if ( s == 0 )
			{
				// achromatic (grey)
				r = g = b = v;
				return;
			}
			h /= 60;			// sector 0 to 5
			var i = ( int )Math.Floor( h );
			var f = h - i;
			var p = v * ( 1 - s );
			var q = v * ( 1 - s * f );
			var t = v * ( 1 - s * ( 1 - f ) );
			switch ( i )
			{
				case 0:
					r = v;
					g = t;
					b = p;
					break;
				case 1:
					r = q;
					g = v;
					b = p;
					break;
				case 2:
					r = p;
					g = v;
					b = t;
					break;
				case 3:
					r = p;
					g = q;
					b = v;
					break;
				case 4:
					r = t;
					g = p;
					b = v;
					break;
				default:		// case 5:
					r = v;
					g = p;
					b = q;
					break;
			}
		}
	}
}
