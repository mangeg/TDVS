using TDVS.EntitySystem;
using Microsoft.Xna.Framework;

namespace TDVS.Game.EntitySystem.Components
{
	public class Transform2D : IComponent
	{
		private Vector2 _transform = Vector2.Zero;

		public Vector2 Transform
		{
			get { return _transform; }
			set { _transform = value; }
		}

		void IComponent.Reset()
		{
			_transform = Vector2.Zero;
		}
	}
}
