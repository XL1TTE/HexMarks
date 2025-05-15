using System.Collections.Generic;
using UnityEngine;

namespace XL1TTE.Animator{
    
    public static class Extantions{
        
        public static FrameAnimation ToFrameAnimation
        (
            this SpriteRenderer renderer, 
            IEnumerable<Sprite> sprites,
            float framesDuration = 100)
        {
            
            List<FrameAnimation.Frame> frames = new ();

            foreach (var sprite in sprites){
                var frame = new FrameAnimation.Frame(sprite, framesDuration);
                frames.Add(frame);
            }
            return new FrameAnimation(renderer, frames);
        }
        
    }
    
    
    public static class YieldExtentions{
        public static WaitForCompletion WaitForCompletion(this xlAnimation animation){
            return new WaitForCompletion(animation);
        }
    }
}
