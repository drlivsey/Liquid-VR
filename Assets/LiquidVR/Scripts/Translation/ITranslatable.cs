using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Liquid.Translation
{
    [CreateAssetMenu(fileName = "TranslatorSettings", menuName = "Liquid VR/Translator Settings", order = 0)]
    public class TranslatorConstants : ScriptableSingleton<TranslatorConstants>
    {
        [SerializeField] private string m_translationFilesPath = "Translation/";
        [SerializeField] private string m_fullTranslationFilePath = "Assets/Resources/Translation/";
        [SerializeField] private string m_defaultTranslationFileName = "Translation";
        [SerializeField] private string m_imageResourcesFolderPath = "Translation/Images";
        [SerializeField] private string m_audioResourcesFolderPath = "Translation/Audios";

        public static string TranslationFilesPath 
        {
            get => instance.m_translationFilesPath;
        }
        public static string FullTranslationFilesPath 
        {
            get => instance.m_fullTranslationFilePath;
        }
        public static string DefaultTranslationFileName
        {
            get => instance.m_defaultTranslationFileName;
        }
        public static string ImageResourcesFolderPath
        {
            get => instance.m_imageResourcesFolderPath;
        }
        public static string AudioResourcesFolderPath
        {
            get => instance.m_audioResourcesFolderPath;
        }
    }
    interface ITranslatable
    {
        void Translate(string cultureValue);
    }
}
