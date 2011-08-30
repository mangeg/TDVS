using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	public interface IBinding
	{
		void OnChanged( IProperty p );
	}

	public class Binding<T1, T2> : IBinding
	{
		protected Func<T1> _get;
		protected Property<T1> _dest;
		protected IProperty[] _sources;

		protected Binding()
		{
		}
		public Binding( Property<T1> dest, Func<T2, T1> trans, Property<T2> source )
		{
			_dest = dest;
			source.AddBinding( this );
			_get = () => trans( source.Value );
			_sources = new IProperty[] { source };
			OnChanged( source );
		}

		public void OnChanged( IProperty p )
		{
			_dest.InternalSet( _get(), this );
		}
		public void Delete()
		{
			foreach ( var source in _sources )
			{
				source.RemoveBinding( this );
			}
		}		
	}

	public class Binding<T> : Binding<T, T>
	{
		public Binding(Property<T> dest, Property<T> source)
			: base( dest, x => x, source )
		{
		}
		public Binding( Property<T> dest, Func<T> get, Property<T>[] sources )
		{
			_dest = dest;
			_get = get;
			_sources = sources;
			foreach ( var property in _sources )
			{
				property.AddBinding( this );
			}
			if ( _sources.Length > 0 )
			{
				OnChanged( _sources[ 0 ] );
			}
		}
	}
}
