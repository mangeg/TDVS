using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Screen;
using TDVS.Screen.Menues;
using TDVS.Settings;

namespace TDVS
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class TDVSGame : Microsoft.Xna.Framework.Game
	{
		public GraphicsDeviceManager Graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		ScreenManager screenManager;

		public TDVSGame()
		{
			Graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			screenManager = new ScreenManager( this );
			Components.Add( screenManager );
#if WINDOWS
			//Components.Add( new Cursor( this ) );
#endif

			base.Initialize();

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += Window_ClientSizeChanged;

			SettingsManager.Initialize( this );
			SettingsManager.ApplyVideoSettings();

			screenManager.AddScreen( new MainMenu() );
		}

		void Window_ClientSizeChanged( object sender, EventArgs e )
		{
			var S = SettingsManager.Settings;
			if ( !S.VideoSettings.FullScreen )
			{
				S.VideoSettings.WindowedResolution.Width = Window.ClientBounds.Width;
				S.VideoSettings.WindowedResolution.Height = Window.ClientBounds.Height;
			}
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();

			spriteBatch = new SpriteBatch( GraphicsDevice );
			font = Content.Load<SpriteFont>( @"Fonts\DefaultMenuFont" );
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		/// 
		protected override void UnloadContent()
		{
			SettingsManager.Save();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update( GameTime gameTime )
		{
			InputManager.Update();
			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.DarkBlue );
			FpsMeter.Update( gameTime );

			spriteBatch.Begin();

			spriteBatch.DrawString( font, "FPS: " + ( FpsMeter.FPS ).ToString(), new Vector2( 10, 10 ), Color.Green, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1 );
			spriteBatch.DrawString( font, "MS/f: " + ( gameTime.ElapsedGameTime.TotalMilliseconds ).ToString(), new Vector2( 10, 10 + font.LineSpacing * 0.8f ), Color.Green, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1 );

			spriteBatch.End();

			base.Draw( gameTime );
		}
	}
}
