using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Battle
{
    public class Enviroment : MonoBehaviour
    {
        void Awake()
        {
            LEFT_TOP_OUT_OF_SCREEN = LeftTop_OutOfScreen;
            LEFT_BOTTOM_OUT_OF_SCREEN = LeftBottom_OutOfScreen;
            TOP_CENTER_OUT_OF_SCREEN = TopCenter_OutOfScreen;
        }

        [SerializeField] Transform LeftTop_OutOfScreen;
        [SerializeField] Transform LeftBottom_OutOfScreen;
        [SerializeField] Transform TopCenter_OutOfScreen;
        
        
        public static Transform LEFT_TOP_OUT_OF_SCREEN {get; private set;}
        public static Transform LEFT_BOTTOM_OUT_OF_SCREEN { get; private set; }
        public static Transform TOP_CENTER_OUT_OF_SCREEN { get; private set; }
    }
}
