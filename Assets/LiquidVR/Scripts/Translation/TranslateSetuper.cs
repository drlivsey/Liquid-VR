using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace Liquid.Translation
{
    public class TranslateSetuper : MonoBehaviour
    {
        [SerializeField] private TextAsset m_translationFile = null;
        [SerializeField] private BaseTranslator[] m_translateElements = null;

        public void GatherTranslatedItems() => m_translateElements = FindObjectsOfType<BaseTranslator>(true);

        public void GenerateTamplates()
        {
            if (TryFindUndefinedKeys(out var keys))
            {
                WriteKeys(keys);
            }
        }

        private bool TryFindUndefinedKeys(out List<string> keys)
        {
            keys = new List<string>();

            var xmlDocument = XDocument.Load($"{TranslatorConstants.FullTranslationFilesPath}{m_translationFile.name}.xml");

            if (xmlDocument == null)
                return false;

            var elements = xmlDocument
                            .Descendants("item")
                            .Elements();

            var keysValues = m_translateElements.Select(x => x.ObjectKey);

            foreach (var key in keysValues)
            {
                if (elements.Any(x => x.Parent.Attribute("key").Value == key))
                    continue;
                
                keys.Add(key);
            }

            return true;
        }

        private void WriteKeys(List<string> keys)
        {
            var xmlDocument = XDocument.Load($"{TranslatorConstants.FullTranslationFilesPath}{m_translationFile.name}.xml");

            if (xmlDocument == null)
                return;
            
            var target = xmlDocument.Descendants("targets").Elements().First().Parent;

            foreach (var key in keys)
            {
                var element = new XElement("item", 
                    new XAttribute("key", key), 
                    new XAttribute("name", m_translateElements.Where(x => x.ObjectKey == key).First().name),
                    new object[] 
                    { 
                        new XElement("RU", "RU"),
                        new XElement("UA", "UA"),
                        new XElement("EN", "EN")
                    }
                );

                target.Add(element);
            }

            xmlDocument.Save($"{TranslatorConstants.FullTranslationFilesPath}{m_translationFile.name}.xml");
        }        
    }
}