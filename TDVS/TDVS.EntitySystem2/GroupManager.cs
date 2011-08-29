using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem2
{
    public class GroupManager
    {
        private World world;

        public GroupManager(World world)
        {
            // TODO: Complete member initialization
            this.world = world;
        }
        internal void Set(string group, Entity entity)
        {
            throw new NotImplementedException();
        }

        internal void Remove(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
