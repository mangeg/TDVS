using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Extensions
{
	public static class XnaStructExtensions
	{
		public static Vector2 ToVector2( this Point point )
		{
			return new Vector2( point.X, point.Y );
		}
		public static Point ToPoint( this Vector2 vec )
		{
			return new Point( ( int )vec.X, ( int )vec.Y );
		}
	}
}
