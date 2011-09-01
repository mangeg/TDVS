using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.EntitySystem;

namespace TDVS.Game.EntitySystem
{
	public class ComponentPool
	{
		private Dictionary<Type, List<IComponent>> _componetPool
			= new Dictionary<Type, List<IComponent>>();
		private int _initialCount;

		public ComponentPool()
		{
			_initialCount = 10;
		}

		public void Initialize()
		{
			foreach ( var type in _componetPool.Keys )
			{
				Populate( type, _initialCount );
			}
		}

		private void Populate(Type type, int count)
		{
			for ( int i = 0; i < count; i++ )
			{
				Return( ( IComponent )Activator.CreateInstance( type ) );
			}
		}

		public T Take<T>() where T : IComponent
		{
			var type = typeof( T );
			if ( _componetPool.ContainsKey( type ) )
			{
				IComponent component = _componetPool[ type ].LastOrDefault();
				if ( component == null )
				{
					Populate( type, ( int )( _initialCount * 0.25 ) );
					component = _componetPool[ type ].LastOrDefault();
				}

				_componetPool[ type ].Remove( component );

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
