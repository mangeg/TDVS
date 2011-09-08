using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Game.Camera
{
    public class Camera
    {
        private static Vector2 _position = Vector2.Zero;
        private static Point _viewPortSize = Point.Zero;
        private static Rectangle _worldRectangle = Rectangle.Empty;

        public static Point ViewPortSize
        {
            get { return _viewPortSize; }
            set { _viewPortSize = value; }
        }

        public static Rectangle WorldRectangle
        {
            get { return _worldRectangle; }
            set { _worldRectangle = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set
            {
                _position =
                    new Vector2(
                        MathHelper.Clamp( value.X, _worldRectangle.X, _worldRectangle.Width - ViewPortSize.X ),
                        MathHelper.Clamp( value.Y, _worldRectangle.Y, _worldRectangle.Height - ViewPortSize.Y ) );
            }
        }

        public static void Initialize(Point worldSize, Point viewPortSize)
        {
            _worldRectangle = new Rectangle(0, 0, worldSize.X, worldSize.Y);
            _viewPortSize = viewPortSize;
        }

        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        public static Vector2 Transform(Vector2 point)
        {
            return point - _position;
        }

        public static Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle( rectangle.Left - ( int ) _position.X, rectangle.Top - ( int ) _position.Y,
                                  rectangle.Width, rectangle.Height );
        }
    }
}
