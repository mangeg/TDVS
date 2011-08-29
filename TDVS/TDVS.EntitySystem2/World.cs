using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.Common;

namespace TDVS.EntitySystem2
{
    public class World
    {
        #region Public properties
        
        public SystemManager SystemManager { get; private set; }
        public EntityManager EntityManager { get; private set; }
        public TagManager TagManager { get; private set; }
        public GroupManager GroupManager { get; private set; }
        

        public int Delta;
        
        #endregion

        #region Private fields

        private Bag<Entity> _Refreshed = new Bag<Entity>();
        private Bag<Entity> _Deleted = new Bag<Entity>();
        private Dictionary<Type, IManager> _Managers = new Dictionary<Type, IManager>();

        #endregion

        #region Constructor

        public World()
        {
            EntityManager = new EntityManager(this);
            SystemManager = new SystemManager(this);
            TagManager = new TagManager(this);
            GroupManager = new GroupManager(this);

        }

        #endregion

        #region Manager Set- and Get methods
        
        public void SetManager(IManager manager)
        {
            _Managers.Add(manager.GetType(), manager);
        }

        public T GetManager<T>() where T : IComponent
        {
            IManager m;
            _Managers.TryGetValue(typeof (T), out m);
            return (T) m;
        }

        #endregion

        #region Entity controls

        public Entity CreateEntity() { return this.EntityManager.Create(); }
        public Entity CreateEntity(string tag)
        {
            var e = this.EntityManager.Create();
            this.TagManager.Register(tag, e);
            return e;
        }

        public Entity GetEntity(int entityId) { return this.EntityManager.GetEntity(entityId); }
        public void RefreshEntity(Entity entity) { _Refreshed.Add(entity); }
        public void DeleteEntity(Entity entity)
        {
            this.GroupManager.Remove(entity);
            if(!_Deleted.Contains(entity))
            {
                _Deleted.Add(entity);
            }
        }

        #endregion

        #region Loop start

        public void LoopStart()
        {
            if(!_Refreshed.IsEmpty())
            {
                for (int i = 0, j = _Refreshed.Size(); j > i; i++)
                {
                    this.EntityManager.Refresh(_Refreshed.Get(i));
                }
                _Refreshed.Clear();
            }

            if(!_Deleted.IsEmpty())
            {
                for(int i = 0, j = _Deleted.Size(); j > i; i++)
                {
                    var e = _Deleted.Get(i);
                    this.EntityManager.Remove(e);
                }

                _Deleted.Clear();
            }
        }

        #endregion

        #region Get and Load state

        public Dictionary<Entity, Bag<IComponent>> GetCurrentState()
        {
            var entities = this.EntityManager.GetActiveEntities();
            var currentState = new Dictionary<Entity, Bag<IComponent>>();
            for(int i = 0, j = entities.Size(); i < j; i++)
            {
                var e = entities.Get(i);
                var components = e.GetComponents();
                currentState.Add(e, components);
            }

            return currentState;
        }

        public void LoadEntityState(string tag, string groupName, Bag<IComponent> components)
        {
            Entity e;
            if(!String.IsNullOrEmpty(tag))
            {
                e = CreateEntity(tag);
            } else
            {
                e = CreateEntity();
            }

            if(!String.IsNullOrEmpty(groupName))
            {
                this.GroupManager.Set(groupName, e);
            }

            for(int i = 0, j = components.Size(); i < j; i++)
            {
                e.AddComponent(components.Get(i));
            }
        }

        #endregion
    }
}
