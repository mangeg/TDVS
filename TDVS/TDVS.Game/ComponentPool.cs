using System;
using System.Collections.Generic;
using TDVS.Common.Extensions;
using TDVS.EntitySystem;

namespace TDVS.Game
{
	public class ComponentPool
	{
		private readonly Dictionary<Type, List<IComponent>> _componetPool
			= new Dictionary<Type, List<IComponent>>();
		private readonly int _initialCount;

		public ComponentPool( params Type[] componentTypes )
		{
			_initialCount = 10;
			foreach ( var item in componentTypes )
			{
				_componetPool.Add( item, new List<IComponent>() );
			}
		}

		public void Initialize()
		{
			foreach ( var type in _componetPool.Keys )
			{
				Populate( type, _initialCount );
			}
		}

		private void Populate( Type type, int count )
		{
			for ( var i = 0; i < count; i++ )
			{
				Return( ( IComponent )Activator.CreateInstance( type, true ) );
			}
		}

		public T Take<T>() where T : IComponent
		{
			var type = typeof( T );
			if ( _componetPool.ContainsKey( type ) )
			{
				var component = _componetPool[ type ].PopSafe();
				if ( Equals( component, default( T ) ) )
				{
					Populate( type, ( int )( _initialCount * 0.25 ) );
					component = _componetPool[ type ].Pop();
				}

				component.Reset();

				return ( T )component;
			}

			return default( T );
		}

		public void Return( IComponent c )
		{
			var type = c.GetType();
			if ( _componetPool.ContainsKey( type ) )
			{
				_componetPool[ type ].Add( c );
			}
		}
	}
}
