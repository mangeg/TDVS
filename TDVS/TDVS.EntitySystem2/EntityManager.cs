using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.Common;

namespace TDVS.EntitySystem2
{
    public class EntityManager
    {
        private World _World;
        private Bag<Entity> _ActiveEntities = new Bag<Entity>();
        private Bag<Entity> _RemovedAndAvalibleEntities = new Bag<Entity>();
        private int _NextAvalibleId;
        private long _UniqueEntityId;

        public int EntityCount { get; private set; }
        public long EntityTotalCreated { get; private set; }
        public long EntityTotalRemoved { get; private set; }

        private Bag<Bag<IComponent>> _ComponentsByType = new Bag<Bag<IComponent>>();
        
        private Bag<IComponent> _EntityComponents = new Bag<IComponent>();

        public EntityManager(World world) { _World = world; }

        public Entity Create()
        {
            var e = _RemovedAndAvalibleEntities.RemoveLast();
            if(e == null)
                e = new Entity(_World, _NextAvalibleId++);
            else
             e.Reset();

            e.UniqueId = _UniqueEntityId++;
            _ActiveEntities.Set(e.Id, e);
            
            EntityCount++;
            EntityTotalCreated++;

            // Fire CreatedEntity Event here...

            return e;
        }

        public void Remove(Entity e)
        {
            _ActiveEntities.Set(e.Id, null);

            e.TypeBits = 0;
            Refresh(e);

            EntityCount--;
            EntityTotalRemoved++;

            _RemovedAndAvalibleEntities.Add(e);

            // Fire RemovedEntity Event here...
        }

        private void RemoveComponentsOfEntity(Entity e)
        {
            var entityId = e.Id;
            var noComponentsByType = _ComponentsByType.Size();
            for(var a = 0; noComponentsByType > a; a++)
            {
                var components = _ComponentsByType.Get(a);
                if(components != null && entityId < components.Size())
                {
                    // Removed component event here
                    components.Set(entityId, null);
                }
            }
        }

        

        public void Refresh(Entity entity)
        {
            var systemManager = _World.SystemManager;
            var systems = systemManager.GetSystems();
            var noOfSystems = systems.Size();

            for(var i = 0; noOfSystems > i; i++)
            {
                systems.Get(i).Change(entity);
            }
        }

        public void AddComponent(Entity entity, IComponent component)
        {
            var type = ComponentTypeManager.GetTypeFor(component.GetType());

            if(type.Id >= _ComponentsByType.GetCapacity())
            {
                _ComponentsByType.Set(type.Id, null);
            }

            var components = _ComponentsByType.Get(type.Id);
            if(components == null)
            {
                components = new Bag<IComponent>();
                _ComponentsByType.Set(type.Id, components);
            }

            components.Set(entity.Id, component);
            entity.AddTypeBit(type.Bit);

            // Fire add component event here
        }

        internal void AddComponent<T>(Entity entity, IComponent component)
        {
            var type = ComponentTypeManager.GetTypeFor<T>();

            if(type.Id >= _ComponentsByType.GetCapacity())
            {
                _ComponentsByType.Set(type.Id, null);
            }

            var components = _ComponentsByType.Get(type.Id);
            if(components == null)
            {
                components = new Bag<IComponent>();
                _ComponentsByType.Set(type.Id, components);
            }

            components.Set(entity.Id, component);

            entity.AddTypeBit(type.Bit);

            // Added component event here
        }

        internal void RemoveComponent<T>(Entity entity, IComponent component) where T : IComponent
        {
            var type = ComponentTypeManager.GetTypeFor<T>();
            RemoveComponent(entity, type);
        }

        internal void RemoveComponent(Entity entity, ComponentType type)
        {
            var entityId = entity.Id;
            var components = _ComponentsByType.Get(type.Id);
            //removed comoponent event here

            components.Set(entityId, null);
            entity.RemoveTypeBit(type.Bit);
        }

        internal IComponent GetComponent(Entity entity, ComponentType type)
        {
            var entityId = entity.Id;
            var bag = _ComponentsByType.Get(type.Id);
            if (bag != null && entityId < bag.GetCapacity())
                return bag.Get(entityId);

            return null;
        }

        internal bool IsActive(int entityId)
        {
            return _ActiveEntities.Get(entityId) != null;
        }

        internal Bag<IComponent> GetComponents(Entity entity)
        {
            _EntityComponents.Clear();
            var entityId = entity.Id;
            
            for(var a = 0; _ComponentsByType.Size() > a; a++)
            {
                var components = _ComponentsByType.Get(a);
                if(components != null && entityId < components.Size())
                {
                    var component = components.Get(entityId);
                    if(component != null)
                    {
                        _EntityComponents.Add(component);
                    }
                }
            }

            return _EntityComponents;
        }

        public Bag<Entity> GetActiveEntities()
        {
            return _ActiveEntities;
        }

        public Entity GetEntity(int entityId)
        {
            return _ActiveEntities.Get(entityId);
        }

    }
}
