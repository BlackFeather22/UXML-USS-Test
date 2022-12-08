using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UI_Manager
{
    public class OptionsMenuUI : UIWindow
    {
        [SerializeField] private UnityEvent MainMenuButtonClickCallback = null;
        
        private const string Audio = "Audio";
        private const string Credits = "Credits";
        private const string MainMenu = "MainMenu";
        
        private Button AudioButton;
        private Button CreditsButton;
        private Button MainMenuButton;
        public override void RegisterCallback()
        {
            AudioButton = uiDocument.rootVisualElement.Q<Button>(Audio);
            CreditsButton = uiDocument.rootVisualElement.Q<Button>(Credits);
            MainMenuButton = uiDocument.rootVisualElement.Q<Button>(MainMenu);
            
            MainMenuButton.RegisterCallback<MouseUpEvent>((e) => MainMenuButtonClickCallback.Invoke());
        }

        public override async Task OpenWindowAsync()
        { 
            EnableUI();
            
            var root = uiDocument.rootVisualElement.style;

            root.opacity = 0;


            await DOTween.To(() => root.opacity.value,
                x => root.opacity = x,
                1,
                1)
                .AsyncWaitForCompletion();
        }

        public override async Task CloseWindowAsync()
        {
            var root = uiDocument.rootVisualElement.style;

            await DOTween.To(() => root.opacity.value,
                x => root.opacity = x,
                0,
                1)
                .AsyncWaitForCompletion();

            DisableUI();
        }
    }
}