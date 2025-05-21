using System;
using System.Linq;
using System.Reflection;

namespace GameUtilities{
    public static class ReflectionUtility
    {
        public static Type[] GetSubclasses<T>()
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes;

            return types.Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract).ToArray();
        }
    }
}
