using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TDVS.EntitySystem
{
	public interface IProperty
	{
		void AddBinding( IBinding b );
		void RemoveBinding( IBinding b );
	}

	public class Property<T> : IProperty
	{
		protected T _value;
		protected List<IBinding> _bindings = new List<IBinding>();
		protected Action<T> _set;

		[XmlIgnore]
		public bool IsInitializing { get; protected set; }
		[XmlIgnore]
		public Action<T> Set
		{
			get { return _set; }
			set
			{
				IsInitializing = true;
				_set = value;
				if ( _value != null && !_value.Equals( default( T ) ) )
				{
					_set( _value );
				}
				_value = default( T );
				IsInitializing = false;
			}
		}
		[XmlIgnore]
		public Func<T> Get { get; set; }
		public T Value
		{
			get
			{
				if ( Get != null )
					return Get();
				return _value;
			}
			set
			{

			}
		}

		internal void InternalSet( T obj, IBinding binding )
		{
			if ( Set != null )
				Set( obj );
			else
				_value = obj;

			foreach ( var b in _bindings )
			{
				if ( b == binding )
					b.OnChanged( this );
			}
		}
		public void AddBinding( IBinding binding )
		{
			if ( !_bindings.Contains( binding ) )
				_bindings.Add( binding );
		}
		public void RemoveBinding( IBinding binding )
		{
			_bindings.Remove( binding );
		}

		public static implicit operator T( Property<T> obj )
		{
			return obj._value;
		}

		public override string ToString()
		{
			return Value != null ? Value.ToString() : String.Empty;
		}
	}
}
