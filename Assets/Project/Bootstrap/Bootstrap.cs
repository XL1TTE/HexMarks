
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Bootstrap{
    
    
    public class Bootsrap {
                
        private static readonly string m_BootSceneName = "BOOT";
                
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Boot(){

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == m_BootSceneName && scene.isLoaded) { return; }
            }

            SceneManager.LoadScene(m_BootSceneName, LoadSceneMode.Additive);
        }
    }
}
