using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Common
{
	public class Main : Microsoft.Xna.Framework.Game
	{
		public List<Entity> Entities { get; protected set; }

		public Main()
		{
			Entities = new List<Entity>();
		}

		public void AddEntity( Entity entity )
		{
			Entities.Add( entity );
		}
		public void RemoveEntity( Entity entity )
		{
			Entities.Remove( entity );
		}
		public Entity Get( String id )
		{
			return Entities.FirstOrDefault( c => c.ID == id );
		}
	}
}
