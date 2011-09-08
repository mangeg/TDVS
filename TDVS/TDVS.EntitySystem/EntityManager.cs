using System.Collections.Generic;
using System.Linq;
using TDVS.Common.Extensions;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Delegate for EntityCreated events.
	/// </summary>
	/// <param name="e">The entity.</param>
	public delegate void EntityCreatedHandler( Entity e );
	/// <summary>
	/// Delegate for EntityDeleted events.
	/// </summary>
	/// <param name="e">The entity.</param>
	public delegate void EntityDeletedHandler( Entity e );
	/// <summary>
	/// Delegate for ComponentAdded events.
	/// </summary>
	/// <param name="e">The entity.</param>
	/// <param name="c">The component.</param>
	public delegate void ComponentAddedHandler( Entity e, IComponent c );
	/// <summary>
	/// Delegate for ComponentRemoved events.
	/// </summary>
	/// <param name="e">The entity.</param>
	/// <param name="c">The component.</param>
	public delegate void ComponentRemovedHandler( Entity e, IComponent c );

	/// <summary>
	/// Manager for entities.
	/// </summary>
	public class EntityManager : IManager
	{
		private readonly World _world;

		private readonly List<Entity> _allEnteties = new List<Entity>( 10 );
		private readonly List<int> _activeEnteties = new List<int>();
		private readonly List<int> _inactiveEnteties = new List<int>();

		private readonly Dictionary<int, Dictionary<int, IList<IComponent>>> _componentsForEntity =
			new Dictionary<int, Dictionary<int, IList<IComponent>>>();

		/// <summary>
		/// Occurs when an <see cref="Entity"/> is created/activated.
		/// </summary>
		public event EntityCreatedHandler EntityCreated;
		/// <summary>
		/// Occurs when en <see cref="Entity"/> is removed/deleted.
		/// </summary>
		public event EntityDeletedHandler EntityRemoved;
		/// <summary>
		/// Occurs when a component is added.
		/// </summary>
		public event ComponentAddedHandler ComponentAdded;
		/// <summary>
		/// Occurs when a component is removed.
		/// </summary>
		public event ComponentRemovedHandler ComponentRemoved;

		/// <summary>
		/// Gets the active count.
		/// </summary>
		public int ActiveCount
		{
			get { return _activeEnteties.Count; }
		}
		/// <summary>
		/// Gets the inactive count.
		/// </summary>
		public int InactiveCount
		{
			get { return _inactiveEnteties.Count; }
		}
		/// <summary>
		/// Gets the total count.
		/// </summary>
		public int TotalCount
		{
			get { return _allEnteties.Count; }
		}

		/// <summary>
		/// Gets the world.
		/// </summary>
		public World World
		{
			get { return _world; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityManager"/> class.
		/// </summary>
		/// <param name="world">The world.</param>
		public EntityManager( World world )
		{
			_world = world;
		}

		/// <summary>
		/// Creates a new Entity.
		/// </summary>
		/// <returns>The new <see cref="Entity"/>.</returns>
		public Entity Create()
		{
			Entity e;

			int id;
			if ( _inactiveEnteties.Count > 0 )
			{
				id = _inactiveEnteties.Pop();
				e = _allEnteties[ id ];
			}
			else
			{
				id = _allEnteties.Count;
				e = new Entity( _world, id );
				_allEnteties.Add( e );
				_componentsForEntity.Add( e.ID, new Dictionary<int, IList<IComponent>>() );
			}

			_activeEnteties.Add( id );

			if ( EntityCreated != null )
				EntityCreated( e );

			return e;
		}
		/// <summary>
		/// Deletes the specified Entity.
		/// </summary>
		/// <param name="e">The <c>Entity</c> to delete.</param>
		public void Delete( Entity e )
		{
			_activeEnteties.Remove( e.ID );
			_inactiveEnteties.Add( e.ID );

			RemoveAllComponents( e );

			if ( EntityRemoved != null )
				EntityRemoved( e );
		}
		/// <summary>
		/// Gets a specific <see cref="Entity"/>.
		/// </summary>
		/// <param name="id">The entity id.</param>
		/// <returns></returns>
		public Entity GetEntity( int id )
		{
			if ( _activeEnteties.Contains( id ) )
				return _allEnteties[ id ];

			return default( Entity );
		}
		/// <summary>
		/// Determines whether the specified id is an active <see cref="Entity"/>.
		/// </summary>
		/// <param name="id">The entity id.</param>
		/// <returns>
		///   <c>true</c> if the specified id is active; otherwise, <c>false</c>.
		/// </returns>
		public bool IsActive( int id )
		{
			return _activeEnteties.Contains( id );
		}
		/// <summary>
		/// Refershes the specified entity.
		/// </summary>
		/// <param name="e">The entity.</param>
		public void Refersh( Entity e )
		{
			foreach ( var system in _world.SystemManager.Systems )
			{
				system.EntityChanged( e );
			}
		}

		/// <summary>
		/// Adds the component.
		/// </summary>
		/// <param name="e">The entity to add the component to.</param>
		/// <param name="component">The component to add.</param>
		/// <returns>The <see cref="IComponent"/> that was passed in.</returns>
		public IComponent AddComponent( Entity e, IComponent component )
		{
			var type = ComponentTypeManager.GetTypeFor( component.GetType() );
			if ( !_componentsForEntity[ e.ID ].ContainsKey( type.ID ) )
			{
				_componentsForEntity[ e.ID ].Add( type.ID, new List<IComponent>() );
			}

			_componentsForEntity[ e.ID ][ type.ID ].Add( component );
			e.TypeBits.Or( type.Bit );
			if ( ComponentAdded != null )
				ComponentAdded( e, component );

			return component;
		}
		/// <summary>
		/// Removes the component.
		/// </summary>
		/// <param name="e">The entity.</param>
		/// <param name="type">The type of component to remove.</param>
		public void RemoveComponent( Entity e, ComponentType type )
		{
			// Have we ever registered a component of this type for this entity?
			if ( _componentsForEntity[ e.ID ].ContainsKey( type.ID ) )
			{
				// Do we currently have any component of this type registred.
				if ( _componentsForEntity[ e.ID ][ type.ID ].Count > 0 )
				{
					// Fire event
					if ( ComponentRemoved != null )
					{
						foreach ( var component in _componentsForEntity[ e.ID ][ type.ID ] )
						{
							ComponentRemoved( e, component );
						}
					}
					// Remove all of them
					_componentsForEntity[ e.ID ][ type.ID ].Clear();
				}
			}
			// Remove type bits
			e.TypeBits.And( type.Bit.Not() );
		}
		/// <summary>
		/// Removes all components.
		/// </summary>
		/// <param name="e">The entity to remove all components from.</param>
		public void RemoveAllComponents( Entity e )
		{
			foreach ( var componentType in _componentsForEntity[ e.ID ] )
			{
				RemoveComponent( e, ComponentTypeManager.GetTypeFor( componentType.Key ) );
			}
		}

		/// <summary>
		/// Gets the component.
		/// </summary>
		/// <param name="e">The entity to get the component for.</param>
		/// <param name="type">The type of component.</param>
		/// <returns>The <see cref="IComponent"/> if found; else <c>null</c>.</returns>
		public IComponent GetComponent( Entity e, ComponentType type )
		{
			if ( _componentsForEntity[ e.ID ].ContainsKey( type.ID ) )
			{
				return _componentsForEntity[ e.ID ][ type.ID ].FirstOrDefault();
			}

			return null;
		}
		/// <summary>
		/// Gets the component.
		/// </summary>
		/// <typeparam name="T">The type of component to get</typeparam>
		/// <param name="e">The etity to get the component for.</param>
		/// <returns>The <see cref="IComponent"/> if found; else <c>null</c>.</returns>
		public T GetComponent<T>( Entity e ) where T : IComponent
		{
			var type = ComponentTypeManager.GetTypeFor<T>();
			return ( T )GetComponent( e, type );
		}
		/// <summary>
		/// Gets all the components of the specific type.
		/// </summary>
		/// <typeparam name="T">The type of components to get.</typeparam>
		/// <param name="entity">The entity to find the components for.</param>
		/// <returns>List of all components found.</returns>
		public IEnumerable<T> GetComponents<T>( Entity entity ) where T : IComponent
		{
			var type = ComponentTypeManager.GetTypeFor<T>();
			if ( _componentsForEntity[ entity.ID ].ContainsKey( type.ID ) )
			{
				return _componentsForEntity[ entity.ID ][ type.ID ].Cast<T>();
			}
			return null;
		}
	}
}
