using System;
using System.Collections.Generic;
using System.Linq;

namespace TDVS.Common.Extensions
{
	public static class TypeExtensions
	{
		public static IEnumerable<Type> GetDerivedTypes( this Type type )
		{
			if ( !( type.IsClass || type.IsInterface ) )
				return Type.EmptyTypes;

			var asms = AppDomain.CurrentDomain.GetAssemblies();
			var types = asms
				.SelectMany( a => a.GetTypes() )
				.Where( type.IsAssignableFrom )
				.Where( t => t != type );

			return types;
		}
	}
}
