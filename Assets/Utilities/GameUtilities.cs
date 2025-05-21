using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameUtilities{
    public static class GameUtility{
        
        public static IEnumerator FlyToCenterOfScreen(GameObject obj, float duration, Vector3 offset = default){

            Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));

            Tween tween = obj.transform.DOMove(center + offset, duration); 
            
            yield return tween.WaitForCompletion();
        }
        public static IEnumerator FlyToPosition(GameObject obj, Vector3 targetPos, float duration){

            Tween tween = obj.transform.DOMove(targetPos, duration); 
            
            yield return tween.WaitForCompletion();
        }
        
    }
}
