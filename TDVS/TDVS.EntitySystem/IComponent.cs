using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Interface for components.
	/// </summary>
	public interface IComponent
	{
		/// <summary>
		/// Resets this instance and sets default values.
		/// </summary>
		void Reset();
	}
}
