using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(Slider))]
    public class IProgressBar : MonoBehaviour{
        [SerializeField] protected Slider m_slider;
        public virtual float GetProgress() => m_slider.value;
        public virtual void UpdateProgress(float value) => m_slider.value = Mathf.Clamp(value, 0.0f, 1.0f);
    }
}
