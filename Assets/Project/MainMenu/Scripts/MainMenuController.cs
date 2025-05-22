using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.MainMenu
{
    
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] Button m_PlayButton;
        [SerializeField] Button m_ExitButton;

        void Awake()
        {
            InPlayMenuController.inMainMenu = true;
        }

        void OnDestroy()
        {
            InPlayMenuController.inMainMenu = false;
        }

        void OnEnable()
        {
            m_PlayButton?.onClick.AddListener(OnPlayButtonClicked);
            m_ExitButton?.onClick.AddListener(OnExitButtonClicked);
        }

        void OnDisable()
        {
            m_PlayButton?.onClick.RemoveAllListeners();
            m_ExitButton?.onClick.RemoveAllListeners();

        }

        private void OnExitButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
        }
    }
}
