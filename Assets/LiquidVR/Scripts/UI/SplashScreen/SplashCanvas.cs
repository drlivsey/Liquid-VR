using UnityEngine;
using Liquid.Utils;

namespace Liquid.UI
{
    public class SplashCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas m_targetCanvas = null;
        [SerializeField] private ScaleProceduralAnimator m_animator = null;

        public void Show()
        {
            if (m_animator)
            {
                m_animator.Animate();
                return;
            }

            m_targetCanvas.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (m_animator)
            {
                m_animator.AnimateReversed();
                return;
            }

            m_targetCanvas.gameObject.SetActive(false);
        }
    }
}