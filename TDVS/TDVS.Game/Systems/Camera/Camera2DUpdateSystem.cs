using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TDVS.EntitySystem;
using TDVS.Game.Components;

namespace TDVS.Game.Systems.Camera
{
    public sealed class Camera2DUpdateSystem : EntityProcessingSystem
    {
        public Camera2DUpdateSystem() : base(typeof(Camera2DComponent)) { }

        public override void Process(Entity e)
        {
            throw new NotImplementedException();
        }

        public static void Initialize(Entity e, Point worldSize, Point viewPortSize)
        {
            //var camera2D = e.GetComponent<Camera2DComponent>();
            
            //camera2D.WorldRectangle = new Rectangle(0, 0, worldSize.X, worldSize.Y);
            //camera2D.ViewPortSize = viewPortSize;
        }


        public static void Move(Entity e, Vector2 offset)
        {
            var camera = e.GetComponent<Camera2DComponent>( );

            camera.Position += offset;
        }

        public static Vector2 Transform(Entity e, Vector2 point)
        {
            var camera = e.GetComponent<Camera2DComponent>( );
            return point - camera.Position;
        }

        public static Rectangle Transform(Entity e, Rectangle rectangle)
        {
            var camera = e.GetComponent<Camera2DComponent>( );
            return new Rectangle( rectangle.Left - ( int ) camera.Position.X, rectangle.Top - ( int ) camera.Position.Y,
                                  rectangle.Width, rectangle.Height );
        }

        public Matrix Transform(Entity e)
        {
            var camera = e.GetComponent<Camera2DComponent>( );

            var transform = Matrix.CreateTranslation( new Vector3( -camera.Position.X, -camera.Position.Y, 0 ) )*
                            Matrix.CreateRotationZ( 0.0f )*
                            Matrix.CreateScale( new Vector3( 1.0f, 1.0f, 0 ) )*
                            Matrix.CreateTranslation( new Vector3( camera.WorldSize.X*0.5f,
                                                                   camera.WorldSize.Y*0.5f, 0 ) );
            return transform;
        }
    }
}
