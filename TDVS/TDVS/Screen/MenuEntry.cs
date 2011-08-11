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
		#region Fields
		private String _Text;
		private Vector2 _Position;
		private float _SelectionFade;
		private float _TimeSinceSelected;
		#endregion

		#region Properties		
		public String Text
		{
			get { return _Text; }
			set { _Text = value; }
		}
		public Vector2 Position
		{
			get { return _Position; }
			set { _Position = value; }
		}
		public float SelectionFade
		{
			get { return _SelectionFade; }
			set { _SelectionFade = value; }
		}
		#endregion

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
			{
				_SelectionFade = Math.Min( _SelectionFade + fadeSpeed, 1 );
			}
			else
			{
				_SelectionFade = Math.Max( _SelectionFade - fadeSpeed, 0 );
			}
		}

		public virtual void Draw( MenuScreen screen, bool isSelected, GameTime gameTime )
		{
			var spriteBatch = screen.ScreenManager.SpriteBatch;
			var font = screen.ScreenManager.DefaultFont;

			Color color = isSelected ? Color.Yellow : Color.White;

			double time = gameTime.TotalGameTime.TotalSeconds;
			float scale = 1 + 0.15f * _SelectionFade;

			Vector2 origin = new Vector2( 0, font.LineSpacing / 2 );
			color *= screen.TransitionAlpha;


			spriteBatch.DrawString( font, _Text, _Position, color, 0,
								  origin, scale, SpriteEffects.None, 0 );
		}

		#region Helpers
		public virtual int GetHeight( SpriteFont font )
		{
			return font.LineSpacing;
		}
		public virtual int GetWidth( SpriteFont font )
		{
			return ( int )font.MeasureString( _Text ).X;
		}
		public virtual bool IsMouseOver( SpriteFont font, int x, int y )
		{
			int w = GetWidth( font );
			int h = GetHeight( font );
			if ( x > _Position.X && x < _Position.X + w
				&& y > _Position.Y - font.LineSpacing / 2 && y < _Position.Y + font.LineSpacing / 2 + h )
				return true;

			return false;
		}
		#endregion
	}
}
