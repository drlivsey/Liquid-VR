using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private bool m_initOnEnable = false;

        private List<TabButton> _tabsList = new List<TabButton>();
        private TabButton _currentTab = null;

        public void RegisterTab(TabButton button)
        {
            if (_tabsList.Contains(button)) return;
            if (_tabsList.Count == 0 && m_initOnEnable) 
            {
                _currentTab = button;
                button.SetSelected();
            }
            _tabsList.Add(button);
        }

        public void UnregisterTab(TabButton button)
        {
            if (_tabsList.Contains(button))
                _tabsList.Remove(button);
        }

        public void OnTabHoverEntered(TabButton button)
        {
            if (IsValidTab(button) == false) return;
            if (button != _currentTab) button.SetHovered();
        }

        public void OnTabHoverExited(TabButton button)
        {
            if (IsValidTab(button) == false) return;
            if (button != _currentTab) button.SetDefault();
        }

        public void OnTabClick(TabButton button)
        {
            if (IsValidTab(button) == false) return;
            foreach (var tab in _tabsList)
            {
                if (tab == button) continue;
                tab.SetDeselected();
            }
            
            _currentTab = button;
            button.SetSelected();
        }

        private bool IsValidTab(TabButton button)
        {
            return _tabsList.Contains(button);
        }
    }
}