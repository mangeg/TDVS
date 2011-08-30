using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// A entity system that handles entities that are grouped together.
	/// </summary>
	public abstract class GroupSystem : EntitySystem
	{
		private String _group;

		/// <summary>
		/// Gets the group name.
		/// </summary>
		public String Group
		{
			get { return _group; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupSystem"/> class.
		/// </summary>
		/// <param name="group">The group name.</param>
		public GroupSystem( String group )
		{
			_group = group;
		}

		/// <summary>
		/// Processes the specified entity.
		/// </summary>
		/// <param name="e">The entity.</param>
		public abstract void Process( Entity e );
		/// <summary>
		/// Processes entities.
		/// </summary>
		/// <param name="entities"></param>
		public override void ProcessEntities( Dictionary<int, Entity> entities )
		{
			var list = _world.GroupManager.GetEntities( _group );
			foreach ( var item in list )
			{
				Process( item );
			}
		}
	}
}
