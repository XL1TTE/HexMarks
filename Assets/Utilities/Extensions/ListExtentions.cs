using System;
using System.Collections.Generic;

namespace Project.Utilities.Extantions{
    public static class ListExtantions{
        
        public static T Dequeue<T>(this List<T> list){
            if(list.Count == 0) {throw new Exception("List was empty. Nothing to dequeue"); }
            
            var temp = list[0];
            list.RemoveAt(0);
            return temp;
        }
        
        public static void Enqueue<T>(this List<T> list, T item){
            list.Add(item);
        }

        public static bool TryRemove<T>(this List<T> queue, Predicate<T> predicate)
        {
            foreach (var t in queue)
            {
                if (predicate.Invoke(t)) { queue.Remove(t); return true; }
            }
            return false;
        }
    } 
}
