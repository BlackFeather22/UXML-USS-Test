using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI_Manager
{
    public class MainMenuUI : UIWindow
    {
        [SerializeField] UnityEvent StartButtonCallback;
        [SerializeField] UnityEvent OptionsButtonCallback;
        // Start is called before the first frame update
        void OnEnable()
        {
            uiDocument.rootVisualElement.Q<Button>("Start")
                .RegisterCallback<MouseUpEvent>((e) => StartButtonCallback.Invoke());
            uiDocument.rootVisualElement.Q<Button>("Options")
                .RegisterCallback<MouseUpEvent>((e) => OptionsButtonCallback.Invoke());
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
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