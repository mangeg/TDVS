using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TDVS.Settings;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Runtime.InteropServices;

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
			base.Initialize();

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += Window_ClientSizeChanged;

			SettingsManager.Initialize( this );
			SettingsManager.ApplyVideoSettings();

#if WINDOWS
			Components.Add( new Cursor( this ) );
#endif
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
			GraphicsDevice.Clear( Color.CornflowerBlue );
			FpsMeter.Update( gameTime );		

			spriteBatch.Begin(); 

			spriteBatch.DrawString( font, "FPS: " + ( FpsMeter.FPS ).ToString(), new Vector2( 100, 100 ), Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 );
			spriteBatch.DrawString( font, "MS/s: " + ( gameTime.ElapsedGameTime.TotalMilliseconds ).ToString(), new Vector2( 100, 100 + font.MeasureString( "FPS: " + ( FpsMeter.FPS ).ToString() ).Y ), Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 );

			spriteBatch.End();

			base.Draw( gameTime );
		}
	}
}
