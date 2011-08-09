using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS
{
	public static class FpsMeter
	{
		private static float totalTime = 0f;
		private static int frameCount = 0;
		private static int fps = 0;

		public static void Update( GameTime gameTime )
		{
			totalTime += ( float )gameTime.ElapsedGameTime.TotalSeconds;			
			if ( totalTime > 1 )
			{
				fps = frameCount;
				frameCount = 0;
				totalTime = 0f;
			}
			else
			{
				frameCount++;
			}
		}

		public static int FPS { get { return fps; } }
	}
}
