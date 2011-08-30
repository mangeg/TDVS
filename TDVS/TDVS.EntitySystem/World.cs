using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	public class World
	{
		private EntityManager _entityManager;
		private SystemManager _systemsManager;
		private GroupManager _groupManager;

		/// <summary>
		/// Gets the entity manager.
		/// </summary>
		public EntityManager EntityManager
		{
			get { return _entityManager; }
		}
		/// <summary>
		/// Gets the system manager.
		/// </summary>
		public SystemManager SystemManager
		{
			get { return _systemsManager; }
		}
		/// <summary>
		/// Gets the group manager.
		/// </summary>
		public GroupManager GroupManager
		{
			get { return _groupManager; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="World"/> class.
		/// </summary>
		public World()
		{
			_entityManager = new EntityManager( this );
			_systemsManager = new SystemManager( this );
			_groupManager = new GroupManager();
		}
	}
}
