using System.Collections;

namespace TDVS.Common.Extensions
{
	/// <summary>
	/// Extensions for <see cref="BitArray"/>
	/// </summary>
	public static class BitArrayExtensions
	{
		/// <summary>
		/// Makes a clone of the BitArray and then performs the Or-operation.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="value">The value.</param>
		/// <returns>The cloned result of the Or-operation</returns>
		public static BitArray OrCopy( this BitArray array, BitArray value )
		{
			var copy = ( BitArray )array.Clone();
			return copy.Or( value );
		}
		/// <summary>
		/// Makes a clone of the BitArray and then performs the And-operation
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="value">The value.</param>
		/// <returns>The cloned result of the And-operation</returns>
		public static BitArray AndCopy( this BitArray array, BitArray value )
		{
			var copy = ( BitArray )array.Clone();
			return copy.And( value );
		}
		/// <summary>
		/// Makes an And test.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the the array contains the bits from the value; else, <c>false</c></returns>
		public static bool AndTest( this BitArray array, BitArray value )
		{
			var copy = ( BitArray )array.Clone();
			return copy.And( value ) == copy;
		}
	}
}
