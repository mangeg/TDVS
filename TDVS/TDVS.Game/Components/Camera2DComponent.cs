using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
    public class Camera2DComponent : IComponent
    {
        private Vector2 _position;
        private Point _viewPortSize;
        private Rectangle _worldRectangle;

        public Point ViewPortSize
        {
            get { return _viewPortSize; }
            set { _viewPortSize = value; }
        }

        public Rectangle WorldRectangle
        {
            get { return _worldRectangle; }
            set { _worldRectangle = value; }
        }

        public Point WorldSize
        {
            get { return new Point( WorldRectangle.X, WorldRectangle.Y ); }
            set 
            {
                _worldRectangle.Width = value.X;
                _worldRectangle.Height = value.Y;
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position =
                    new Vector2(
                        MathHelper.Clamp(value.X, _worldRectangle.X, _worldRectangle.Width - ViewPortSize.X),
                        MathHelper.Clamp(value.Y, _worldRectangle.Y, _worldRectangle.Height - ViewPortSize.Y));
            }
        }

        #region IComponent Members

        public void Reset()
        {
            _position = Vector2.Zero;
            _viewPortSize = Point.Zero;
            _worldRectangle = Rectangle.Empty;
        }

        #endregion
    }
}
