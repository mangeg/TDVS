using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS
{
	/// <summary>
	/// Static class for monitoring FPS
	/// </summary>
	public static class FpsMeter
	{
		private static float totalTime = 0f;
		private static int frameCount = 0;
		private static int fps = 0;

		/// <summary>
		/// Updates the meter with specified game time.
		/// </summary>
		/// <param name="gameTime">The game time.</param>
		public static void Update( GameTime gameTime )
		{
			totalTime += ( float )gameTime.ElapsedGameTime.TotalSeconds;
			frameCount++;
			if ( totalTime > 1 )
			{
				fps = frameCount;
				frameCount = 0;
				totalTime = 0f;
			}
			else
			{
				//frameCount++;
			}
		}

		/// <summary>
		/// Gets the current FPS.
		/// </summary>
		public static int FPS { get { return fps; } }
	}
}
