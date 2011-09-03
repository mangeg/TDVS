using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TDVS.Common;
using TDVS.Common.Extensions;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Abstract class for a system to process a set of <see cref="Entity"/>
	/// </summary>
	public abstract class EntitySystem
	{
		/// <summary>
		/// Sets the max number of component types allowed.
		/// </summary>
		public static readonly int MAX_NR_COMPONENT_TYPES = 128;
		/// <summary>
		/// Sets the max number of entity system types.
		/// </summary>
		public static readonly int MAX_NR_SYSTEM_TYPES = 64;

		/// <summary>
		/// Enabled flag
		/// </summary>
		protected bool _enabled = true;

		/// <summary>
		/// System bits for this type.
		/// </summary>
		protected BitArrayExt _systemBits = new BitArrayExt( EntitySystem.MAX_NR_SYSTEM_TYPES );
		/// <summary>
		/// Type bits for this type.
		/// </summary>
		protected BitArrayExt _typeBits = new BitArrayExt( EntitySystem.MAX_NR_COMPONENT_TYPES );

		/// <summary>
		/// All entities bound to this system.
		/// </summary>
		protected Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();

		/// <summary>
		/// Reference to the world object.
		/// </summary>
		protected World _world;

		/// <summary>
		/// Gets or sets the system bit.
		/// </summary>
		/// <value>
		/// The system bit.
		/// </value>
		public BitArrayExt SystemBit
		{
			get { return _systemBits; }
			set { _systemBits = value; }
		}
		/// <summary>
		/// Gets or sets the type bit.
		/// </summary>
		/// <value>
		/// The type bit.
		/// </value>
		public BitArrayExt TypeBit
		{
			get { return _typeBits; }
			set { _typeBits = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="EntitySystem"/> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}
		/// <summary>
		/// Gets the world.
		/// </summary>
		public World World
		{
			get { return _world; }
			internal set { _world = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EntitySystem"/> class.
		/// </summary>
		protected EntitySystem()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="EntitySystem"/> class.
		/// </summary>
		/// <param name="types">The types (components) that this system handles.</param>
		protected EntitySystem( params Type[] types )
		{
			foreach ( var ctype in types.Select( ComponentTypeManager.GetTypeFor ) )
			{
				_typeBits.Or( ctype.Bit );
			}
		}

		/// <summary>
		/// Enables this instance.
		/// </summary>
		public void Enable()
		{
			_enabled = true;
		}
		/// <summary>
		/// Disables this instance.
		/// </summary>
		public void Disable()
		{
			_enabled = false;
		}
		/// <summary>
		/// Toggles this instance.
		/// </summary>
		public void Toggle()
		{
			_enabled = !_enabled;
		}
		/// <summary>
		/// Adds a entity.
		/// </summary>
		/// <param name="e">The etity to add.</param>
		public void AddEntity( Entity e )
		{
			e.SystemBits.Or( _systemBits );
			_entities.Add( e.ID, e );
			EntityAdded( e );
		}
		/// <summary>
		/// Removes a entity.
		/// </summary>
		/// <param name="e">The entity to remove.</param>
		public void RemoveEntity( Entity e )
		{
			e.SystemBits.And( _systemBits.Not() );
			if ( _entities.ContainsKey( e.ID ) )
				_entities.Remove( e.ID );
			EntityRemoved( e );
		}
		/// <summary>
		/// Signals that a entity have changed.
		/// </summary>
		/// <param name="e">The entity that have changed.</param>
		public void EntityChanged( Entity e )
		{
			var contains = ( _systemBits & e.SystemBits ) == _systemBits;
			var interest = ( _typeBits & e.TypeBits ) == _typeBits;

			if ( interest && !contains )
			{
				AddEntity( e );
			}
			else if ( !interest && contains )
			{
				RemoveEntity( e );
			}
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public virtual void Initialize() { }
		/// <summary>
		/// Perform a check to see if this system should be processing.
		/// </summary>
		/// <returns><c>true</c> if it should; else <c>false</c></returns>
		public virtual bool CheckProcessing() { return _enabled; }
		/// <summary>
		/// Processes entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		public virtual void ProcessEntities( Dictionary<int, Entity> entities ) { }

		/// <summary>
		/// Called before doing any processing.
		/// </summary>
		public virtual void Begin() { }
		/// <summary>
		/// Called after processing is finished.
		/// </summary>
		public virtual void End() { }
		/// <summary>
		/// Processes all entities for this system.
		/// Override to change behavior.
		/// </summary>
		public virtual void Process()
		{
			if ( CheckProcessing() )
			{
				Begin();
				ProcessEntities( _entities );
				End();
			}
		}

		/// <summary>
		/// Called when an entity is added, override to perform any action.
		/// </summary>
		/// <param name="e">The etity that was added.</param>
		public virtual void EntityAdded( Entity e ) { }
		/// <summary>
		/// Called when an entity is removed, override to perform any action..
		/// </summary>
		/// <param name="e">The entity that was removed.</param>
		public virtual void EntityRemoved( Entity e ) { }

		/// <summary>
		/// Gets a merged array of types.
		/// </summary>
		/// <param name="reqType">Type of the required type.</param>
		/// <param name="types">The other types.</param>
		/// <returns></returns>
		public static Type[] GetTypes( Type reqType, params Type[] types )
		{
			var list = types.ToList();
			list.Insert( 0, reqType );
			return list.ToArray();
		}
	}
}
