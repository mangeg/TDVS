using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	public delegate void EntityCreatedHandler( Entity e );
	public delegate void EntityDeletedHandler( Entity e );
	public delegate void ComponentAddedHandler( Entity e, IComponent c );
	public delegate void ComponentRemovedHandler( Entity e, IComponent c );

	/// <summary>
	/// Manager for entities.
	/// </summary>
	public class EntityManager : IManager
	{
		private World _world;

		private List<Entity> _allEnteties = new List<Entity>( 10 );
		private List<int> _activeEnteties = new List<int>();
		private List<int> _inactiveEnteties = new List<int>();

		private Dictionary<int, Dictionary<int, IComponent>> _componentsForEntity =
			new Dictionary<int, Dictionary<int, IComponent>>();

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

			int id = -1;
			if ( _inactiveEnteties.Count > 0 )
			{
				id = _inactiveEnteties[ _inactiveEnteties.Count - 1 ];
				_inactiveEnteties.RemoveAt( _inactiveEnteties.Count - 1 );

				e = _allEnteties[ id ];
			}
			else
			{
				id = _allEnteties.Count;
				e = new Entity( _world, id );
				_allEnteties.Add( e );
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
		public void Refersh( Entity e )
		{

		}

		/// <summary>
		/// Adds the component.
		/// </summary>
		/// <param name="e">The entity to add the component to.</param>
		/// <param name="component">The component to add.</param>
		public void AddComponent( Entity e, IComponent component )
		{
			Dictionary<int, IComponent> list = null;

			ComponentType type = ComponentTypeManager.GetTypeFor( component.GetType() );
			if ( !_componentsForEntity.ContainsKey( type.ID ) )
			{
				list = new Dictionary<int, IComponent>();
				_componentsForEntity.Add( type.ID, list );
			}
			else
			{
				list = _componentsForEntity[ type.ID ];
			}

			list.Add( e.ID, component );
			e.TypeBits = e.TypeBits.Or( type.Bit );
			if ( ComponentAdded != null )
				ComponentAdded( e, component );
		}
		/// <summary>
		/// Adds the component.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The e.</param>
		/// <param name="component">The component.</param>
		public void AddComponent<T>( Entity e, IComponent component ) where T : IComponent
		{
			AddComponent( e, component );
		}
		/// <summary>
		/// Removes the component.
		/// </summary>
		/// <param name="e">The entity.</param>
		/// <param name="type">The type of component to remove.</param>
		public void RemoveComponent( Entity e, ComponentType type )
		{
			if ( _componentsForEntity.ContainsKey( type.ID ) )
			{
				if ( _componentsForEntity[ type.ID ].ContainsKey( e.ID ) )
				{
					var component = _componentsForEntity[ type.ID ][ e.ID ];
					_componentsForEntity[ type.ID ].Remove( e.ID );
					e.TypeBits = e.TypeBits.And( type.Bit.Not() );
					if ( ComponentRemoved != null )
						ComponentRemoved( e, component );
				}
			}
		}
		/// <summary>
		/// Removes all components.
		/// </summary>
		/// <param name="e">The entity to remove all components from.</param>
		public void RemoveAllComponents( Entity e )
		{
			foreach ( var item in _componentsForEntity )
			{
				if ( item.Value.ContainsKey( e.ID ) )
				{
					var component = item.Value[ e.ID ];
					item.Value.Remove( e.ID );
					if ( ComponentRemoved != null )
						ComponentRemoved( e, component );
				}
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
			if ( _componentsForEntity.ContainsKey( type.ID ) )
			{
				if ( _componentsForEntity[ type.ID ].ContainsKey( e.ID ) )
				{
					return _componentsForEntity[ type.ID ][ e.ID ];
				}
			}

			return null;
		}
		/// <summary>
		/// Gets the component.
		/// </summary>
		/// <typeparam name="T">The type of component to get</typeparam>
		/// <param name="e">The etity to get the component for.</param>
		/// <returns>The <see cref="IComponent"/> if found; else <c>null</c>.</returns>
		public IComponent GetComponent<T>( Entity e ) where T : IComponent
		{
			var type = ComponentTypeManager.GetTypeFor<T>();
			return GetComponent( e, type );
		}
	}
}
