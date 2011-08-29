using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TDVS.Common;

namespace TDVS.EntitySystem2
{
    public class Entity
    {
        #region Private Fields

        private EntityManager _EntityManager;
        
        #endregion

        #region Public Properties

        public int Id { get; set; }
        public long UniqueId { get; set; }
        public long TypeBits { get; set; }
        public long SystemBits { get; set; }

        public World _World { get; set; }

        #endregion

        #region Constructors
        
        public Entity() { }
        public Entity(World world, int id)
        {
            _World = world;
            _EntityManager = world.EntityManager;
            Id = id;
        }

        #endregion

        #region System- and TypeBits operations
        
        
        // Add one bit to a byte
        // TypeBits = 0001 0000
        // typeBit  = 0000 0001
        //         |= 0001 0001
        
        // Remove one bit from a byte
        // TypeBits = 0000 0110
        // typeBit  = 0000 0010
        // ~typeBit = 1111 1101
        //         $= 0000 0010
        public void AddTypeBit(long typeBit) { TypeBits |= typeBit; }
        public void AddSystemBit(long systemBit) { SystemBits |= systemBit; }
        public void RemoveTypeBit(long typeBit) { TypeBits &= ~typeBit; }
        public void RemoveSystemBit(long systemBit) { SystemBits &= ~systemBit; }
        public void Reset() { TypeBits = 0; SystemBits = 0; }

        #endregion

        #region Component operations
        
        public void AddComponent(IComponent component) { _EntityManager.AddComponent(this, component); }
        public void AddComponent<T>(IComponent component) where T : IComponent { _EntityManager.AddComponent<T>(this, component); }
        public void RemoveComponent<T>(IComponent component) where T: IComponent { _EntityManager.RemoveComponent<T>(this, component); }
        public void RemoveComponent(ComponentType type) { _EntityManager.RemoveComponent(this, type); }

        public IComponent GetComponent(ComponentType type) { return _EntityManager.GetComponent(this, type); }
        public T GetComponent<T>() where T : IComponent { return (T)GetComponent(ComponentTypeManager.GetTypeFor<T>()); }
        public Bag<IComponent> GetComponents() { return _EntityManager.GetComponents(this); }

        #endregion

        #region Entity operations

        public bool IsActive() { return _EntityManager.IsActive(Id); }
        public void Refresh() { _World.RefreshEntity(this); }
        public void Delete() { _World.DeleteEntity(this); }

        #endregion

        #region Group- and Tag operations
        
        public void SetGroup(string group) { _World.GroupManager.Set(group, this); }
        public void SetTag(string tag) { _World.TagManager.Register(tag, this); }
        public string GetTag() { return _World.TagManager.GetTagOfEntity(this); }
        
        #endregion

        #region Operation Override's

        public override String ToString() { return "Entity[" + Id + "]"; }

        #endregion
    }
}
