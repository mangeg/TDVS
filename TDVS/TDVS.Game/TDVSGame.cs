using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLog;
using TDVS.Common;
using TDVS.Common.Extensions;
using TDVS.Common.Input;
using TDVS.Common.Utils;
using TDVS.EntitySystem;
using TDVS.Game.EntitySystems;
using TDVS.Game.EntitySystems.Components;
using TDVS.Game.Screen;
using TDVS.Game.Screen.Menues;
using TDVS.Game.Settings;
using TDVS.Game.Systems;
using LogManager = TDVS.Common.Logging.LogManager;

namespace TDVS.Game
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class TDVSGame : Microsoft.Xna.Framework.Game
	{
		public GraphicsDeviceManager Graphics;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;
		//		private ScreenManager _screenManager;
		private ComponentPool _pool;

		public EntitySystem.EntitySystem UIRenderSystem { get; set; }
		public EntitySystem.EntitySystem MovementSystem2D { get; set; }
		public World World { get; private set; }

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
			World = new World();
			World.EntityManager.ComponentRemoved += ComponentRemoved;
			_pool = new ComponentPool( typeof( IComponent ).GetDerivedTypes().ToArray() );
			_pool.Initialize();

			/*_screenManager = new ScreenManager( this );
			Components.Add( _screenManager );*/

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += WindowClientSizeChanged;

			SettingsManager.Initialize( this );
			SettingsManager.ApplyVideoSettings();

			//			_screenManager.AddScreen( new MainMenu() );

			base.Initialize();
		}

		void ComponentRemoved( Entity e, IComponent c )
		{
			_pool.Return( c );
		}

		void WindowClientSizeChanged( object sender, EventArgs e )
		{
			var s = SettingsManager.Settings;
			if ( !s.VideoSettings.FullScreen )
			{
				s.VideoSettings.WindowedResolution.Width = Window.ClientBounds.Width;
				s.VideoSettings.WindowedResolution.Height = Window.ClientBounds.Height;
			}
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();

			_spriteBatch = new SpriteBatch( GraphicsDevice );
			_font = Content.Load<SpriteFont>( @"Fonts\DefaultMenuFont" );

			var systemManager = World.SystemManager;
			UIRenderSystem = systemManager.SetSystem( new UIRenderSystem( _spriteBatch, Content, typeof( Transform2D ) ) );
			MovementSystem2D = systemManager.SetSystem( new MovementSystem2D() );

			var e = World.EntityManager.Create();
			World.EntityManager.AddComponent( e, new Transform2D() );
			World.EntityManager.AddComponent( e, new Velocity2D() { Direction = new Vector2( 1, 1 ), Speed = 100 } );
			e.Refresh();
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
			MovementSystem2D.Process();
			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.DarkBlue );
			FpsMeter.SUpdate( gameTime );

			_spriteBatch.Begin();

			UIRenderSystem.Process();

			_spriteBatch.DrawString( _font, "FPS: " + ( FpsMeter.sFPS ), new Vector2( 10, 10 ),
				Color.Green, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1 );
			_spriteBatch.DrawString( _font, "MS/f: " + ( gameTime.ElapsedGameTime.TotalMilliseconds ),
				new Vector2( 10, 10 + _font.LineSpacing * 0.8f ), Color.Green, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1 );

			_spriteBatch.End();

			base.Draw( gameTime );
		}
	}
}
