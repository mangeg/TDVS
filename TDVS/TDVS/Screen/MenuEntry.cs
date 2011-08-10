using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TDVS.Screen
{
	public class MenuEntry
	{
		private String text;
		private Vector2 position;
		private float selectionFade;

		public String Text
		{
			get { return text; }
			set { text = value; }
		}
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}
		public float SelectionFade
		{
			get { return selectionFade; }
			set { selectionFade = value; }
		}

		public event EventHandler Selected;

		protected internal virtual void OnSelectEntry( )
		{
			if ( Selected != null )
				Selected( this, new EventArgs() );
		}

		public virtual void Update( MenuScreen screen, bool isSelected, GameTime gameTime )
		{
			float fadeSpeed = ( float )gameTime.ElapsedGameTime.TotalSeconds * 4;

			if ( isSelected )
				selectionFade = Math.Min( selectionFade + fadeSpeed, 1 );
			else
				selectionFade = Math.Max( selectionFade - fadeSpeed, 0 );
		}
		public virtual void Draw( MenuScreen screen, bool isSelected, GameTime gameTime )
		{
			var screenManager = screen.ScreenManager;
			var spriteBatch = screenManager.SpriteBatch;
			var font = screenManager.DefaultFont;

			// Draw the selected entry in yellow, otherwise white.
			Color color = isSelected ? Color.Yellow : Color.White;

			// Pulsate the size of the selected menu entry.
			double time = gameTime.TotalGameTime.TotalSeconds;
			float pulsate = ( float )Math.Sin( time * 4 ) + 1;
			float scale = 1 + pulsate * 0.1f * selectionFade;

			Vector2 origin = new Vector2( 0, font.LineSpacing / 2 );
			spriteBatch.DrawString( font, text, position, color, 0,
								  origin, scale, SpriteEffects.None, 0 );
		}

		public virtual int GetHeight( SpriteFont font )
		{
			return font.LineSpacing;
		}
		public virtual int GetWidth( SpriteFont font )
		{
			return ( int )font.MeasureString( text ).X;
		}
	}
}
