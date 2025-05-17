using UnityEngine;
using UnityEngine.Rendering;

namespace Project.ObjectInteractions{
    public interface IHaveInteractions 
    {
        Transform GetTransform();
        SpriteRenderer GetRenderer();
        SortingGroup GetSortingGroup(); 
        
        Collider2D GetCollider();
    }
}

