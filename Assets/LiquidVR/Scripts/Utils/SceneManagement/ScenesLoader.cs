using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Liquid.Utils
{
    public class ScenesLoader : MonoBehaviour
    {
        [SerializeField] private string m_sceneName = string.Empty;
        [SerializeField] private LoadSceneMode m_loadingMode = LoadSceneMode.Single;

        [SerializeField] private UnityEvent m_onLoadingBegin = new UnityEvent();
        [SerializeField] private UnityEvent m_onLoadingComplete = new UnityEvent();

        public string SceneName
        {
            get => m_sceneName;
            set => m_sceneName = value;
        }

        public LoadSceneMode LoadingMode
        {
            get => m_loadingMode;
            set => m_loadingMode = value;
        }

        public UnityEvent OnLoadingBegin  => m_onLoadingBegin;
        public UnityEvent OnLoadingComplete => m_onLoadingComplete;

        public void LoadScene()
        {
            LoadSceneByName(m_sceneName);
        }

        public void LoadSceneByName(string name)
        {
            m_onLoadingBegin?.Invoke();
            SceneManager.LoadScene(name, m_loadingMode);
            m_onLoadingComplete?.Invoke();
        }

        public void LoadSceneAsync()
        {
            LoadSceneByNameAsync(m_sceneName);
        }

        public void LoadSceneByNameAsync(string name)
        {
            StartCoroutine(LoadAsync(name));
        }

        private IEnumerator LoadAsync(string name)
        {
            m_onLoadingBegin?.Invoke();

            var operation = SceneManager.LoadSceneAsync(name, m_loadingMode);
            while (!operation.isDone) 
            {
                yield return new WaitForEndOfFrame();
            }

            m_onLoadingComplete?.Invoke();
        }
    }
}