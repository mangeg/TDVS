using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TDVS
{
	public static class FormsHelpers
	{
		public static void TintBitmap( Bitmap bmp, Color color, float percent )
		{
			for ( int i = 0; i < bmp.Width; i++ )
			{
				for ( int j = 0; j < bmp.Height; j++ )
				{
					System.Drawing.Color cc = bmp.GetPixel( i, j );
					if ( cc.A > 0 )
					{
						var red = Convert.ToInt32( ( ( color.R - cc.R ) * percent + cc.R ) );
						var blue = Convert.ToInt32( ( ( color.B - cc.B ) * percent + cc.B ) );
						var green = Convert.ToInt32( ( ( color.G - cc.G ) * percent + cc.G ) );
						bmp.SetPixel( i, j, System.Drawing.Color.FromArgb( cc.A, red, green, blue ) );
					}
				}
			}
		}
	}
}
