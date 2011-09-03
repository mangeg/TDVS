using System;
using System.Collections.Generic;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Entity system that processes all enteties one by one.
	/// </summary>
	public abstract class EntityProcessingSystem : EntitySystem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityProcessingSystem"/> class.
		/// </summary>
		/// <param name="reqType">Rquired type.</param>
		/// <param name="types">Other types.</param>
		protected EntityProcessingSystem(Type reqType, params Type [] types)
			: base( GetTypes( reqType, types ) )
		{
		}

		/// <summary>
		/// Processes the specified etity.
		/// </summary>
		/// <param name="e">The etity.</param>
		public abstract void Process( Entity e );

		/// <summary>
		/// Processes entities.
		/// </summary>
		/// <param name="entities">The enteties.</param>
		public override void ProcessEntities( Dictionary<int, Entity> entities )
		{
			foreach ( var item in entities )
			{
				Process( item.Value );
			}
		}
	}
}
