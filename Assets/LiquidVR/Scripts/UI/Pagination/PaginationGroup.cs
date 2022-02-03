using System.Collections.Generic;
using UnityEngine;

namespace Liquid.UI
{
    public class PaginationGroup : MonoBehaviour
    {
        [SerializeField] private bool m_loop = false;
        [SerializeField] private List<Page> m_pagesList = new List<Page>();

        public int CurrentPage => _currentIndex;

        private int _currentIndex = 0;

        public void RegisterPage(Page page)
        {
            if (m_pagesList.Contains(page)) return;
            m_pagesList.Add(page);
        }

        public void UnregisterPage(Page page)
        {
            if (m_pagesList.Contains(page))
            {
                m_pagesList.Add(page);
            }
        }

        public void ShowNext()
        {
            if (TryGetNextPageIndex(out var index))
            {
                ShowByIndex(index);
            }
        }

        public void ShowPrevious()
        {
            if (TryGetPreviousPageIndex(out var index))
            {
                ShowByIndex(index);
            }
        }

        public void ShowByIndex(int index)
        {
            if (index < 0 || index >= m_pagesList.Count) return;

            _currentIndex = index;
            for (var i = 0; i < m_pagesList.Count; i++)
            {
                var page = m_pagesList[i];
                if (i == index) page.Show();
                else page.Hide();
            }
        }

        private bool TryGetNextPageIndex(out int index)
        {
            index = _currentIndex + 1;
            if (index >= m_pagesList.Count)
            {
                if (m_loop)
                {
                    index = 0;
                    return true;
                }
                else
                {
                    index = -1;
                    return false;
                }
            }
            else 
            {
                return true;
            }
        }

        private bool TryGetPreviousPageIndex(out int index)
        {
            index = _currentIndex - 1;
            if (index < 0)
            {
                if (m_loop)
                {
                    index = m_pagesList.Count - 1;
                    return true;
                }
                else
                {
                    index = -1;
                    return false;
                }
            }
            else 
            {
                _currentIndex--;
                return true;
            }
        }
    }
}