using UnityEngine;
using UnityEngine.UI;

namespace Liquid.Translation
{
    [RequireComponent(typeof(Image))]
    public class ImageTranslator : BaseTranslator
    {
        [SerializeField] private Image m_translationTarget = null;

        public override void Translate(string cultureValue)
        {
            if (TryLoadSprite(cultureValue, out var requiredSprite))
                m_translationTarget.sprite = requiredSprite;
        }

        public override void InitializeComponent()
        {
            m_translationTarget = m_translationTarget == null ? GetComponent<Image>() : m_translationTarget;
        }

        private bool TryLoadSprite(string spriteName, out Sprite sprite)
        {
            sprite = null;

            var loadedSprite = Resources.Load<Sprite>(string.Join("/", TranslatorConstants.ImageResourcesFolderPath, spriteName));

            if (loadedSprite == null)
                return false;

            sprite = loadedSprite;
            return true;
        }
    }
}
