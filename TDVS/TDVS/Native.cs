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
		[DllImport( "user32.dll" )]
		public static extern void ClipCursor( ref Rectangle rect );
		[DllImport( "user32.dll" )]
		public static extern bool GetClipCursor( ref Rectangle rect);
	}
}
