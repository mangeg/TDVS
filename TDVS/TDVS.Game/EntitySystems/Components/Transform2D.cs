using TDVS.EntitySystem;
using Microsoft.Xna.Framework;

namespace TDVS.Game.EntitySystems.Components
{
	public class Transform2D : IComponent
	{
		public Vector2 Transform { get; set; }

		public Transform2D()
		{
			Reset();
		}

		public void Reset()
		{
			Transform = Vector2.Zero;
		}
	}
}
