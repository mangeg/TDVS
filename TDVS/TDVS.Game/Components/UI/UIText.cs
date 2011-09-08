using System;
using TDVS.Common.Resources;
using TDVS.EntitySystem;

namespace TDVS.Game.Components.UI
{
	public class UIText : IComponent
	{
		public ResourceID FontID { get; set; }
		public String Text { get; set; }

		public void Reset()
		{
			Text = String.Empty;
		}
	}
}
