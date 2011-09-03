using System;
using System.Collections.Generic;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Manager for tag entity systems.
	/// </summary>
	public sealed class TagManager
	{
		private readonly Dictionary<String, Entity> _entityByTag = new Dictionary<String, Entity>();

		/// <summary>
		/// Registers the specified tag.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <param name="e">The entity.</param>
		public void Register( String tag, Entity e )
		{
			_entityByTag.Add( tag, e );
		}
		/// <summary>
		/// Unregisters the specified tag.
		/// </summary>
		/// <param name="tag">The tag.</param>
		public void Unregister( String tag )
		{
			_entityByTag.Remove( tag );
		}
		/// <summary>
		/// Determines whether the specified tag is registered.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <returns>
		///   <c>true</c> if the specified tag is registered; otherwise, <c>false</c>.
		/// </returns>
		public bool IsRegistered( String tag )
		{
			return _entityByTag.ContainsKey( tag );
		}
		/// <summary>
		/// Gets the entity.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <returns><see cref="Entity"/> if found; else <c>null</c></returns>
		public Entity GetEntity( String tag )
		{
			if ( _entityByTag.ContainsKey( tag ) )
				return _entityByTag[ tag ];
			return null;
		}
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <param name="e">The etity to get the tag for.</param>
		/// <returns><see cref="String"/> for the tag if found; else <c>String.Empty</c></returns>
		public String GetTag( Entity e )
		{
			foreach ( var item in _entityByTag )
			{
				if ( item.Value.ID == e.ID )
					return item.Key;
			}

			return String.Empty;
		}
	}
}
