using System.Collections;
using TDVS.Common;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Class for making unique types for <see cref="IComponent"/>
	/// </summary>
	public class ComponentType
	{
		private static int _nextId;

		/// <summary>
		/// Gets the bit.
		/// </summary>
		public BitArrayExt Bit { get; private set; }

		/// <summary>
		/// Gets the ID.
		/// </summary>
		public int ID { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentType"/> class.
		/// </summary>
		public ComponentType()
		{
			Init();
		}

		private void Init()
		{
			ID = _nextId++;
			Bit = new BitArrayExt( EntitySystem.MAX_NR_COMPONENT_TYPES );
			Bit.Set( ID, true );
		}
	}
}
