using System;
using System.Collections;

namespace TDVS.Common
{
	/// <summary>
	/// Extention class of the <see cref="BitArray"/> class to support |, & operators.
	/// </summary>
	public class BitArrayExt : ICloneable
	{
		private const int BitsPerInt32 = 32;
		//private const int BytesPerInt32 = 4;
		//private const int BitsPerByte = 8;

		private readonly int _size;
		private int _version;
		private readonly int[] _array;

		/// <summary>
		/// Gets the length in bits.
		/// </summary>
		public int Length
		{
			get { return _size; }
		}
		/// <summary>
		/// Gets the count in bits.
		/// </summary>
		public int Count
		{
			get { return _size; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BitArrayExt"/> class.
		/// </summary>
		/// <param name="size">The size in bits.</param>
		/// <param name="defaultValue">Default value for all bits.</param>
		public BitArrayExt( int size, bool defaultValue )
		{
			_size = size;
			_array = new int[ GetArrayLength( size, BitsPerInt32 ) ];
			_version = 0;

			int value = defaultValue ? unchecked( ( ( int )0xffffffff ) ) : 0;

			for ( var i = 0; i < _array.Length; i++ )
			{
				_array[ i ] = value;
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BitArrayExt"/> class.
		/// </summary>
		/// <param name="size">The size in bits.</param>
		public BitArrayExt( int size )
			: this( size, false )
		{
		}

		/// <summary>
		/// Gets or sets the <see cref="System.Boolean"/> at the specified index.
		/// </summary>
		public bool this[ int index ]
		{
			get { return Get( index ); }
			set { Set( index, value ); }
		}

		/// <summary>
		/// Gets the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public bool Get( int index )
		{
			return ( _array[ index / BitsPerInt32 ] & ( 1 << ( index % BitsPerInt32 ) ) ) != 0;
		}
		/// <summary>
		/// Sets the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public void Set( int index, bool value )
		{
			if ( value )
			{
				_array[ index / BitsPerInt32 ] |= ( 1 << ( index % BitsPerInt32 ) );
			}
			else
			{
				_array[ index / BitsPerInt32 ] &= ~( 1 << ( index % BitsPerInt32 ) );
			}
		}

		/// <summary>
		/// Ands the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Returns a reference to the current instance ANDed with value.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public BitArrayExt And( BitArrayExt value )
		{
			if ( value == null )
				throw new ArgumentNullException( "value" );
			if ( Length != value.Length )
				throw new ArgumentException( "Not the same array lenghts", "value" );

			int ints = GetArrayLength( _size, BitsPerInt32 );
			for ( var i = 0; i < ints; i++ )
			{
				_array[ i ] &= value._array[ i ];
			}

			_version++;
			return this;
		}
		/// <summary>
		/// Ors the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A reference to the current instance ORed with value. </returns>
		public BitArrayExt Or( BitArrayExt value )
		{
			if ( value == null )
				throw new ArgumentNullException( "value" );
			if ( Length != value.Length )
				throw new ArgumentException( "Not the same array lenghts", "value" );

			int ints = GetArrayLength( _size, BitsPerInt32 );
			for ( var i = 0; i < ints; i++ )
			{
				_array[ i ] |= value._array[ i ];
			}

			_version++;
			return this;
		}
		/// <summary>
		/// Xors the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A reference to the current instance XORed with value.</returns>
		public BitArrayExt Xor( BitArrayExt value )
		{
			if ( value == null )
				throw new ArgumentNullException( "value" );
			if ( Length != value.Length )
				throw new ArgumentException( "Not the same array lenghts", "value" );

			int ints = GetArrayLength( _size, BitsPerInt32 );
			for ( var i = 0; i < ints; i++ )
			{
				_array[ i ] ^= value._array[ i ];
			}

			_version++;
			return this;
		}
		/// <summary>
		/// Inverts all the bit values. On/true bit values are converted to 
		/// off/false. Off/false bit values are turned on/true.
		/// </summary>
		/// <returns>The current instance is updated and returned</returns>
		public BitArrayExt Not()
		{
			int ints = GetArrayLength( _size, BitsPerInt32 );
			for ( var i = 0; i < ints; i++ )
			{
				_array[ i ] = ~_array[ i ];
			}

			_version++;
			return this;
		}
		/// <summary>
		/// Sets all bits.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public void SetAll( bool value )
		{
			var ints = GetArrayLength( _size, BitsPerInt32 );
			int v = value ? unchecked( ( ( int )0xffffffff ) ) : 0;
			for ( var i = 0; i < ints; i++ )
			{
				_array[ i ] = v;
			}
		}

		/// <summary>
		/// Implements the operator &amp;.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static BitArrayExt operator &( BitArrayExt first, BitArrayExt second )
		{
			var ret = new BitArrayExt( first._size, false );
			int ints = GetArrayLength( first._size, BitsPerInt32 );
			for ( int i = 0; i < ints; i++ )
			{
				ret._array[ i ] = first._array[ i ] & second._array[ i ];
			}

			return ret;
		}
		/// <summary>
		/// Implements the operator |.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static BitArrayExt operator |( BitArrayExt first, BitArrayExt second )
		{
			var ret = new BitArrayExt( first._size, false );
			int ints = GetArrayLength( first._size, BitsPerInt32 );
			for ( int i = 0; i < ints; i++ )
			{
				ret._array[ i ] = first._array[ i ] | second._array[ i ];
			}

			return ret;
		}
		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=( BitArrayExt first, BitArrayExt second )
		{
			return !Equals( first, second );
		}
		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==( BitArrayExt first, BitArrayExt second )
		{
			return Equals( first, second );
		}

		private static int GetArrayLength( int n, int div )
		{
			return n > 0 ? ( ( ( n - 1 ) / div ) + 1 ) : 0;
		}

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public object Clone()
		{
			var ret = new BitArrayExt( _size ) { _version = _version };
			Array.Copy( _array, ret._array, _array.Length );
			return ret;
		}

		/// <summary>
		/// Equalses the specified other.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public bool Equals( BitArrayExt other )
		{
			if ( ReferenceEquals( null, other ) ) return false;
			if ( ReferenceEquals( this, other ) ) return true;

			if ( _size != other._size )
				return false;

			var ints = GetArrayLength( _size, BitsPerInt32 );
			for ( var i = 0; i < ints; i++ )
			{
				if ( _array[ i ] != other._array[ i ] )
					return false;
			}
			return true;
		}
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals( object obj )
		{
			if ( ReferenceEquals( null, obj ) ) return false;
			if ( ReferenceEquals( this, obj ) ) return true;
			return obj.GetType() == typeof( BitArrayExt ) && Equals( ( BitArrayExt )obj );
		}
		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var result = _size;
				result = ( result * 397 ) ^ _version;
				result = ( result * 397 ) ^ ( _array != null ? _array.GetHashCode() : 0 );
				return result;
			}
		}
	}
}