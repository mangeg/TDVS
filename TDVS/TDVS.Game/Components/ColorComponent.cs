using Microsoft.Xna.Framework;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class ColorComponent : IComponent 
	{
		public Color Color { get; set; }

		public ColorComponent()
		{
			Reset();
		}

		public void Reset()
		{
			Color = Color.White;
		}
	}
}
