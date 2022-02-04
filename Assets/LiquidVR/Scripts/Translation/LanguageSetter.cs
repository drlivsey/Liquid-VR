using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;

namespace Liquid.Translation
{
    public class LanguageSetter : MonoBehaviour
    {
        [SerializeField] private TextAsset m_translationFile = null;
        [SerializeField] protected static string m_language = string.Empty;
        [SerializeField] protected bool m_translateOnEnable = true;

        public static string CurrentLanguage
        {
            get => m_language; 
            private set 
            {
                m_language = value;

            }
        }

        private BaseTranslator[] _translators;
        private IEnumerable<XElement> _translatedItems = null;

        private void Awake() 
        {
            _translators = FindObjectsOfType<BaseTranslator>(true);
            _translatedItems = XDocument.Parse(m_translationFile.text)
                            .Descendants("item")
                            .Elements();
        }

        private void OnEnable()
        {
            if (m_translateOnEnable) 
                UpdateTranslation();
        }

        public void RefreshTargets()
        {
            _translators = FindObjectsOfType<BaseTranslator>(true);
        }

        public void RefreshTranslation()
        {
            RefreshTargets();
            UpdateTranslation();
        }

        public void SetLanguage(string language)
        {
            CurrentLanguage = language;
            UpdateTranslation();
        }

        private void UpdateTranslation()
        {
            foreach (var translator in _translators)
            {
                try
                {
                    if (TryGetCultureValue(translator.ObjectKey, out var value) == false)
                        continue;

                    translator.Translate(value);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{ex.Message} on: {translator.gameObject.name}");
                }
            }
        }

        private bool TryGetCultureValue(string objectKey, out string value)
        {
            value = string.Empty;

            if (_translatedItems == null)
                return false;

            if (_translatedItems.Any(x => x.Parent.Attribute("key").Value == objectKey) == false)
                return false;

            var element = _translatedItems
                            .First(x => x.Parent
                            .Attribute("key").Value == objectKey);
                            
            value = element.Parent.Element(m_language).Value;
            return true;
        }
    }
}