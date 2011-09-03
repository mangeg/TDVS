using System.Collections.Generic;

namespace TDVS.Common.Extensions
{
	/// <summary>
	/// Collection of IList&lt;T&gt; extensions.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Pops a value from the list.
		/// </summary>
		/// <typeparam name="T">Types stored in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns>The popped item.</returns>
		public static T Pop<T>( this IList<T> list )
		{
			var index = list.Count - 1;
			var item = list[ index ];
			list.RemoveAt( index );
			return item;
		}
		/// <summary>
		/// Pops a value from the list in a safe manner.
		/// </summary>
		/// <typeparam name="T">Types stored in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns>The popped item if list is not empty, else default..</returns>
		public static T PopSafe<T>( this IList<T> list )
		{
			var item = default( T );

			if ( list.Count > 0 )
			{
				var index = list.Count - 1;
				item = list[ index ];
				list.RemoveAt( index );
			}

			return item;
		}
		/// <summary>
		/// Pops the top value from the list.
		/// </summary>
		/// <typeparam name="T">Types stored in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns>The top item from the list</returns>
		public static T PopTop<T>( this IList<T> list )
		{
			var item = list[ 0 ];
			list.RemoveAt( 0 );
			return item;
		}
		/// <summary>
		/// Pops the top value from the list in a safe manner.
		/// </summary>
		/// <typeparam name="T">Types stored in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns>The top item from the list if list is not empty, else default</returns>
		public static T PopTopSafe<T>( this IList<T> list )
		{
			var item = default( T );

			if ( list.Count > 0 )
			{
				item = list[ 0 ];
				list.RemoveAt( 0 );
			}

			return item;
		}
	}
}
