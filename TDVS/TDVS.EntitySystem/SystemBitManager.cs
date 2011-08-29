using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TDVS.EntitySystem
{
	public static class SystemBitManager
	{
		private static int _next;
		private static Dictionary<Type, BitArray> _bits
			= new Dictionary<Type, BitArray>();

		public static BitArray GetBit<T>() where T : EntitySystem
		{
			BitArray res;
			if ( !_bits.TryGetValue( typeof( T ), out res ) )
			{
				res = new BitArray( EntitySystem.MAX_SYSTEM_TYPE_BITS );
				res.Set( _next++, true );
				_bits.Add( typeof( T ), res );
			}

			return res;
		}
	}
}
