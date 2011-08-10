using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Screen
{
	public abstract class GameScreen
	{
		ScreenManager manager;

		public ScreenManager ScreenManager
		{
			get { return manager; }
			protected set { manager = value; }
		}

		public virtual void Activate() { }
		public virtual void Deactivate() { }
		public virtual void Unload() { }
		public virtual void HandleInput( GameTime gameTime ) { }
		public virtual void Update( GameTime gameTime ) { }
		public virtual void Draw( GameTime gameTime ) { }
	}
}
