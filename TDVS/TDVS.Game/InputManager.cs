using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TDVS.Game
{
	/// <summary>
	/// Enum over possible actions with the mouse.
	/// </summary>
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

	/// <summary>
	/// Static class for managing user input
	/// </summary>
	static class InputManager
	{
		static InputManager()
		{
			Update();
		}

		#region Keyboard
		private static KeyboardState _CurrentKeyboardState;
		private static KeyboardState _PrevKeyboardState;

		/// <summary>
		/// Gets the state of the current keyboard.
		/// </summary>
		/// <value>
		/// The state of the current keyboard.
		/// </value>
		public static KeyboardState CurrentKeyboardState
		{
			get { return _CurrentKeyboardState; }
		}
		/// <summary>
		/// Gets the previous state of the keyboard.
		/// </summary>
		/// <value>
		/// The state of the previous keyboard.
		/// </value>
		public static KeyboardState PreviousKeyboardState
		{
			get { return _PrevKeyboardState; }
		}

		/// <summary>
		/// Determines whether a specific key is currently pressed.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the kkey is pressed; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsKeyPressed( Keys key )
		{
			return _CurrentKeyboardState.IsKeyDown( key );
		}
		/// <summary>
		/// Determines whether the specified key was triggered during the most recent update. Have to release and press it again to trigger it again.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the key was pressed during the most recent update; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsKeyTriggered( Keys key )
		{
			return _CurrentKeyboardState.IsKeyDown( key ) &&
				!_PrevKeyboardState.IsKeyDown( key );
		}
		#endregion

		#region Mouse

		private static MouseState _CurrentMouseState;
		private static MouseState _PrevMouseState;

		/// <summary>
		/// Gets the current state of the mouse.
		/// </summary>
		/// <value>
		/// The current state of the mouse.
		/// </value>
		public static MouseState CurrentMouseState
		{
			get
			{
				return _CurrentMouseState;
			}
		}
		/// <summary>
		/// Gets the previous state of the mouse.
		/// </summary>
		/// <value>
		/// The previous state of the mouse.
		/// </value>
		public static MouseState PreviousMouseState
		{
			get
			{
				return _PrevMouseState;

			}
		}

		/// <summary>
		/// Determines whether the specified mouse action is pressed.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <returns>
		///   <c>true</c> if the action is pressed; otherwise, <c>false</c>.
		/// </returns>
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
		/// <summary>
		/// Determines whether the specified mouse action was triggered during the most recent update.
		/// Have to be released and pressed again to cause another trigger.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <returns>
		///   <c>true</c> if the mouse action was triggered during the most recent update; otherwise, <c>false</c>.
		/// </returns>
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
		/// <summary>
		/// Gets a value indicating whether the mouse has moved since the last update.
		/// </summary>
		/// <value>
		///   <c>true</c> if moved; otherwise, <c>false</c>.
		/// </value>
		public static bool MouseMoved
		{
			get
			{
				return _CurrentMouseState != _PrevMouseState;
			}
		}
		/// <summary>
		/// Gets the current mouse position.
		/// </summary>
		public static Point MousePosition
		{
			get { return new Point( _CurrentMouseState.X, _CurrentMouseState.Y ); }
		}
		/// <summary>
		/// Gets the mouse delta movement since last update.
		/// </summary>
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

		/// <summary>
		/// Updates this instance and fetch new states for input devices.
		/// </summary>
		public static void Update()
		{
			_PrevKeyboardState = _CurrentKeyboardState;
			_CurrentKeyboardState = Keyboard.GetState();

			_PrevMouseState = _CurrentMouseState;
			_CurrentMouseState = Mouse.GetState();
		}
	}
}
