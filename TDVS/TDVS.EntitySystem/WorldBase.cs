using System;
using Microsoft.Xna.Framework;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Represent the WorldBase for the component based entity system.
	/// </summary>
	public class WorldBase
	{
		private readonly EntityManager _entityManager;
		private readonly SystemManager _systemsManager;
		private readonly GroupManager _groupManager;
		private readonly TagManager _tagManager;

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
		/// Initializes a new instance of the <see cref="WorldBase"/> class.
		/// </summary>
		public WorldBase()
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

		/// <summary>
		/// Initializes the world.
		/// </summary>
		public virtual void Initialize() { }
		/// <summary>
		/// Load resources.
		/// </summary>
		public virtual void LoadResource() { }
		/// <summary>
		/// Unload resources.
		/// </summary>
		public virtual void UnloadResources() { }
		/// <summary>
		/// Updates the world.
		/// </summary>
		/// <param name="gameTime">The game time.</param>
		public virtual void Update( GameTime gameTime ) { }
		/// <summary>
		/// Draws the world.
		/// </summary>
		/// <param name="gameTime">The game time.</param>
		public virtual void Draw( GameTime gameTime ) { }
	}
}
