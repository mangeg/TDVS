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
		private Texture2D _BlankTexture;
		private SpriteFont _DefaultFont;
		private SpriteBatch _SpriteBatch;
		private List<GameScreen> _TempScreens = new List<GameScreen>();
		private List<GameScreen> _Screens = new List<GameScreen>();

		public SpriteBatch SpriteBatch
		{
			get { return _SpriteBatch; }
		}
		public SpriteFont DefaultFont
		{
			get { return _DefaultFont; }
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

			_SpriteBatch = new SpriteBatch( GraphicsDevice );
			_BlankTexture = Game.Content.Load<Texture2D>( @"Textures\blank" );
			_DefaultFont = Game.Content.Load<SpriteFont>( @"Fonts\DefaultMenuFont" );
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );

			_TempScreens.Clear();

			foreach ( var item in _Screens )
			{
				_TempScreens.Add( item );
			}

			bool otherScreenHasFocus = !Game.IsActive;
			bool coveredByOtherScreen = false;

			for ( int i = _TempScreens.Count - 1; i >= 0; i-- )
			{
				var screen = _TempScreens[ i ];

				screen.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

				if ( screen.ScreenState == GameScreenState.TransitionOn ||
					screen.ScreenState == GameScreenState.Active )
				{
					if ( !otherScreenHasFocus )
					{
						screen.HandleInput( gameTime );
						otherScreenHasFocus = true;
					}

					if ( !screen.IsPopup )
					{
						coveredByOtherScreen = true;
					}
				}
			}
		}

		public override void Draw( GameTime gameTime )
		{
			base.Draw( gameTime );

			foreach ( var screen in _Screens )
			{
				if ( screen.ScreenState == GameScreenState.Hidden )
					continue;

				screen.Draw( gameTime );
			}
		}

		public void AddScreen( GameScreen screen )
		{
			screen.ScreenManager = this;
			_Screens.Add( screen );
		}
		public void RemoveSceen( GameScreen screen )
		{
			screen.Unload();
			_Screens.Remove( screen );
		}
	}
}
