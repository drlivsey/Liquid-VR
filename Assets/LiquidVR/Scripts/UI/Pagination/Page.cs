using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.UI
{
    public class Page : MonoBehaviour
    {
        [SerializeField] private PaginationGroup m_paginationGroup = null;
        [SerializeField] private GameObject m_pageObject = null;
        [SerializeField] private UnityEvent m_onShow = new UnityEvent();
        [SerializeField] private UnityEvent m_onHide = new UnityEvent();

        public UnityEvent OnShow => m_onShow;
        public UnityEvent OnHide => m_onHide;

        private void OnEnable() 
        {
            m_paginationGroup?.RegisterPage(this);
        }

        private void OnDisable() 
        {
            m_paginationGroup?.UnregisterPage(this);
        }

        public virtual void Show()
        {
            m_pageObject?.SetActive(true);
            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            m_pageObject?.SetActive(false);
            OnHide?.Invoke();
        }
    }
}