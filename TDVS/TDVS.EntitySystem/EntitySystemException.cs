using System;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// General exception in entity systems.
	/// </summary>
	public class EntitySystemException : Exception
	{
		/// <summary>
		/// Gets or sets the name of the system.
		/// </summary>
		/// <value>
		/// The name of the system.
		/// </value>
		public String SystemName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EntitySystemException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="systemType">Type of the system.</param>
		public EntitySystemException( String message, Type systemType )
			: base( message )
		{
			SystemName = systemType.Name;
		}
	}
}