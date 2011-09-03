using System;
using System.Collections.Generic;
using System.Collections;
using TDVS.Common;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Static manager for component types.
	/// </summary>
	public static class ComponentTypeManager
	{
		private static readonly Dictionary<Type, ComponentType> _sSComponentTypes = new Dictionary<Type, ComponentType>();

		/// <summary>
		/// Gets the <see cref="ComponentType"/>.
		/// </summary>
		/// <typeparam name="T">The type of component to get the <see cref="ComponentType"/> for.</typeparam>
		/// <returns>The <see cref="ComponentType"/> for this IComponent</returns>
		public static ComponentType GetTypeFor<T>() where T : IComponent
		{
			ComponentType type;
			Type toFind = typeof( T );
			if ( !_sSComponentTypes.TryGetValue( toFind, out type ) )
			{
				type = new ComponentType();
				_sSComponentTypes.Add( toFind, type );
			}

			return type;
		}
		/// <summary>
		/// Gets the <see cref="ComponentType"/>.
		/// </summary>
		/// <param name="componentType">Type of the component.</param>
		/// <returns>The <see cref="ComponentType"/> for this IComponent</returns>
		public static ComponentType GetTypeFor( Type componentType )
		{
			ComponentType type;
			if ( !_sSComponentTypes.TryGetValue( componentType, out type ) )
			{
				type = new ComponentType();
				_sSComponentTypes.Add( componentType, type );
			}
			return type;
		}

		/// <summary>
		/// Gets the bit.
		/// </summary>
		/// <typeparam name="T">The type of component to get the <see cref="BitArray"/> for.</typeparam>
		/// <returns>The <see cref="BitArray"/> for this component type</returns>
		public static BitArrayExt GetBit<T>() where T : IComponent
		{
			return GetTypeFor<T>().Bit;
		}
		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <typeparam name="T">The type of component to get the ID for.</typeparam>
		/// <returns>The ID for this component type</returns>
		public static int GetID<T>() where T : IComponent
		{
			return GetTypeFor<T>().ID;
		}
	}
}
