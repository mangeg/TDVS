using Microsoft.Xna.Framework;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class Transform2DComponent : IComponent 
	{
		public Vector2 Transform { get; set; }
		public float Roation { get; set; }
		public float Scale { get; set; }

		public Transform2DComponent()
		{
			Reset();
		}

		public void Reset()
		{
			Transform = Vector2.Zero;
			Roation = 0;
			Scale = 1f;
		}
	}
}
