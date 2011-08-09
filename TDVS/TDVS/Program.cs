using System;

namespace TDVS
{
#if WINDOWS || XBOX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main( string[] args )
		{
			using ( TDVSGame game = new TDVSGame() )
			{
				game.Run();
			}
		}
	}
#endif
}

