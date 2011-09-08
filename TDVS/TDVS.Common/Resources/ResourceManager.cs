using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace TDVS.Common.Resources
{
	public static class ResourceManager
	{
		private static ContentManager _content;
		private static readonly Dictionary<ResourceID, object> _sIDToResource = new Dictionary<ResourceID, object>();
		private static readonly Dictionary<String, ResourceID> _sNameToID = new Dictionary<string, ResourceID>();

		public static void Initialize( ContentManager content )
		{
			_content = content;
		}

		public static T Get<T>( ResourceID id )
		{
			if ( _sIDToResource.ContainsKey( id ) && _sIDToResource[ id ] is T )
				return ( T )_sIDToResource[ id ];

			return default( T );
		}
		public static T Get<T>( String path )
		{
			if ( _sNameToID.ContainsKey( path ) && _sIDToResource.ContainsKey( _sNameToID[ path ] ) )
			{
				if ( _sIDToResource[ _sNameToID[ path ] ] is T )
					return ( T )_sIDToResource[ _sNameToID[ path ] ];
			}

			var id = new ResourceID();
			var obj = _content.Load<T>( path );

			_sIDToResource.Add( id, obj );
			_sNameToID.Add( path, id );

			return obj;
		}
		public static ResourceID GetID<T>( String path )
		{
			if ( _sNameToID.ContainsKey( path ) )
				return _sNameToID[ path ];

			var id = new ResourceID();
			var obj = _content.Load<T>( path );

			_sIDToResource.Add( id, obj );
			_sNameToID.Add( path, id );

			return id;
		}
	}
}
