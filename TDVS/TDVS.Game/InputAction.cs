using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Game
{
	public class InputAction
	{
		List<Keys> keys;
		List<MouseButtons> mouseButtons;
		List<Buttons> buttons;
		bool trigger;

		public IList<Keys> Keys
		{
			get { return keys; }
		}
		public IList<MouseButtons> MouseButtons
		{
			get { return mouseButtons; }
		}
		public IList<Buttons> Buttons
		{
			get { return buttons; }
		}

		public InputAction( Keys[] keys, MouseButtons[] mouseButtons, bool trigger )
		{
			this.keys = keys != null ? keys.ToList() : new List<Keys>();
			this.mouseButtons = mouseButtons != null ? mouseButtons.ToList() : new List<MouseButtons>();
			this.buttons = buttons != null ? buttons.ToList() : new List<Buttons>();

			this.trigger = trigger;
		}

		public bool Evaluate()
		{
			foreach ( var key in keys )
			{
				if ( trigger )
				{
					if ( InputManager.IsKeyTriggered( key ) )
						return true;
				}
				else
					if ( InputManager.IsKeyPressed( key ) )
						return true;				
			}
			foreach ( var key in mouseButtons )
			{
				if ( trigger )
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
