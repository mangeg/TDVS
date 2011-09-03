using TDVS.EntitySystem;
using Microsoft.Xna.Framework;

namespace TDVS.Game.EntitySystem.Components
{
	public class Velocity2D : IComponent
	{
		public float Speed { get; set; }
		public Vector2 Direction { get; set; }

		public Velocity2D() { }
		public Velocity2D( float speed, Vector2 direction )
		{
			Speed = speed;
			Direction = direction;
		}

		void IComponent.Reset()
		{
			Speed = 0f;
			Direction = Vector2.Zero;
		}
	}
}
