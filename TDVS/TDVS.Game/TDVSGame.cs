using System;
using System.Linq;
using Microsoft.Xna.Framework;
using TDVS.Common.Extensions;
using TDVS.Common.Resources;
using TDVS.Common.Settings;
using TDVS.EntitySystem;
using TDVS.Game.Settings;
using TDVS.Game.Systems;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Game
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class TDVSGame : Microsoft.Xna.Framework.Game
	{
		public GraphicsDeviceManager Graphics;
		private ComponentPool _pool;

	    private TDVS.TileMap.TileMap _tilemap;
	    private Texture2D _tileset;
		public World World { get; private set; }
        
		public TDVSGame()
		{
			Graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";

			ResourceManager.Initialize( Content ); 
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			World = new World( this );
			World.EntityManager.ComponentRemoved += ComponentRemoved;

			_pool = new ComponentPool( typeof( IComponent ).GetDerivedTypes().ToArray() );
			_pool.Initialize();

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += WindowClientSizeChanged;

			base.Initialize();
			World.Initialize();

			SettingsManager.Get<GameSettings>().ApplyVideoSettings( this );
		}
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
			World.LoadResource();
		    _tileset = Content.Load<Texture2D>( @"Textures\tilemap" );
		    _tilemap = new TDVS.TileMap.TileMap( _tileset );
		    Camera.Camera.Initialize( new Point( 1600, 1600 ),
		                              new Point( SettingsManager.Settings.VideoSettings.WindowedResolution.Width,
		                                         SettingsManager.Settings.VideoSettings.WindowedResolution.Height ) );
		}
		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		/// 
		protected override void UnloadContent()
		{
			World.UnloadResources();
		}
		/// <summary>
		/// Allows the game to run logic such as updating the World,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update( GameTime gameTime )
		{
		    var k = Keyboard.GetState( );
            if (k.IsKeyDown(Keys.W))
                Camera.Camera.Move(new Vector2(0.0f, -1.0f));
            if (k.IsKeyDown(Keys.A))
                Camera.Camera.Move(new Vector2(-1.0f, 0.0f));
            if (k.IsKeyDown(Keys.S))
                Camera.Camera.Move(new Vector2(0.0f, 1.0f));
            if (k.IsKeyDown(Keys.D))
                Camera.Camera.Move(new Vector2(1.0f, 0.0f));


            Console.WriteLine(Camera.Camera.Position.X + ", " + Camera.Camera.Position.Y);
            _tilemap.Update( gameTime, Camera.Camera.Position, Camera.Camera.ViewPortSize );
			base.Update( gameTime );
			World.Update( gameTime );
		}
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime )
		{
			_spriteBatch.Begin();
            _tilemap.Draw( _spriteBatch );
			_spriteBatch.End();
			base.Draw( gameTime );
			World.Draw( gameTime );
		}

		void ComponentRemoved( Entity e, IComponent c )
		{
			_pool.Return( c );
		}
		void WindowClientSizeChanged( object sender, EventArgs e )
		{
			/*
			var s = SettingsManager.GameSettings;
			if ( !s.VideoSettings.FullScreen )
			{
				s.VideoSettings.WindowedResolution.Width = Window.ClientBounds.Width;
				s.VideoSettings.WindowedResolution.Height = Window.ClientBounds.Height;
			}
			 */
		}
	}
}
