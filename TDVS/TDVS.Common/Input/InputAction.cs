using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Common.Input
{
	/// <summary>
	/// Representing any form of input action that can be combined from several devices.
	/// </summary>
	public class InputAction
	{
		List<Keys> _keys;
		List<MouseButtons> _mouseButtons;
		List<Buttons> _buttons;
		bool _trigger;

		/// <summary>
		/// Gets the keys.
		/// </summary>
		public IList<Keys> Keys
		{
			get { return _keys; }
		}
		/// <summary>
		/// Gets the mouse buttons.
		/// </summary>
		public IList<MouseButtons> MouseButtons
		{
			get { return _mouseButtons; }
		}
		/// <summary>
		/// Gets the buttons.
		/// </summary>
		public IList<Buttons> Buttons
		{
			get { return _buttons; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="InputAction"/> is trigger.
		/// </summary>
		/// <value>
		///   <c>true</c> if trigger; otherwise, <c>false</c>.
		/// </value>
		public bool Trigger
		{
			get { return _trigger; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputAction"/> class.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="mouseButtons">The mouse buttons.</param>
		/// <param name="trigger">if set to <c>true</c> only trigger actions will be tracked.</param>
		public InputAction( Keys[] keys, MouseButtons[] mouseButtons, bool trigger )
		{
			this._keys = keys != null ? keys.ToList() : new List<Keys>();
			this._mouseButtons = mouseButtons != null ? mouseButtons.ToList() : new List<MouseButtons>();
			this._buttons = _buttons != null ? _buttons.ToList() : new List<Buttons>();

			this._trigger = trigger;
		}

		/// <summary>
		/// Evaluates this instance.
		/// </summary>
		/// <returns><c>true</c> if any of the keys and actions was triggered.</returns>
		public bool Evaluate()
		{
			foreach ( var key in _keys )
			{
				if ( _trigger )
				{
					if ( InputManager.IsKeyTriggered( key ) )
						return true;
				}
				else
					if ( InputManager.IsKeyPressed( key ) )
						return true;				
			}
			foreach ( var key in _mouseButtons )
			{
				if ( _trigger )
				{
					if ( InputManager.IsMouseButtonTriggered( key ) )
						return true;
				}
				else
					if ( InputManager.IsMouseButtonPressed( key ) )
						return true;
			}

			return false;
		}
	}
}
