using System;
using System.Collections.Generic;
using System.Collections;
using TDVS.Common;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Manager for type bits for <see cref="EntitySystem"/> types.
	/// </summary>
	public static class SystemBitManager
	{
		private static int _next;
		private static readonly Dictionary<Type, BitArrayExt> _sBits
			= new Dictionary<Type, BitArrayExt>();

		/// <summary>
		/// Gets the bit.
		/// </summary>
		/// <typeparam name="T">The type of the entity system.</typeparam>
		/// <returns>The bits for the type requested.</returns>
		public static BitArrayExt GetBit<T>() where T : EntitySystem
		{
			return GetBit( typeof( T ) );
		}

		public static BitArrayExt GetBit( Type type )
		{
			BitArrayExt res;
			if ( !_sBits.TryGetValue( type, out res ) )
			{
				res = new BitArrayExt( EntitySystem.MAX_NR_SYSTEM_TYPES );
				res.Set( _next++, true );
				_sBits.Add( type, res );
			}

			return res;
		}
	}
}
