using System;
using System.Collections.Generic;
using System.Linq;

namespace CMSystem
{
    public partial class CMSEntity
    {
        public string id;

        public List<EntityComponentDefinition> components = new List<EntityComponentDefinition>() { new AnythingTag() };

        public T Define<T>() where T : EntityComponentDefinition, new()
        {
            var t = GetTag<T>();
            if (t != null)
                return t;

            var entity_component = new T();
            components.Add(entity_component);
            return entity_component;
        }

        public bool Is<T>(out T unknown) where T : EntityComponentDefinition, new()
        {
            unknown = GetTag<T>();
            return unknown != null;
        }

        public bool Is<T>() where T : EntityComponentDefinition, new()
        {
            return GetTag<T>() != null;
        }

        public bool Is(Type type)
        {
            return components.Find(m => m.GetType() == type) != null;
        }

        public T GetTag<T>() where T : EntityComponentDefinition, new()
        {
            return components.Find(m => m is T) as T;
        }
        
        public List<T> GetAllTagsOf<T>() where T: EntityComponentDefinition{
            return components.OfType<T>().ToList();
        }

        public T Clone<T>() where T : CMSEntity, new()
        {
            var clone = new T();
            clone.components = this.components;
            return clone;
        }

    }
}
