using System;
using System.Collections.Generic;
using TDVS.Common.Settings.Targets;

namespace TDVS.Common.Settings
{
	/// <summary>
	/// Static manager for settings.
	/// </summary>
	public static class SettingsManager
	{
		private static readonly Dictionary<Type, ITarget> _sTargets = new Dictionary<Type, ITarget>();
		private static readonly Dictionary<Type, SettingsBase> _sEttingObjects = new Dictionary<Type, SettingsBase>();

		/// <summary>
		/// Sets the target for a specific settings type.
		/// </summary>
		/// <typeparam name="T">The type for the settings</typeparam>
		/// <param name="target">The target.</param>
		public static void SetTarget<T>( ITarget target )
		{
			var type = typeof( T );
			if ( _sTargets.ContainsKey( type ) )
				_sTargets[ type ] = target;
			else
			{
				_sTargets.Add( type, target );
			}
		}
		/// <summary>
		/// Gets settings for the specific type.
		/// </summary>
		/// <typeparam name="T">Type of the settings to get.</typeparam>
		/// <returns>The settings. If none has been loaded a new instance is created and returned.</returns>
		public static T Get<T>() where T : SettingsBase
		{
			var type = typeof (T);
			if ( !_sEttingObjects.ContainsKey( type ) )
			{
				var val = ( T )Activator.CreateInstance( type, true );
				_sEttingObjects.Add( type, val );
			}
			return ( T )_sEttingObjects[ type ];
		}
		/// <summary>
		/// Saves the settings of the specific type to the target set.
		/// </summary>
		/// <typeparam name="T">Type of the setting to save.</typeparam>
		public static void Save<T>() where T : SettingsBase
		{
			var type = typeof( T );
			if ( !_sTargets.ContainsKey( type ) )
				throw new Exception( "" );

			_sTargets[ type ].Value = _sEttingObjects[ type ];
			_sTargets[ type ].Save();
		}
		/// <summary>
		/// Loads the settings for the specific type via the target set.
		/// </summary>
		/// <typeparam name="T">The type of settings to load.</typeparam>
		public static void Load<T>() where T : SettingsBase
		{
			var type = typeof( T );
			if ( !_sTargets.ContainsKey( typeof( T ) ) )
				throw new Exception( "" );

			_sTargets[ type ].Load();
			_sEttingObjects[ type ] = ( T )_sTargets[ type ].Value;
		}
	}
}
