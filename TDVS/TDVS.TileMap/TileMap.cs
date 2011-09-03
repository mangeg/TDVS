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

namespace TDVS.TileMap
{
    public class TileMap
    {
        private int[,] _map;
        private Random _random = new Random();

        public TileMap()
        {
            _map = new int[10,10];
            for(var x = 0; x < 10; x++)
            {
                for(var y = 0; y < 10; y++)
                {
                    _map[x, y] = _random.Next(10);
                }
            }
        }
    }
}
