using System;
using System.Collections;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Class for making unique types for <see cref="Component"/>
	/// </summary>
	public class ComponentType
	{
		private static int _nextId = 0;

		private BitArray _bit;
		private int _id;

		/// <summary>
		/// Gets the bit.
		/// </summary>
		public BitArray Bit
		{
			get { return _bit; }
		}
		/// <summary>
		/// Gets the ID.
		/// </summary>
		public int ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentType"/> class.
		/// </summary>
		public ComponentType()
		{
			Init();
		}

		private void Init()
		{
			_id = _nextId++;			
			_bit = new BitArray( EntitySystem.MAX_COMPONENT_TYPE_BITS );
			_bit.Set( _id, true );
		}
	}
}
