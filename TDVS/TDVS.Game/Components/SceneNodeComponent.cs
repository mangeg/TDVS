using System.Collections.Generic;
using TDVS.EntitySystem;

namespace TDVS.Game.Components
{
	public class SceneNodeComponent : IComponent 
	{
		public Entity Parent { get; set; }
		public List<Entity> Children { get; set; }

		public SceneNodeComponent()
		{
			Reset();
		}

		public void Reset()
		{
			Parent = null;
			if(Children != null)
				Children.Clear();
			else
			{
				Children = new List<Entity>();
			}
		}
	}
}
