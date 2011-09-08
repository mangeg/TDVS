using Microsoft.Xna.Framework;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class LocalTransform2DComponent : IComponent
	{
		public Vector2 Transform { get; set; }
		public float Roation { get; set; }

		public void Reset()
		{
			Transform = Vector2.Zero;
			Roation = 0;
		}
	}
}