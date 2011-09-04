using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Settings
{
	[Serializable]
	public class VideoSettings
	{
		public VideoSettings()
		{
			MouseColor = Vector4.One;
			WindowedResolution = new Resolution()
			                     	{
			                     		Width = 800,
										Height = 600
			                     	};
			FullscreenResolution = new Resolution();
			VSynchEnabled = true;
			MouseScale = 1.0f;
		}

		[DefaultValue( 1.0f )]
		public float MouseScale { get; set; }
		public Vector4 MouseColor { get; set; }
		[XmlAttribute]
		[DefaultValue( false )]
		public bool FullScreen { get; set; }
		[XmlAttribute]
		[DefaultValue( true )]
		public bool VSynchEnabled { get; set; }
		
		public Resolution WindowedResolution { get; set; }
		public Resolution FullscreenResolution { get; set; }
	}
}