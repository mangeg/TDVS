using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TDVS.Game
{
	public enum MouseButtons
	{
		LeftButton,
		MiddleButton,
		RightButton,
		XButton1,
		XButton2,
		ScrollUp,
		ScrollDown,
		Move,
	}

	static class InputManager
	{
		static InputManager()
		{
			Update();
		}

		#region Keyboard
		private static KeyboardState _CurrentKeyboardState;
		private static KeyboardState _PrevKeyboardState;

		public static KeyboardState CurrentKeyboardState
		{
			get { return _CurrentKeyboardState; }
		}
		public static KeyboardState PreviousKeyboardState
		{
			get { return _PrevKeyboardState; }
		}

		public static bool IsKeyPressed( Keys key )
		{
			return _CurrentKeyboardState.IsKeyDown( key );
		}
		public static bool IsKeyTriggered( Keys key )
		{
			return _CurrentKeyboardState.IsKeyDown( key ) &&
				!_PrevKeyboardState.IsKeyDown( key );
		}
		#endregion

		#region Mouse

		private static MouseState _CurrentMouseState;
		private static MouseState _PrevMouseState;

		public static MouseState CurrentMouseState
		{
			get
			{
				return _CurrentMouseState;
			}
		}
		public static MouseState PreviousMouseState
		{
			get
			{
				return _PrevMouseState;

			}
		}

		public static bool IsMouseButtonPressed( MouseButtons button )
		{
			switch ( button )
			{
				case MouseButtons.LeftButton:
					return _CurrentMouseState.LeftButton == ButtonState.Pressed;
				case MouseButtons.MiddleButton:
					return _CurrentMouseState.MiddleButton == ButtonState.Pressed;
				case MouseButtons.RightButton:
					return _CurrentMouseState.RightButton == ButtonState.Pressed;
				case MouseButtons.XButton1:
					return _CurrentMouseState.XButton1 == ButtonState.Pressed;
				case MouseButtons.XButton2:
					return _CurrentMouseState.XButton2 == ButtonState.Pressed;		
				case MouseButtons.ScrollUp:
					return _CurrentMouseState.ScrollWheelValue - _PrevMouseState.ScrollWheelValue > 0;
				case MouseButtons.ScrollDown:
					return _CurrentMouseState.ScrollWheelValue - _PrevMouseState.ScrollWheelValue < 0;
				default:
					return false;
			}
		}
		public static bool IsMouseButtonTriggered( MouseButtons button )
		{
			switch ( button )
			{
				case MouseButtons.LeftButton:
					return _CurrentMouseState.LeftButton == ButtonState.Pressed && 
						_PrevMouseState.LeftButton == ButtonState.Released;
				case MouseButtons.MiddleButton:
					return _CurrentMouseState.MiddleButton == ButtonState.Pressed && 
						_PrevMouseState.MiddleButton == ButtonState.Released;
				case MouseButtons.RightButton:
					return _CurrentMouseState.RightButton == ButtonState.Pressed && 
						_PrevMouseState.RightButton == ButtonState.Released;
				case MouseButtons.XButton1:
					return _CurrentMouseState.XButton1 == ButtonState.Pressed && 
						_PrevMouseState.XButton1 == ButtonState.Released;
				case MouseButtons.XButton2:
					return _CurrentMouseState.XButton2 == ButtonState.Pressed && 
						_PrevMouseState.XButton2 == ButtonState.Released;
				case MouseButtons.ScrollUp:
					return _CurrentMouseState.ScrollWheelValue - _PrevMouseState.ScrollWheelValue < 0;
				case MouseButtons.ScrollDown:
					return _CurrentMouseState.ScrollWheelValue - _PrevMouseState.ScrollWheelValue > 0;
				default:
					return false;
			}
		}
		public static bool MouseMoved
		{
			get
			{
				return _CurrentMouseState != _PrevMouseState;
			}
		}
		public static Point MousePosition
		{
			get { return new Point( _CurrentMouseState.X, _CurrentMouseState.Y ); }
		}
		public static Vector2 MouseDelta
		{
			get
			{
				int dX = _CurrentMouseState.X - _PrevMouseState.X;
				int dY = _CurrentMouseState.Y - _PrevMouseState.Y;
				return new Vector2( dX, dY );
			}
		}

		#endregion

		public static void Update()
		{
			_PrevKeyboardState = _CurrentKeyboardState;
			_CurrentKeyboardState = Keyboard.GetState();

			_PrevMouseState = _CurrentMouseState;
			_CurrentMouseState = Mouse.GetState();
		}
	}
}
