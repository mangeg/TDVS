using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	public sealed class GroupManager
	{
		private readonly List<Entity> _empty = new List<Entity>();
		private Dictionary<String, List<Entity>> _entitiesByGroup = new Dictionary<String, List<Entity>>();
		private Dictionary<int, String> _groupByEntities = new Dictionary<int, String>();

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupManager"/> class.
		/// </summary>
		/// <param name="world">The world.</param>
		public GroupManager( )
		{
		}

		/// <summary>
		/// Sets the group for a <see cref="Entity"/>.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <param name="e">The entity.</param>
		public void Set( String group, Entity e )
		{
			Remove( e );
			List<Entity> list;
			if ( !_entitiesByGroup.TryGetValue( group, out list ) )
			{
				list = new List<Entity>();
				_entitiesByGroup.Add( group, list );
			}
			list.Add( e );
			_groupByEntities[ e.ID ] = group;
		}
		/// <summary>
		/// Removes the specified entity from any group.
		/// </summary>
		/// <param name="e">The e.</param>
		public void Remove( Entity e )
		{
			if ( _groupByEntities.ContainsKey( e.ID ) )
			{
				var group = _groupByEntities[ e.ID ];
				_groupByEntities.Remove( e.ID );
				var list = _entitiesByGroup[ group ];
				list.Remove( e );
			}
		}
		/// <summary>
		/// Gets the entities for a specific group.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <returns>An list of entities.</returns>
		public List<Entity> GetEntities( String group )
		{
			if ( _entitiesByGroup.ContainsKey( group ) )
				return _entitiesByGroup[ group ];

			return _empty;
		}
		/// <summary>
		/// Gets the group name for a <see cref="Entity"/>.
		/// </summary>
		/// <param name="e">The e.</param>
		/// <returns>The name if the entity is grouped; else, <c>null</c></returns>
		public String GetGroup( Entity e )
		{
			if ( _groupByEntities.ContainsKey( e.ID ) )
				return _groupByEntities[ e.ID ];

			return null;
		}
		/// <summary>
		/// Determines whether the specified entity is grouped.
		/// </summary>
		/// <param name="e">The entity.</param>
		/// <returns>
		///   <c>true</c> if the specified e is grouped; otherwise, <c>false</c>.
		/// </returns>
		public bool IsGrouped( Entity e )
		{
			return _groupByEntities.ContainsKey( e.ID );
		}
	}
}
