using TDVS.EntitySystem;
using Microsoft.Xna.Framework;

namespace TDVS.Game.EntitySystems.Components
{
	public class Velocity2D : IComponent
	{
		public float Speed { get; set; }
		public Vector2 Direction { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Velocity2D"/> class.
		/// </summary>
		public Velocity2D()
		{
			Reset();
		}
		public Velocity2D( float speed, Vector2 direction )
		{
			Speed = speed;
			Direction = direction;
		}

		/// <summary>
		/// Resets this instance and sets default values.
		/// </summary>
		public void Reset()
		{
			Speed = 0f;
			Direction = Vector2.Zero;
		}
	}
}
