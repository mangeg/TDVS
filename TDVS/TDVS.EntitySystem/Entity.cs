using System;
using System.Xml.Serialization;
using System.Collections;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// The basic entity class for all game entities.
	/// </summary>
	[Serializable]
	public class Entity
	{
		private int _id;

		private BitArray _typeBits = new BitArray( EntitySystem.MAX_NR_COMPONENT_TYPES );
		private BitArray _systemBits = new BitArray( EntitySystem.MAX_NR_COMPONENT_TYPES );
		
		private EntityManager _entityManager;

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
		public BitArray TypeBits
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
		public BitArray SystemBits
		{
			get { return _systemBits; }
			set { _systemBits = value; }
		}

		/// <summary>
		/// Sets the world.
		/// </summary>
		/// <value>
		/// The world.
		/// </value>
		[XmlIgnore]
		internal World World
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
		/// <param name="world">The world.</param>
		/// <param name="id">The id.</param>
		public Entity( World world, int id )
		{
			_entityManager = world.EntityManager;
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
	}
}
