using UnityEngine;

namespace Liquid.Translation
{
    public abstract class BaseTranslator : MonoBehaviour, ITranslatable
    {
        [SerializeField] private string m_key = string.Empty;

        public bool Initialized
        {
            get; protected set;
        }
        
        public string ObjectKey
        {
            get => m_key;
            set => m_key = value;
        }

        private void Awake()
        {
            InitializeComponent();
        }

        public abstract void Translate(string cultureValue);
        public virtual void InitializeComponent()
        {
            Initialized = true;
        }
    }
}
