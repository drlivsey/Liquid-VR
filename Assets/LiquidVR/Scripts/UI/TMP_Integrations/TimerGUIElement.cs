using UnityEngine;
using TMPro;
using Liquid.Utils;
using Liquid.Core;

namespace Liquid.UI
{
    public class TimerGUIElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_targetTextField = null;
        [SerializeField] private Timer m_targetTimer = null;

        private void OnEnable()
        {
            m_targetTimer?.OnTick.AddListener(UpdateTime);
        }

        private void OnDisable()
        {
            m_targetTimer?.OnTick.RemoveListener(UpdateTime);
        }

        private void UpdateTime()
        {
            if (m_targetTextField)
            {
                m_targetTextField.text = Converter.SecondsToTime(m_targetTimer.Time);
            }
        }
    }
}