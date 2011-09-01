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
		private TagManager _tagManager;

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
		/// Gets the tag manager.
		/// </summary>
		public TagManager TagManager
		{
			get { return _tagManager; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="World"/> class.
		/// </summary>
		public World()
		{
			_entityManager = new EntityManager( this );
			_systemsManager = new SystemManager( this );
			_groupManager = new GroupManager();
			_tagManager = new TagManager();
		}

		/// <summary>
		/// Creates a entity.
		/// </summary>
		/// <returns></returns>
		public Entity CreateEntity()
		{
			return _entityManager.Create();
		}
		/// <summary>
		/// Creates a entity and tags it with the specified tag.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <returns></returns>
		public Entity CreateEntity( String tag )
		{
			var e = _entityManager.Create();
			_tagManager.Register( tag, e );
			return e;
		}
	}
}
