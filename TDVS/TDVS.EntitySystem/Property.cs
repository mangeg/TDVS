using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Interface for properties.
	/// </summary>
	public interface IProperty
	{
		/// <summary>
		/// Adds a binding.
		/// </summary>
		/// <param name="b">The binding.</param>
		void AddBinding( IBinding b );
		/// <summary>
		/// Removes a binding.
		/// </summary>
		/// <param name="b">The binding.</param>
		void RemoveBinding( IBinding b );
	}

	/// <summary>
	/// Class for holding properties that are bindable.
	/// </summary>
	/// <typeparam name="T">The type that this property should wrap.</typeparam>
	public class Property<T> : IProperty
	{
		private T _value;
		private List<IBinding> _bindings = new List<IBinding>();
		private Action<T> _set;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is initializing.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is initializing; otherwise, <c>false</c>.
		/// </value>
		[XmlIgnore]
		public bool IsInitializing { get; protected set; }
		/// <summary>
		/// Gets or sets the set-action.
		/// </summary>
		/// <value>
		/// The set-action.
		/// </value>
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
		/// <summary>
		/// Gets or sets the Get-function.
		/// </summary>
		/// <value>
		/// The get-function.
		/// </value>
		[XmlIgnore]
		public Func<T> Get { get; set; }
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
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
		/// <summary>
		/// Adds the binding.
		/// </summary>
		/// <param name="binding">The binding.</param>
		public void AddBinding( IBinding binding )
		{
			if ( !_bindings.Contains( binding ) )
				_bindings.Add( binding );
		}
		/// <summary>
		/// Removes the binding.
		/// </summary>
		/// <param name="binding">The binding.</param>
		public void RemoveBinding( IBinding binding )
		{
			_bindings.Remove( binding );
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="TDVS.EntitySystem.Property&lt;T&gt;"/> to <typeparamref name="T"/>.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator T( Property<T> obj )
		{
			return obj._value;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Value != null ? Value.ToString() : String.Empty;
		}
	}
}
