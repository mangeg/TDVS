using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Game.Settings;

namespace TDVS.Game
{
	/// <summary>
	/// Software cursor class
	/// </summary>
	public class Cursor : DrawableGameComponent
	{
		private SpriteBatch _SpriteBatch;
		private SpriteBatch __SpriteBatch2;
		private Texture2D _CursorTexture;

		private static bool _IsVisible;
		private static bool _ClipToWindow;
		private static Rectangle _InitialRect;

		/// <summary>
		/// Initializes a new instance of the <see cref="Cursor"/> class.
		/// </summary>
		/// <param name="game">The Game that the game component should be attached to.</param>
		public Cursor( Microsoft.Xna.Framework.Game game )
			: base( game )
		{

		}

		static Cursor()
		{
			_IsVisible = true;
			_ClipToWindow = false;
		}

		/// <summary>
		/// Initializes the component. Override this method to load any non-graphics resources and query for any required services.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

			Native.GetClipCursor( ref _InitialRect );
			Clip( _ClipToWindow );
		}

		/// <summary>
		/// Called when graphics resources need to be loaded. Override this method to load any component-specific graphics resources.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
			_SpriteBatch = new SpriteBatch( GraphicsDevice );
			_CursorTexture = Game.Content.Load<Texture2D>( @"Textures\Cursor" );
		}

		/// <summary>
		/// Called when graphics resources need to be unloaded. Override this method to unload any component-specific graphics resources.
		/// </summary>
		protected override void UnloadContent()
		{
			Native.ClipCursor( ref _InitialRect );
		}

		/// <summary>
		/// Called when the GameComponent needs to be updated. Override this method with component-specific update code.
		/// </summary>
		/// <param name="gameTime">Time elapsed since the last call to Update</param>
		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );
		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code. Reference page contains links to related conceptual articles.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Draw.</param>
		public override void Draw( GameTime gameTime )
		{
			base.Draw( gameTime );

			if ( !_IsVisible ) return;

			Vector2 p = new Vector2( InputManager.MousePosition.X, InputManager.MousePosition.Y );
			Vector2 p2 = new Vector2( p.X + 1, p.Y + 1 );
			Color c = new Color( SettingsManager.Settings.VideoSettings.MouseColor );

			_SpriteBatch.Begin();
			// Shadow
			_SpriteBatch.Draw( _CursorTexture, p2, null, Color.Black, 0f, Vector2.Zero,
				SettingsManager.Settings.VideoSettings.MouseScale, SpriteEffects.None, 1 );
			// Main
			_SpriteBatch.Draw( _CursorTexture, p, null, c,
				0f, Vector2.Zero,
				SettingsManager.Settings.VideoSettings.MouseScale, SpriteEffects.None, 1 );
			_SpriteBatch.End();
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
		/// </value>
		public static bool IsVisible
		{
			get { return _IsVisible; }
			set { _IsVisible = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to clip the mouse to the current window.
		/// </summary>
		/// <value>
		///   <c>true</c> if [clip to window]; otherwise, <c>false</c>.
		/// </value>
		public bool ClipToWindow
		{
			get { return _ClipToWindow; }
			set
			{
				_ClipToWindow = value;
				Clip( _ClipToWindow );
			}
		}

		private void Clip( bool clip )
		{
			if ( clip )
			{
				Rectangle rect = Game.Window.ClientBounds;
				rect.Width += rect.X;
				rect.Height += rect.Y;
				//Native.ClipCursor( ref rect );
			}
			else
			{
				//Native.ClipCursor( ref _InitialRect );
			}
		}
	}
}
