using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UI_Manager
{
    public class OptionsMenuUI : UIWindow
    {
        [SerializeField] private UnityEvent MainMenuButtonClickCallback = null;
        private void OnEnable()
        {
            uiDocument.rootVisualElement.Q<Button>("MainMenu")
                .RegisterCallback<MouseUpEvent>((e) => MainMenuButtonClickCallback.Invoke());
        }

        public override void PlayOpenAnimation()
        {
            EnableUI();
        }

        public override void PlayCloseAnimation()
        {
            DisableUI();
        }
    }
}