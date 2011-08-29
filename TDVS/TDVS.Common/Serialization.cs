using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TDVS.Common
{
	public static class Serialization
	{
		/// <summary>
		/// Serialize an object to a byte[] with a BinaryFormatter
		/// </summary>
		/// <param name="o">Object to serialize</param>
		/// <returns>A byte[] representing the object</returns>
		public static byte[] ObjectToByteArray( object o )
		{
			try
			{
				// Create a disposable MemoryStream to hold the object
				using ( var memoryStream = new MemoryStream() )
				{
					// Create a BinaryFormatter to serialize the object into the MemoryStream
					var binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize( memoryStream, o );

					// Return the MemoryStream as a byte[]
					return memoryStream.ToArray();
				}
			}
			catch ( Exception exception )
			{
				Console.WriteLine( "Exception caught in process: {0}", exception );
				throw exception;
			}
		}

		/// <summary>
		/// Deserialize a byte[] into an object of type T with a BinaryFormatter
		/// </summary>
		/// <typeparam name="T">The type to deserialize to</typeparam>
		/// <param name="bytes">The serialized object</param>
		/// <returns>A deserialized object of type T</returns>
		public static T ByteArrayToObject<T>( byte[] bytes )
		{
			try
			{
				// Create a disposable MemoryStream to hold the object
				using ( var memoryStream = new MemoryStream() )
				{
					var binaryFormatter = new BinaryFormatter();

					memoryStream.Read( bytes, 0, bytes.Length );
					return ( T )binaryFormatter.Deserialize( memoryStream );
				}

			}
			catch ( Exception exception )
			{
				Console.WriteLine( "Exception caught in process: {0}", exception );
				throw exception;
			}
		}
	}
}
