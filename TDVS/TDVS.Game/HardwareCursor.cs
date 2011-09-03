using System;
using Microsoft.Xna.Framework;
using TDVS.Common.Utils;
using Cursor = System.Windows.Forms.Cursor;

namespace TDVS.Game
{
	public class HardwareCursor
	{
		public static void ApplyCursor( IntPtr windowHandle, String image, Point size, Point hotSpot, Color color )
		{
			var bmp = new System.Drawing.Bitmap( image );
			FormsHelpers.TintBitmap( bmp, 
			                         System.Drawing.Color.FromArgb( color.A, color.R, color.G, color.B ), 0.9f );

			Cursor cur;
			if ( size != Point.Zero && hotSpot == Point.Zero )
			{
				cur = Native.CreateCursor( bmp, size.X, size.Y );
			}
			else if ( size != Point.Zero && hotSpot != Point.Zero )
			{
				cur = Native.CreateCursor( bmp, size.X, size.Y, hotSpot.X, hotSpot.Y );
			}
			else
			{
				cur = Native.CreateCursor( bmp );
			}
			System.Windows.Forms.Control.FromHandle( windowHandle ).Cursor = cur;
		}
		public static void ApplyCursor( IntPtr windowHandle, String image, float scale, Point hotSpot, Color color )
		{
			var bmp = new System.Drawing.Bitmap( image );
			FormsHelpers.TintBitmap( bmp,
			                         System.Drawing.Color.FromArgb( color.A, color.R, color.G, color.B ), 0.9f );

			var cur = hotSpot == Point.Zero ? 
				Native.CreateCursor( bmp, scale ) : Native.CreateCursor( bmp, scale, hotSpot.X, hotSpot.Y );
			System.Windows.Forms.Control.FromHandle( windowHandle ).Cursor = cur;
		}
		public static void ApplyCursor( IntPtr windowHandle, String image, float scale, Point hotSpot )
		{
			var bmp = new System.Drawing.Bitmap( image );

			var cur = hotSpot == Point.Zero ? 
				Native.CreateCursor( bmp, scale ) : Native.CreateCursor( bmp, scale, hotSpot.X, hotSpot.Y );
			System.Windows.Forms.Control.FromHandle( windowHandle ).Cursor = cur;
		}
	}
}
