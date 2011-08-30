using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	public sealed class GroupManager
	{
		private World _world;

		public GroupManager( World world )
		{
			_world = world;
		}
	}
}
