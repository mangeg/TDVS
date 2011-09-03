using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Common.Input;

namespace TDVS.Common.Utils
{
	/// <summary>
	/// Software cursor class
	/// </summary>
	public class Cursor : DrawableGameComponent
	{
		private SpriteBatch _spriteBatch;
		private Texture2D _cursorTexture;
		private readonly String _cursorTextureFile;
		private float _scale = 1.0f;
		private Color _color = Color.LightBlue;
		private bool _isVisible;

		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		/// <value>
		/// The scale.
		/// </value>
		public float Scale
		{
			get { return _scale; }
			set { _scale = value; }
		}
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>
		/// The color.
		/// </value>
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsVisible
		{
			get { return _isVisible; }
			set { _isVisible = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cursor"/> class.
		/// </summary>
		/// <param name="game">The Game that the game component should be attached to.</param>
		/// <param name="mouseTexture">The exture file to use.</param>
		public Cursor( Game game, String mouseTexture )
			: base( game )
		{
			_cursorTextureFile = mouseTexture;
		}

		/// <summary>
		/// Called when graphics resources need to be loaded. Override this method to load any component-specific graphics resources.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			_cursorTexture = Game.Content.Load<Texture2D>( _cursorTextureFile );
		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code. Reference page contains links to related conceptual articles.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Draw.</param>
		public override void Draw( GameTime gameTime )
		{
			base.Draw( gameTime );

			if ( !_isVisible ) return;

			var p = new Vector2( InputManager.MousePosition.X, InputManager.MousePosition.Y );
			var p2 = new Vector2( p.X + 1, p.Y + 1 );

			_spriteBatch.Begin();
			// Shadow
			_spriteBatch.Draw( _cursorTexture, p2, null, Color.Black, 0f, Vector2.Zero,
				_scale, SpriteEffects.None, 1 );
			// Main
			_spriteBatch.Draw( _cursorTexture, p, null, _color,
				0f, Vector2.Zero,
				_scale, SpriteEffects.None, 1 );
			_spriteBatch.End();
		}

		
	}
}
