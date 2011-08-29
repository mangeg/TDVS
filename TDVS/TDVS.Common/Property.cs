using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TDVS.Common
{
	public interface IProperty
	{
		void AddBinding( IBinding binding );
		void RemoveBinding( IBinding binding );
	}

	public class Property<T> : IProperty
	{
		protected T _Value;
		private bool _IsEditable;
		protected List<IBinding> _Bindings = new List<IBinding>();
		protected Action<T> set;

		[XmlIgnore]
		public bool IsInitializing { get; protected set; }

		[XmlIgnore]
		public Func<T> Get { get; set; }
		[XmlIgnore]
		public Action<T> Set
		{
			get { return set; }
			set
			{
				IsInitializing = true;
				set = value;
				if ( _Value != null && !_Value.Equals( default( T ) ) )
					set( _Value );
				_Value = default( T );
				IsInitializing = false;
			}
		}

		[XmlAttribute]
		public bool IsEditable
		{
			get { return _IsEditable; }
			set { _IsEditable = value; }
		}

		public T Value
		{
			get
			{
				if ( Get != null )
					return Get();
				return _Value;
			}
			set
			{
				InternalSet( value, null );
			}
		}

		public void InternalSet( T obj, IBinding binding )
		{
			if ( Set != null )
				Set( obj );
			else
				_Value = obj;

			foreach ( var b in _Bindings )
			{
				if ( b != binding )
					b.OnChanged( this );
			}
		}

		public void AddBinding( IBinding binding )
		{
			if ( !_Bindings.Contains( binding ) )
				_Bindings.Add( binding );
		}
		public void RemoveBinding( IBinding binding )
		{
			_Bindings.Remove( binding );
		}

		public static implicit operator T( Property<T> obj )
		{
			return obj.Value;
		}

		public override string ToString()
		{
			return Value != null ? Value.ToString() : "";
		}
	}
}
