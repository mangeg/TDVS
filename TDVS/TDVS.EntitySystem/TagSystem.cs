using System;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Abstract class for a tag system.
	/// </summary>
	public abstract class TagSystem : EntitySystem
	{
		private readonly String _tag;

		/// <summary>
		/// Gets the Tag.
		/// </summary>
		public String Tag
		{
			get { return _tag; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TagSystem"/> class.
		/// </summary>
		/// <param name="tag">The Tag.</param>
		protected TagSystem( String tag )
		{
			_tag = tag;
		}

		/// <summary>
		/// Processes the specified etity.
		/// </summary>
		/// <param name="e">The entity.</param>
		public abstract void Process( Entity e );
		/// <summary>
		/// Processes all entities for this system.
		/// Override to change behavior.
		/// </summary>
		public override void Process()
		{
			if ( !CheckProcessing() ) return;

			Begin();
			var e = _worldBase.TagManager.GetEntity( _tag );
			if ( e != null )
			{
				Process( e );
			}
			End();
		}
	}
}
