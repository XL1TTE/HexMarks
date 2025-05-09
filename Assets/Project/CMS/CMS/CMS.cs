using System;
using System.Collections.Generic;
using Project.Internal.Utilities;
using UnityEngine;


namespace CMSystem
{
    public static class CMS
    {
        static CMSTable<CMSEntity> all = new CMSTable<CMSEntity>();

        static bool isInit;

        public static void Init()
        {
            if (isInit)
                return;
            isInit = true;

            AutoAdd();
        }

        static void AutoAdd()
        {
            var subs = ReflectionUtility.GetSubclasses<CMSEntity>();
            foreach (var subclass in subs)
                all.Add(Activator.CreateInstance(subclass) as CMSEntity);

            var resources = Resources.LoadAll<CMSEntityPfb>("CMS");
            foreach (var resEntity in resources)
            {
                Debug.Log("LOAD ENTITY " + resEntity.GetId());
                all.Add(new CMSEntity()
                {
                    id = resEntity.GetId(),
                    components = resEntity.Components
                });
            }
        }

        public static T Get<T>(string entity_id = null) where T : CMSEntity
        {
            if (entity_id == null)
                entity_id = E.Id<T>();
            var findById = all.FindById(entity_id) as T;

            if (findById == null)
            {
                throw new Exception("unable to resolve entity id '" + entity_id + "'");
            }

            return findById;
        }


        public static T GetAsTag<T>(string entity_id = null) where T : EntityComponentDefinition, new()
        {
            return Get<CMSEntity>(entity_id).GetTag<T>();
        }

        public static List<T> GetAll<T>() where T : CMSEntity
        {
            var allSearch = new List<T>();

            foreach (var a in all.GetAll())
                if (a is T)
                    allSearch.Add(a as T);

            return allSearch;
        }
        
        public static List<CMSEntity> GetAllWithTag<T>() where T: EntityComponentDefinition, new(){
            
            var allSearch = new List<CMSEntity>();
            
            foreach(var e in all.GetAll()){
                if(e.Is<T>()){
                    allSearch.Add(e);
                }
            }
            return allSearch;
        }

        public static List<(CMSEntity e, T tag)> GetAllTags<T>() where T : EntityComponentDefinition, new()
        {
            var allSearch = new List<(CMSEntity, T)>();

            foreach (var a in all.GetAll())
                if (a.Is<T>(out var t))
                    allSearch.Add((a, t));

            return allSearch;
        }

        public static void Unload()
        {
            isInit = false;
            all = new CMSTable<CMSEntity>();
        }
    }



    public static class E
    {
        public static string Id(Type getType)
        {
            return getType.FullName;
        }

        public static string Id<T>() where T : CMSEntity
        {
            return ID<T>.Get();
        }
    }

    static class ID<T> where T : CMSEntity
    {
        static string cache;

        public static string Get()
        {
            if (cache == null)
                cache = typeof(T).FullName;
            return cache;
        }
    }
}


