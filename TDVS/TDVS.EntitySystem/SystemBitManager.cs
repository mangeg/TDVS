using System;
using System.Collections.Generic;
using System.Collections;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Manager for type bits for <see cref="EntitySystem"/> types.
	/// </summary>
	public static class SystemBitManager
	{
		private static int _next;
		private static readonly Dictionary<Type, BitArray> _sBits
			= new Dictionary<Type, BitArray>();

		/// <summary>
		/// Gets the bit.
		/// </summary>
		/// <typeparam name="T">The type of the entity system.</typeparam>
		/// <returns>The bits for the type requested.</returns>
		public static BitArray GetBit<T>() where T : EntitySystem
		{
			BitArray res;
			if ( !_sBits.TryGetValue( typeof( T ), out res ) )
			{
				res = new BitArray( EntitySystem.MAX_NR_SYSTEM_TYPES );
				res.Set( _next++, true );
				_sBits.Add( typeof( T ), res );
			}

			return res;
		}
	}
}
