using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Liquid.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image m_fillImage = null;
        [SerializeField] private BarType m_barType = BarType.Clamped;
        [SerializeField] private bool m_displayProgress = false;
        [SerializeField] private TMP_Text m_progressTextField = null;
        
        private void Awake()
        {
            InitializeComponent();
        }

        public void UpdateProgress(float value)
        {
            UpdateImageFill(value);
            UpdateTextField();
        }

        private void InitializeComponent()
        {
            if (m_fillImage == null) return;
            m_fillImage.type = Image.Type.Filled;
            m_fillImage.fillMethod = Image.FillMethod.Horizontal;
            
            if (m_displayProgress == false && m_progressTextField != null)
            {
                m_progressTextField.gameObject.SetActive(false);
            }
        }

        private void UpdateImageFill(float value)
        {
            if (m_fillImage == null) return;
            m_fillImage.fillAmount = Mathf.Clamp(value / (float)m_barType, 0f, (float)m_barType);
        }

        private void UpdateTextField()
        {
            if (m_displayProgress == false) return;
            if (m_progressTextField == null) return;
            m_progressTextField.text = $"{(int)(m_fillImage.fillAmount * 100)} %";
        }

        private enum BarType 
        {
            Clamped = 1,
            Full = 100
        }
    }
}