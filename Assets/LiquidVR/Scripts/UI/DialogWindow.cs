using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Liquid.Core;
using Liquid.Utils;

namespace Liquid.UI
{
    public class DialogWindow : MonoBehaviour
    {
        [SerializeField] private GameObject m_dialogWindowObject = null;
        [SerializeField] private bool m_animateDialogWindow = false;
        [SerializeField, HideInInspector] private BaseProceduralAnimator m_dialogWindowAnimator = null;

        public bool AnimatePopup 
        { 
            get => m_animateDialogWindow;
            set => m_animateDialogWindow = value;
        }

        public bool IsVisible
        {
            get; private set;
        }

        public DialogResult DialogResult
        {
            get; private set;
        }

        public async Task<DialogResult> ShowDialogAsync()
        {
            DialogResult = DialogResult.Undefind;

            Show();

            while (IsVisible)
            {
                await Task.Yield();
            }

            return DialogResult;
        }

        public IEnumerator ShowDialog()
        {
            DialogResult = DialogResult.Undefind;

            Show();

            var waitFor = new WaitForFixedUpdate();
            while (IsVisible)
            {
                yield return waitFor;
            }
        }

        public void Submit()
        {
            DialogResult = DialogResult.Success;
            Hide();
        }

        public void Cancel()
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void Show()
        {
            if (m_animateDialogWindow)
            {
                m_dialogWindowAnimator?.Animate();
            }
            else 
            {
                if (m_dialogWindowObject.activeSelf == true) return;
                m_dialogWindowObject.SetActive(true);
            }

            IsVisible = true;
        }

        private void Hide()
        {
            if (m_animateDialogWindow)
            {
                m_dialogWindowAnimator?.AnimateReversed();
            }
            else
            {
                if (m_dialogWindowObject.activeSelf == false) return;
                m_dialogWindowObject.SetActive(false);
            }

            IsVisible = false;
        }
    }
}