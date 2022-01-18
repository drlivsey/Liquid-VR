using UnityEngine;
using TMPro;

namespace Liquid.Translation
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranlslator : BaseTranslator
    {
        [SerializeField] private TMP_Text m_translationTarget = null;

        public override void Translate(string cultureValue)
        {
            m_translationTarget.text = cultureValue;
        }

        public override void InitializeComponent()
        {
            m_translationTarget = m_translationTarget == null ? GetComponent<TMP_Text>() : m_translationTarget;
            base.InitializeComponent();
        }
    }
}
