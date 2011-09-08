using Microsoft.Xna.Framework;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class Velocity2DComponent : IComponent
	{
		public Vector2 Direction { get; set; }
		public float Speed { get; set; }

		public void Reset()
		{
			Direction = Vector2.Zero;
			Speed = 0;
		}
	}
}