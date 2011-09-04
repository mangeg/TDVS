using System.ComponentModel;

namespace TDVS.Common.Settings
{
	public class Resolution
	{
		[DefaultValue( 800 )]
		public int Width { get; set; }
		[DefaultValue( 600 )]
		public int Height { get; set; }
	}
}