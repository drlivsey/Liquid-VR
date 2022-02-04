using UnityEngine;

namespace Liquid.Translation
{
    public abstract class BaseTranslator : MonoBehaviour, ITranslatable
    {
        [SerializeField] private string m_key = string.Empty;
        
        public string ObjectKey
        {
            get => m_key;
            set => m_key = value;
        }

        private void OnValidate()
        {
            InitializeComponent();
        }

        public abstract void Translate(string cultureValue);
        public abstract void InitializeComponent();
    }
}
