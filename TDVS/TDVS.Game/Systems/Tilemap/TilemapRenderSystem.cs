using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.EntitySystem;
using TDVS.Game.Components;

namespace TDVS.Game.Systems.Tilemap
{
    public class TilemapRenderSystem : TagSystem
    {
        public TilemapRenderSystem() : base("TILEMAP")
        {
            
        }

        public override void Process(Entity e)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity e, SceneNodeComponent node)
        {
            
        }
    }
}
