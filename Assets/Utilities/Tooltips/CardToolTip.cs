using TMPro;
using UnityEngine;

namespace Project.Utilities{
    public class CardToolTip: MonoBehaviour{
        
        [SerializeField] TextMeshProUGUI Name;
        [SerializeField] TextMeshProUGUI Description;
        
        private static CardToolTip m_current;

        void Awake()
        {
            gameObject.SetActive(false);
            
            if(m_current == null){
                m_current = this;
            }
        }

        public static void Show(string name, string description){
            
            m_current.Name.text = name;

            m_current.Description.text = description;

            m_current.gameObject.SetActive(true);
        }
        
        public static void Hide(){
            m_current.gameObject.SetActive(false);
        }
    }

}
