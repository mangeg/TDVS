using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS
{
	public class HardwareCursor
	{
		public static void ApplyCursor( IntPtr windowHandle, String image, Point size, Point hotSpot, Color color )
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( image );
			if ( color != null )
			{
				FormsHelpers.TintBitmap( bmp, 
					System.Drawing.Color.FromArgb( color.A, color.R, color.G, color.B ), 0.9f );
			}

			System.Windows.Forms.Cursor cur = null;
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
			System.Windows.Forms.Form.FromHandle( windowHandle ).Cursor = cur;
		}
		public static void ApplyCursor( IntPtr windowHandle, String image, float scale, Point hotSpot, Color color )
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( image );
			if ( color != null )
			{
				FormsHelpers.TintBitmap( bmp,
					System.Drawing.Color.FromArgb( color.A, color.R, color.G, color.B ), 0.9f );
			}

			System.Windows.Forms.Cursor cur = null;
			if ( hotSpot == Point.Zero )
			{
				cur = Native.CreateCursor( bmp, scale );
			}
			else
			{
				cur = Native.CreateCursor( bmp, scale, hotSpot.X, hotSpot.Y );
			}
			System.Windows.Forms.Form.FromHandle( windowHandle ).Cursor = cur;
		}
		public static void ApplyCursor( IntPtr windowHandle, String image, float scale, Point hotSpot )
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( image );

			System.Windows.Forms.Cursor cur = null;
			if ( hotSpot == Point.Zero )
			{
				cur = Native.CreateCursor( bmp, scale );
			}
			else
			{
				cur = Native.CreateCursor( bmp, scale, hotSpot.X, hotSpot.Y );
			}
			System.Windows.Forms.Form.FromHandle( windowHandle ).Cursor = cur;
		}
	}
}
