using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDVS.Screen
{
	public class ScreenManager : DrawableGameComponent
	{
		private Texture2D blankTexture;
		private SpriteFont defaultFont;
		private SpriteBatch spriteBatch;

		public ScreenManager( Game game )
			: base( game )
		{

		}

		public override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			spriteBatch = new SpriteBatch( GraphicsDevice );
			blankTexture = Game.Content.Load<Texture2D>( @"Textures\blank" );
			defaultFont = Game.Content.Load<SpriteFont>( @"Fonts\DefaultMenuFont" );
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );
		}

		public override void Draw( GameTime gameTime )
		{
			base.Draw( gameTime );

			spriteBatch.Begin();		
			spriteBatch.End();
		}
	}
}
