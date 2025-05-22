using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class InPlayMenuController : MonoBehaviour{
                
        [SerializeField] Button m_ExitButton;
        [SerializeField] KeyCode m_Bind;
        
        [SerializeField] GameObject MenuObject;

        bool isMenuActive;
        
        public static bool inMainMenu = false;

        void Awake()
        {
            HideMenu();
            
            m_ExitButton.onClick.AddListener(ExitGame);
        }


        void OnDestroy()
        {
            m_ExitButton.onClick.RemoveAllListeners();
        }


        void Update()
        {
            if(inMainMenu){return;}
            
            if(Input.GetKeyDown(m_Bind)){
                if(isMenuActive){HideMenu();} else{ShowMenu();}
            }
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void ShowMenu()
        {
            isMenuActive = true;
            MenuObject.SetActive(true);
        }

        private void HideMenu()
        {
            isMenuActive = false;
            MenuObject.SetActive(false);
        }
    }
}
