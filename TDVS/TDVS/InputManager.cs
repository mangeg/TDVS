using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TDVS
{
	public enum MouseButtons
	{
		LeftButton,
		MiddleButton,
		RightButton,
		XButton1,
		XButton2,
	}

	static class InputManager
	{
		static InputManager()
		{
			Update();
		}

		#region Keyboard
		private static KeyboardState _currentKeyboardState;
		private static KeyboardState _prevKeyboardState;

		public static KeyboardState CurrentKeyboardState
		{
			get { return _currentKeyboardState; }
		}
		public static KeyboardState PreviousKeyboardState
		{
			get { return _prevKeyboardState; }
		}

		public static bool IsKeyPressed( Keys key )
		{
			return _currentKeyboardState.IsKeyDown( key );
		}
		public static bool IsKeyTriggered( Keys key )
		{
			return _currentKeyboardState.IsKeyDown( key ) &&
				!_prevKeyboardState.IsKeyDown( key );
		}
		#endregion

		#region Mouse

		private static MouseState _currentMouseState;
		private static MouseState _prevMouseState;

		public static MouseState CurrentMouseState
		{
			get
			{
				return _currentMouseState;
			}
		}
		public static MouseState PreviousMouseState
		{
			get
			{
				return _prevMouseState;

			}
		}

		public static bool IsMouseButtonPressed( MouseButtons button )
		{
			switch ( button )
			{
				case MouseButtons.LeftButton:
					return _currentMouseState.LeftButton == ButtonState.Pressed;
				case MouseButtons.MiddleButton:
					return _currentMouseState.MiddleButton == ButtonState.Pressed;
				case MouseButtons.RightButton:
					return _currentMouseState.RightButton == ButtonState.Pressed;
				case MouseButtons.XButton1:
					return _currentMouseState.XButton1 == ButtonState.Pressed;
				case MouseButtons.XButton2:
					return _currentMouseState.XButton2 == ButtonState.Pressed;
				default:
					return false;
			}
		}
		public static bool IsMouseButtonTriggered( MouseButtons button )
		{
			switch ( button )
			{
				case MouseButtons.LeftButton:
					return _currentMouseState.LeftButton == ButtonState.Pressed && 
						_prevMouseState.LeftButton == ButtonState.Released;
				case MouseButtons.MiddleButton:
					return _currentMouseState.MiddleButton == ButtonState.Pressed && 
						_prevMouseState.MiddleButton == ButtonState.Released;
				case MouseButtons.RightButton:
					return _currentMouseState.RightButton == ButtonState.Pressed && 
						_prevMouseState.RightButton == ButtonState.Released;
				case MouseButtons.XButton1:
					return _currentMouseState.XButton1 == ButtonState.Pressed && 
						_prevMouseState.XButton1 == ButtonState.Released;
				case MouseButtons.XButton2:
					return _currentMouseState.XButton2 == ButtonState.Pressed && 
						_prevMouseState.XButton2 == ButtonState.Released;
				default:
					return false;
			}
		}
		public static Point MousePosition
		{
			get { return new Point( _currentMouseState.X, _currentMouseState.Y ); }
		}
		public static Vector2 MouseDelta
		{
			get
			{
				int dX = _currentMouseState.X - _prevMouseState.X;
				int dY = _currentMouseState.Y - _prevMouseState.Y;
				return new Vector2( dX, dY );
			}
		}

		#endregion

		public static void Update()
		{
			_prevKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();

			_prevMouseState = _currentMouseState;
			_currentMouseState = Mouse.GetState();
		}
	}
}
