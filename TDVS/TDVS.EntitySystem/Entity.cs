using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;
using TDVS.Common;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// The basic entity class for all game entities.
	/// </summary>
	[Serializable]
	public class Entity
	{
		private int _id;

		private BitArrayExt _typeBits = new BitArrayExt( EntitySystem.MAX_NR_COMPONENT_TYPES );
		private BitArrayExt _systemBits = new BitArrayExt( EntitySystem.MAX_NR_SYSTEM_TYPES );
		
		private EntityManager _entityManager;
		private WorldBase _world;

		/// <summary>
		/// Gets the unique ID for this <see cref="Entity"/>.
		/// </summary>
		[XmlIgnore]
		public int ID
		{
			get { return _id; }
			internal set { _id = value; }
		}
		/// <summary>
		/// Gets or sets the type bits.
		/// </summary>
		/// <value>
		/// The type bits.
		/// </value>
		[XmlIgnore]
		public BitArrayExt TypeBits
		{
			get { return _typeBits; }
			set { _typeBits = value; }
		}

		/// <summary>
		/// Gets or sets the system bits.
		/// </summary>
		/// <value>
		/// The system bits.
		/// </value>
		[XmlIgnore]
		public BitArrayExt SystemBits
		{
			get { return _systemBits; }
			set { _systemBits = value; }
		}

		/// <summary>
		/// Sets the WorldBase.
		/// </summary>
		/// <value>
		/// The WorldBase.
		/// </value>
		[XmlIgnore]
		internal WorldBase WorldBase
		{
			set { if ( value == null ) throw new ArgumentNullException( "value" ); }
		}

		/// <summary>
		/// Sets the entity manager.
		/// </summary>
		/// <value>
		/// The entity manager.
		/// </value>
		[XmlIgnore]
		internal EntityManager EntityManager
		{
			set { _entityManager = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity"/> class.
		/// </summary>
		public Entity()
		{
			Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity"/> class.
		/// </summary>
		/// <param name="worldBase">The WorldBase.</param>
		/// <param name="id">The id.</param>
		public Entity( WorldBase worldBase, int id )
		{
			_world = worldBase;
			_entityManager = worldBase.EntityManager;
			_id = id;
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			_typeBits.SetAll( false );
			_systemBits.SetAll( false );			
		}
		/// <summary>
		/// Adds the component to the entity.
		/// </summary>
		/// <param name="component">The component.</param>
		/// <returns>The <see cref="IComponent"/> that was passed in.</returns>
		public IComponent AddComponent( IComponent component )
		{
			return _entityManager.AddComponent( this, component );
		}
		/// <summary>
		/// Gets a component of specific type.
		/// </summary>
		/// <typeparam name="T">Type of component</typeparam>
		/// <returns>The component if found.</returns>
		public T GetComponent<T>() where T : IComponent
		{
			return _entityManager.GetComponent<T>( this );
		}
		/// <summary>
		/// Gets all the components of the specific type.
		/// </summary>
		/// <typeparam name="T">The type of components to get.</typeparam>
		/// <returns><see cref="IEnumerable{T}"/> of all the components of the specific type.</returns>
		public IEnumerable<T> GetComponents<T>() where T : IComponent
		{
			return _entityManager.GetComponents<T>( this );
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format( "Entity[{0}]", _id );
		}

		/// <summary>
		/// Refreshes this instance.
		/// </summary>
		public void Refresh()
		{
			_entityManager.Refersh( this );
		}
		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public void Delete()
		{
			_entityManager.Delete( this );
		}
		/// <summary>
		/// Sets the tag for this entity.
		/// </summary>
		/// <param name="tag">The tag.</param>
		public void SetTag(string tag)
		{
			_world.TagManager.Register( tag, this );
		}
	}
}
