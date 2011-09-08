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
		private static readonly Dictionary<Type, ComponentType> _sComponentTypes = new Dictionary<Type, ComponentType>();
		private static readonly Dictionary<int, ComponentType> _sComponentTypesByID = new Dictionary<int, ComponentType>();

		/// <summary>
		/// Gets the <see cref="ComponentType"/>.
		/// </summary>
		/// <typeparam name="T">The type of component to get the <see cref="ComponentType"/> for.</typeparam>
		/// <returns>The <see cref="ComponentType"/> for this IComponent</returns>
		public static ComponentType GetTypeFor<T>() where T : IComponent
		{
			return GetTypeFor( typeof( T ) );
		}
		/// <summary>
		/// Gets the <see cref="ComponentType"/>.
		/// </summary>
		/// <param name="componentType">Type of the component.</param>
		/// <returns>The <see cref="ComponentType"/> for this IComponent</returns>
		public static ComponentType GetTypeFor( Type componentType )
		{
			ComponentType type;
			if ( !_sComponentTypes.TryGetValue( componentType, out type ) )
			{
				type = new ComponentType();
				_sComponentTypes.Add( componentType, type );
				_sComponentTypesByID.Add( type.ID, type );
			}
			return type;
		}
		/// <summary>
		/// Gets the <see cref="ComponentType"/> for a specific ID.
		/// </summary>
		/// <param name="id">The ID.</param>
		/// <returns>The <see cref="ComponentType"/> if found; else <c>null</c>.</returns>
		public static ComponentType GetTypeFor( int id )
		{
			ComponentType type;
			_sComponentTypesByID.TryGetValue( id, out type );
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