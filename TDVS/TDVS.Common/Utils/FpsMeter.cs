using Microsoft.Xna.Framework;

namespace TDVS.Common.Utils
{
	/// <summary>
	/// Static class for monitoring FPS
	/// </summary>
	public class FpsMeter
	{
		#region Static
		private static float _sTotalTime;
		private static int _sFrameCount;
		private static int _sFps;

		/// <summary>
		/// Gets the current FPS.
		/// </summary>
		public static int sFPS { get { return _sFps; } }

		/// <summary>
		/// Updates the meter with specified game time.
		/// </summary>
		/// <param name="gameTime">The game time.</param>
		public static void SUpdate( GameTime gameTime )
		{
			_sTotalTime += ( float )gameTime.ElapsedGameTime.TotalSeconds;
			if ( _sTotalTime > 1 )
			{
				_sFps = _sFrameCount;
				_sFrameCount = 0;
				_sTotalTime = 0f;
			}
			_sFrameCount++;
		}
		#endregion

		private float _totalTime;
		private int _frameCount;

		/// <summary>
		/// Gets the FPS.
		/// </summary>
		public int FPS { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FpsMeter"/> class.
		/// </summary>
		public FpsMeter()
		{
			FPS = 0;
		}

		/// <summary>
		/// Updates the <see cref="FpsCounter"/>.
		/// </summary>
		/// <param name="gameTime">The game time.</param>
		public void Update( GameTime gameTime )
		{
			_totalTime += ( float )gameTime.ElapsedGameTime.TotalSeconds;
			if ( _totalTime > 1 )
			{
				FPS = _frameCount;
				_frameCount = 0;
				_totalTime = 0f;
			}
			_frameCount++;
		}
	}
}
