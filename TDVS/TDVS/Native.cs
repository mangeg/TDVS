using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace TDVS
{
	public static class Native
	{
		public struct IconInfo
		{
			public bool fIcon;
			public int xHotspot;
			public int yHotspot;
			public IntPtr hbmMask;
			public IntPtr hbmColor;
		}

		[DllImport( "user32.dll" )]
		public static extern void ClipCursor( ref Rectangle rect );
		[DllImport( "user32.dll" )]
		public static extern bool GetClipCursor( ref Rectangle rect);
		[DllImport( "user32.dll" )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool GetIconInfo( IntPtr hIcon, ref IconInfo pIconInfo );
		[DllImport( "user32.dll" )]
		public static extern IntPtr CreateIconIndirect( ref IconInfo icon );
		[DllImport( "user32.dll" )]
		public static extern IntPtr GetCursor();
		[DllImport( "user32.dll" )]
		public static extern IntPtr SetCursor( IntPtr cursor );

		public static System.Windows.Forms.Cursor CreateCursor( System.Drawing.Bitmap bmp )
		{
			return CreateCursor( bmp, bmp.Width, bmp.Height );
		}
		public static System.Windows.Forms.Cursor CreateCursor( System.Drawing.Bitmap bmp, int width, int height )
		{
			return CreateCursor( bmp, width, height, 0, 0 );
		}
		public static System.Windows.Forms.Cursor CreateCursor( System.Drawing.Bitmap bmp, int width, int height, int xHotSpot, int yHotSpot )
		{
			if ( bmp.Width != width || bmp.Height != height )
			{
				System.Drawing.Bitmap tmpBmp = new System.Drawing.Bitmap( width, height );
				var g = System.Drawing.Graphics.FromImage( tmpBmp );
				g.DrawImage( bmp, 0, 0, width, height );

				bmp = tmpBmp;
			}
			IntPtr ptr = bmp.GetHicon();
			IconInfo tmp = new IconInfo();
			GetIconInfo( ptr, ref tmp );
			tmp.xHotspot = xHotSpot;
			tmp.yHotspot = yHotSpot;			
			tmp.fIcon = false;
			ptr = CreateIconIndirect( ref tmp );
			return new System.Windows.Forms.Cursor( ptr );
		}
		public static void SetCursor( IntPtr handle, System.Windows.Forms.Cursor cursor )
		{
			System.Windows.Forms.Form.FromHandle( handle ).Cursor = cursor;
		}
	}
}
