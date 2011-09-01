using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Interface for bindings.
	/// </summary>
	public interface IBinding
	{
		/// <summary>
		/// Called when the bound property changed.
		/// </summary>
		/// <param name="p">The property.</param>
		void OnChanged( IProperty p );
	}

	/// <summary>
	/// Binding class between 2 different property types.
	/// </summary>
	/// <typeparam name="T1">The type of the source property.</typeparam>
	/// <typeparam name="T2">The type of the target property.</typeparam>
	public class Binding<T1, T2> : IBinding
	{
		/// <summary>
		/// The get-function.
		/// </summary>
		protected Func<T1> _get;
		/// <summary>
		/// The destination property.
		/// </summary>
		protected Property<T1> _dest;
		/// <summary>
		/// The source properties.
		/// </summary>
		protected IProperty[] _sources;

		/// <summary>
		/// Initializes a new instance of the <see cref="Binding&lt;T1, T2&gt;"/> class.
		/// </summary>
		protected Binding()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Binding&lt;T1, T2&gt;"/> class.
		/// </summary>
		/// <param name="dest">The destination property.</param>
		/// <param name="trans">The transition function.</param>
		/// <param name="source">The source property.</param>
		public Binding( Property<T1> dest, Func<T2, T1> trans, Property<T2> source )
		{
			_dest = dest;
			source.AddBinding( this );
			_get = () => trans( source.Value );
			_sources = new IProperty[] { source };
			OnChanged( source );
		}

		/// <summary>
		/// Called when the bound property changed.
		/// </summary>
		/// <param name="p">The property.</param>
		public void OnChanged( IProperty p )
		{
			_dest.InternalSet( _get(), this );
		}
		/// <summary>
		/// Deletes this binding and removes it from all sources.
		/// </summary>
		public void Delete()
		{
			foreach ( var source in _sources )
			{
				source.RemoveBinding( this );
			}
		}		
	}

	/// <summary>
	/// Binding class between 2 of the same property types.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Binding<T> : Binding<T, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Binding&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="dest">The dest.</param>
		/// <param name="source">The source.</param>
		public Binding(Property<T> dest, Property<T> source)
			: base( dest, x => x, source )
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Binding&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="dest">The dest.</param>
		/// <param name="get">The get-function.</param>
		/// <param name="sources">The sources.</param>
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
