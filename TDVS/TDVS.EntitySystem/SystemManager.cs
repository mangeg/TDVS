using System;
using System.Collections.Generic;

namespace TDVS.EntitySystem
{
	/// <summary>
	/// Manager for <see cref="EntitySystem"/> instances.
	/// </summary>
	public class SystemManager
	{
		private readonly WorldBase _worldBase;
		private readonly Dictionary<Type, EntitySystem> _systems = new Dictionary<Type, EntitySystem>();
		private readonly List<EntitySystem> _systemsList = new List<EntitySystem>();

		/// <summary>
		/// Gets a list of all currently set systems.
		/// </summary>
		public IList<EntitySystem> Systems
		{
			get { return _systemsList; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemManager"/> class.
		/// </summary>
		/// <param name="worldBase">The WorldBase.</param>
		public SystemManager( WorldBase worldBase )
		{
			_worldBase = worldBase;
		}

		/// <summary>
		/// Sets a system.
		/// </summary>
		/// <typeparam name="T">Typeof system to set</typeparam>
		/// <param name="system">The system.</param>
		/// <returns>The system</returns>
		public T SetSystem<T>( T system ) where T : EntitySystem
		{
			system.WorldBase = _worldBase;
			_systems.Add( typeof( T ), system );
			if ( !_systemsList.Contains( system ) )
				_systemsList.Add( system );

			system.SystemBit.Or( SystemBitManager.GetBit<T>() );

			return system;
		}
		/// <summary>
		/// Gets a specific system.
		/// </summary>
		/// <typeparam name="T">Type of system to find.</typeparam>
		/// <returns>The EntitySystem if found; else <c>null</c></returns>
		public T GetSystem<T>() where T : EntitySystem
		{
			EntitySystem system;
			_systems.TryGetValue( typeof( T ), out system );
			return system as T;
		}

		/// <summary>
		/// Initializes all currently set systems.
		/// </summary>
		public void InitializeAll()
		{
			foreach (var t in _systemsList)
			{
				t.Initialize();
			}
		}
	}
}
