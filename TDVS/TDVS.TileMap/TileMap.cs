using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TDVS.Common.TileMap
{
    public class TileMap
    {
        private int[,] _map;
        private Random _random = new Random();
        private Texture2D _tileset;

        private Point _tileSize = new Point(64, 64);
        private Point _worldSize = new Point(128, 128); // Size in tiles.

        public TileMap(Texture2D tileset)
        {
            _tileset = tileset;
            _map = new int[_worldSize.X,_worldSize.Y];
            for(var x = 0; x < _worldSize.X; x++)
            {
                for(var y = 0; y < _worldSize.Y; y++)
                {
                    _map[ x, y ] = _random.Next(16);
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(var y = 0; y < _worldSize.Y; y++)
            {
                for(var x = 0; x < _worldSize.X; x++)
                {
                    var tileNo = _map[ y, x ];

                    var tileX = tileNo%4;
                    var tileY = tileNo/4;

                    var sourceRectangle = new Rectangle( x*_tileSize.X, y*_tileSize.Y, _tileSize.X, _tileSize.Y );
                    var destinationRectangle = new Rectangle( tileX*_tileSize.X, tileY*_tileSize.Y, _tileSize.X, _tileSize.Y );

                    spriteBatch.Draw( _tileset, sourceRectangle, destinationRectangle, Color.White );
                }
            }
        }
    }
}
