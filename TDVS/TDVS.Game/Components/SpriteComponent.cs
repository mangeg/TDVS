using Microsoft.Xna.Framework;
using TDVS.Common.Resources;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class SpriteComponent : IComponent
	{
		private Rectangle _rect = Rectangle.Empty;

		public Color Color { get; set; }
		public ResourceID TextureID { get; set; }
		public Rectangle Rect { get { return _rect; } set { _rect = value; } }
		public int X { get { return _rect.X; } set { _rect.X = value; } }
		public int Y { get { return _rect.Y; } set { _rect.Y = value; } }
		public int Width { get { return _rect.Width; } set { _rect.Width = value; } }
		public int Height { get { return _rect.Height; } set { _rect.Height = value; } }
		public bool UseRect { get; set; }

		public SpriteComponent()
		{
			Reset();
		}

		public void Reset()
		{
			Color = Color.White;
			TextureID = ResourceID.Zero;
			Rect = Rectangle.Empty;
			UseRect = false;
		}
	}
}
