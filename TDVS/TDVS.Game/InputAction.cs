using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Game
{
	/// <summary>
	/// Representing any form of input action that can be combined from serveral devices.
	/// </summary>
	public class InputAction
	{
		List<Keys> _Keys;
		List<MouseButtons> _MouseButtons;
		List<Buttons> _Buttons;
		bool _Trigger;

		/// <summary>
		/// Gets the keys.
		/// </summary>
		public IList<Keys> Keys
		{
			get { return _Keys; }
		}
		/// <summary>
		/// Gets the mouse buttons.
		/// </summary>
		public IList<MouseButtons> MouseButtons
		{
			get { return _MouseButtons; }
		}
		/// <summary>
		/// Gets the buttons.
		/// </summary>
		public IList<Buttons> Buttons
		{
			get { return _Buttons; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="InputAction"/> is trigger.
		/// </summary>
		/// <value>
		///   <c>true</c> if trigger; otherwise, <c>false</c>.
		/// </value>
		public bool Trigger
		{
			get { return _Trigger; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputAction"/> class.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="mouseButtons">The mouse buttons.</param>
		/// <param name="trigger">if set to <c>true</c> only trigger actions will be tracked.</param>
		public InputAction( Keys[] keys, MouseButtons[] mouseButtons, bool trigger )
		{
			this._Keys = keys != null ? keys.ToList() : new List<Keys>();
			this._MouseButtons = mouseButtons != null ? mouseButtons.ToList() : new List<MouseButtons>();
			this._Buttons = _Buttons != null ? _Buttons.ToList() : new List<Buttons>();

			this._Trigger = trigger;
		}

		/// <summary>
		/// Evaluates this instance.
		/// </summary>
		/// <returns><c>true</c> if any of the keys and actions was triggered.</returns>
		public bool Evaluate()
		{
			foreach ( var key in _Keys )
			{
				if ( _Trigger )
				{
					if ( InputManager.IsKeyTriggered( key ) )
						return true;
				}
				else
					if ( InputManager.IsKeyPressed( key ) )
						return true;				
			}
			foreach ( var key in _MouseButtons )
			{
				if ( _Trigger )
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
