using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem2
{
    public class TagManager
    {
        private World world;

        public TagManager(World world)
        {
            // TODO: Complete member initialization
            this.world = world;
        }
        internal void Register()
        {
            throw new NotImplementedException();
        }

        internal void Register(string tag, Entity entity)
        {
            throw new NotImplementedException();
        }

        internal string GetTagOfEntity(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
