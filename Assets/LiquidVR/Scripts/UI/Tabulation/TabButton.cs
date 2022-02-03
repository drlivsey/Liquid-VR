using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Liquid.UI
{
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private bool m_interactable = true;
        [SerializeField] private Image m_targetGraphic = null;
        [SerializeField] private Sprite m_defaultSprite = null;
        [SerializeField] private Sprite m_disabledSprite = null;
        [SerializeField] private Sprite m_hoveredSprite = null;
        [SerializeField] private Sprite m_selectedSprite = null;
        [SerializeField] private GameObject m_tabObject = null;
        [SerializeField] private TabGroup m_tabGroup = null;

        [SerializeField] private UnityEvent m_onSelect = new UnityEvent();
        [SerializeField] private UnityEvent m_onDeselect = new UnityEvent();
        [SerializeField] private UnityEvent m_onHoverEntered = new UnityEvent();
        [SerializeField] private UnityEvent m_onHoverExited = new UnityEvent();
        

        public bool Interactable 
        {
            get => m_interactable;
            set 
            {
                m_interactable = value;
                if (value) SetSprite(m_defaultSprite);
                else SetSprite(m_disabledSprite);
            }
        }

        public UnityEvent OnSelect => m_onSelect;
        public UnityEvent OnDeselect => m_onDeselect;
        public UnityEvent OnHoverEntered => m_onHoverEntered;
        public UnityEvent OnHoverExited => m_onHoverExited;
        

        private void OnValidate()
        {
            if (m_targetGraphic == null) return;
            if (m_interactable) SetSprite(m_defaultSprite);
            else SetSprite(m_disabledSprite);
        }

        private void OnEnable()
        {
            m_tabGroup.RegisterTab(this);
        }

        private void OnDisable()
        {
            m_tabGroup.UnregisterTab(this);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (m_interactable == false) return;
            m_tabGroup.OnTabHoverEntered(this);
        }

        public void OnPointerExit(PointerEventData data)
        {
            if (m_interactable == false) return;
            m_tabGroup.OnTabHoverExited(this);
        }

        public void OnPointerDown(PointerEventData data)
        {
            if (m_interactable == false) return;
            m_tabGroup.OnTabClick(this);
        }

        public void SetSelected()
        {
            SetSprite(m_selectedSprite);
            m_tabObject.SetActive(true);
            OnSelect?.Invoke();
        }

        public void SetDeselected()
        {
            SetSprite(m_defaultSprite);
            m_tabObject.SetActive(false);
            OnDeselect?.Invoke();
        }

        public void SetHovered()
        {
            SetSprite(m_hoveredSprite);
            OnHoverEntered?.Invoke();
        }

        public void SetDefault()
        {
            SetSprite(m_defaultSprite);
            OnHoverExited?.Invoke();
        }

        private void SetSprite(Sprite sprite)
        {
            if (m_targetGraphic == null || sprite == null) return;

            m_targetGraphic.sprite = sprite;
        }
    }
}