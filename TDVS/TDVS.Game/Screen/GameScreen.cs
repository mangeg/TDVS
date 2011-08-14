using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Game.Screen
{
	public enum GameScreenState
	{
		TransitionOn,
		Active,
		TransitionOff,
		Hidden,
	}

	public abstract class GameScreen
	{
		private ScreenManager _Manager = null;
		private bool _IsPopup = false;
		private bool _IsExiting = false;
		private bool _OtherScreenHasFocus = false;
		private TimeSpan _TransitionOnTime = TimeSpan.Zero;
		private TimeSpan _TransitionOffTime = TimeSpan.Zero;
		private GameScreenState _ScreenState = GameScreenState.TransitionOn;
		private float _TransitionPosition = 1;

		public ScreenManager ScreenManager
		{
			get { return _Manager; }
			internal set { _Manager = value; }
		}
		public bool IsPopup
		{
			get { return _IsPopup; }
			protected set { _IsPopup = value; }
		}
		public bool IsExiting
		{
			get { return _IsExiting; }
			protected set { _IsExiting = value; }
		}
		public bool IsActive
		{
			get
			{
				return !_OtherScreenHasFocus &&
					( _ScreenState == GameScreenState.Active ||
					_ScreenState == GameScreenState.TransitionOn );
			}
		}
		public TimeSpan TransitionOnTime
		{
			get { return _TransitionOnTime; }
			protected set { _TransitionOnTime = value; }
		}
		public TimeSpan TransitionOffTime
		{
			get { return _TransitionOffTime; }
			protected set { _TransitionOffTime = value; }
		}
		public GameScreenState ScreenState
		{
			get { return _ScreenState; }
			protected set { _ScreenState = value; }
		}
		public float TransitionPosition
		{
			get { return _TransitionPosition; }
			protected set { _TransitionPosition = value; }
		}
		public float TransitionAlpha
		{
			get { return 1f - TransitionPosition; }
		}

		public virtual void Activate() { }
		public virtual void Deactivate() { }
		public virtual void Unload() { }
		public virtual void HandleInput( GameTime gameTime ) { }
		public virtual void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
		{
			if ( _IsExiting )
			{
				_ScreenState = GameScreenState.TransitionOff;

				if ( !UpdateTransition( gameTime, _TransitionOffTime, 1 ) )
				{
					ScreenManager.RemoveSceen( this );
				}
			}
			else if ( coveredByOtherScreen )
			{
				if ( UpdateTransition( gameTime, _TransitionOffTime, 1 ) )
				{
					_ScreenState = GameScreenState.TransitionOff;
				}
				else
				{
					_ScreenState = GameScreenState.Hidden;
				}
			}
			else
			{
				if ( UpdateTransition( gameTime, TransitionOnTime, -1 ) )
				{
					_ScreenState = GameScreenState.TransitionOn;
				}
				else
				{
					_ScreenState = GameScreenState.Active;
				}
			}
		}
		public virtual void Draw( GameTime gameTime ) { }

		public bool UpdateTransition( GameTime gameTime, TimeSpan time, int direction )
		{
			float tDelta;

			if ( time == TimeSpan.Zero )
			{
				tDelta = 1;
			}
			else
			{
				tDelta = ( float )( gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds );
			}

			_TransitionPosition += tDelta * direction;

			if ( ( direction < 0 && _TransitionPosition <= 0 ) ||
				( direction > 0 && _TransitionPosition >= 1 ) )
			{
				_TransitionPosition = MathHelper.Clamp( _TransitionPosition, 0f, 1f );
				return false;
			}

			return true;
		}
		public void ExitScren()
		{
			if ( TransitionOffTime == TimeSpan.Zero )
			{
				ScreenManager.RemoveSceen( this );
			}
			else
			{
				IsExiting = true;
			}
		}
	}
}
