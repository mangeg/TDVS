using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Abstract class for a tag system.
	/// </summary>
	public abstract class TagSystem : EntitySystem
	{
		private String _group;

		/// <summary>
		/// Gets the group.
		/// </summary>
		public String Group
		{
			get { return _group; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TagSystem"/> class.
		/// </summary>
		/// <param name="group">The group.</param>
		public TagSystem( String group )
		{
			_group = group;
		}

		/// <summary>
		/// Processes the specified etity.
		/// </summary>
		/// <param name="e">The entity.</param>
		public abstract void Process( Entity e );
		/// <summary>
		/// Processes entities.
		/// </summary>
		/// <param name="entities"></param>
		public override void ProcessEntities( Dictionary<int, Entity> entities )
		{
			var e = _world.TagManager.GetEntity( _group );
			if ( e != null )
			{
				Process( e );
			}
		}
	}
}
