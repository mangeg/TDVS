using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.Common;

namespace TDVS.EntitySystem2
{
    public class SystemManager
    {
        private World _World;
        private Dictionary<Type, EntitySystem> _Systems = new Dictionary<Type, EntitySystem>();
        private Bag<EntitySystem> _Bagged = new Bag<EntitySystem>();

        public SystemManager(World world)
        {
            _World = world;
        }

        public T SetSystem<T>(T system) where T : EntitySystem
        {
            system.SetWorld(_World);

            _Systems.Add(typeof (T), system);

            if(!_Bagged.Contains(system))
                _Bagged.Add(system);

            system.SystemBit = SystemBitManager.GetBitFor(system);

            return system;
        }

        public T GetSystem<T>() where T : EntitySystem
        {
            EntitySystem system;
            _Systems.TryGetValue(typeof (T), out system);
            return (T) system;
        }

        public Bag<EntitySystem> GetSystems()
        {
            return _Bagged;
        }

        public void InitializeAll()
        {
            for(var i = 0; i < _Bagged.Size(); i++)
            {
                _Bagged.Get(i).Initialize();
            }
        }
    }
}
