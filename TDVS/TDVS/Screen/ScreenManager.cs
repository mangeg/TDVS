using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Screen.Menues;

namespace TDVS.Screen
{
	public class ScreenManager : DrawableGameComponent
	{
		private Texture2D blankTexture;
		private SpriteFont defaultFont;
		private SpriteBatch spriteBatch;
		private List<GameScreen> screens = new List<GameScreen>();

		public SpriteBatch SpriteBatch
		{
			get { return spriteBatch; }
		}
		public SpriteFont DefaultFont
		{
			get { return defaultFont; }
		}

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

			foreach ( var screen in screens )
			{
				screen.Update( gameTime );
			}
		}

		public override void Draw( GameTime gameTime )
		{
			base.Draw( gameTime );

			foreach ( var screen in screens )
			{
				screen.Draw( gameTime );
			}
		}

		public void AddScreen( GameScreen screen )
		{
			screen.ScreenManager = this;
			screens.Add( screen );
		}
	}
}
