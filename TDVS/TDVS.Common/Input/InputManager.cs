using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Input
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
	public static class InputManager
	{
		static InputManager()
		{
			Update();
		}

		private static KeyboardState _currentKeyboardState;
		private static KeyboardState _prevKeyboardState;
		private static MouseState _currentMouseState;
		private static MouseState _prevMouseState;

		/// <summary>
		/// Gets the state of the current keyboard.
		/// </summary>
		/// <value>
		/// The state of the current keyboard.
		/// </value>
		public static KeyboardState CurrentKeyboardState
		{
			get { return _currentKeyboardState; }
		}
		/// <summary>
		/// Gets the previous state of the keyboard.
		/// </summary>
		/// <value>
		/// The state of the previous keyboard.
		/// </value>
		public static KeyboardState PreviousKeyboardState
		{
			get { return _prevKeyboardState; }
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
			return _currentKeyboardState.IsKeyDown( key );
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
			return _currentKeyboardState.IsKeyDown( key ) &&
				!_prevKeyboardState.IsKeyDown( key );
		}
		

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
				return _currentMouseState;
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
				return _prevMouseState;

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
					return _currentMouseState.LeftButton == ButtonState.Pressed;
				case MouseButtons.MiddleButton:
					return _currentMouseState.MiddleButton == ButtonState.Pressed;
				case MouseButtons.RightButton:
					return _currentMouseState.RightButton == ButtonState.Pressed;
				case MouseButtons.XButton1:
					return _currentMouseState.XButton1 == ButtonState.Pressed;
				case MouseButtons.XButton2:
					return _currentMouseState.XButton2 == ButtonState.Pressed;		
				case MouseButtons.ScrollUp:
					return _currentMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue > 0;
				case MouseButtons.ScrollDown:
					return _currentMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue < 0;
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
				case MouseButtons.ScrollUp:
					return _currentMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue < 0;
				case MouseButtons.ScrollDown:
					return _currentMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue > 0;
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
				return _currentMouseState != _prevMouseState;
			}
		}
		/// <summary>
		/// Gets the current mouse position.
		/// </summary>
		public static Point MousePosition
		{
			get { return new Point( _currentMouseState.X, _currentMouseState.Y ); }
		}
		/// <summary>
		/// Gets the mouse delta movement since last update.
		/// </summary>
		public static Vector2 MouseDelta
		{
			get
			{
				int dX = _currentMouseState.X - _prevMouseState.X;
				int dY = _currentMouseState.Y - _prevMouseState.Y;
				return new Vector2( dX, dY );
			}
		}

		/// <summary>
		/// Updates this instance and fetch new states for input devices.
		/// </summary>
		public static void Update()
		{
			_prevKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();

			_prevMouseState = _currentMouseState;
			_currentMouseState = Mouse.GetState();
		}
	}
}
