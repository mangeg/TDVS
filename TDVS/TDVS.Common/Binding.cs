using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Common
{
	public interface IBinding
	{
		void OnChanged( IProperty changed );
	}

	public class Binding<T1, T2> : IBinding
	{
		protected Func<T1> _Get;
		protected Property<T1> _Destination;
		protected IProperty[] _Sources;

		protected Binding()
		{
		}

		public Binding( Property<T1> destination, Func<T2, T1> transform, Property<T2> source )
		{
			_Destination = destination;
			source.AddBinding( this );
			_Get = () => transform( source.Value );
			_Sources = new IProperty[] { source };
			OnChanged( source );
		}

		public virtual void OnChanged( IProperty changed )
		{
			_Destination.InternalSet( _Get(), this );
		}
		public virtual void Delete()
		{
			foreach ( var source in _Sources )
			{
				source.RemoveBinding( this );
			}
		}
	}

	public class Binding<T> : Binding<T, T>
	{
		public Binding( Property<T> destination, Property<T> source )
			: base( destination, x => x, source )
		{

		}

		public Binding( Property<T> destination, Func<T> get, params Property<T>[] sources )
		{
			_Destination = destination;
			_Get = get;
			_Sources = sources;
			foreach ( var property in _Sources )
			{
				property.AddBinding( this );
			}
			if ( _Sources.Length > 0 )
				OnChanged( _Sources[ 0 ] );
		}
	}
}
