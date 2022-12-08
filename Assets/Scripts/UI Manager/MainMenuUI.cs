using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;

namespace UI_Manager
{
    public class MainMenuUI : UIWindow
    {
        [SerializeField] UnityEvent StartButtonCallback;
        [SerializeField] UnityEvent OptionsButtonCallback;
        // Start is called before the first frame update

        private const string Start = "Start";
        private const string Options = "Options";
        private const string Quit = "Quit";
        
        private Button startButton;
        private Button optionsButton;
        private Button quitButton;
        public override void RegisterCallback()
        {
            startButton = uiDocument.rootVisualElement.Q<Button>(Start);
            startButton.RegisterCallback<MouseUpEvent>((e) => StartButtonCallback.Invoke());
            optionsButton = uiDocument.rootVisualElement.Q<Button>(Options);
            optionsButton.RegisterCallback<MouseUpEvent>((e) => OptionsButtonCallback.Invoke());
            quitButton = uiDocument.rootVisualElement.Q<Button>(Quit);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public override async Task OpenWindowAsync()
        { 
            EnableUI();
            
            var start = startButton.style;
            var options = optionsButton.style;
            var quit = quitButton.style;
            
            start.opacity = 0;
            options.opacity = 0;
            quit.opacity = 0;
            

            await DOTween.To(() => start.opacity.value,
                x => start.opacity = x,
                1,
                1)
                .AsyncWaitForCompletion();
            
            await DOTween.To(() => options.opacity.value,
                x => options.opacity = x,
                1,
                1)
                .AsyncWaitForCompletion();
            
            await DOTween.To(() => quit.opacity.value,
                x => quit.opacity = x,
                1,
                1)
                .AsyncWaitForCompletion();
        }

        public override async Task CloseWindowAsync()
        {
            var start = startButton.style;
            var options = optionsButton.style;
            var quit = quitButton.style;
            
            await DOTween.To(() => start.opacity.value,
                x => start.opacity = x,
                0,
                1)
                .AsyncWaitForCompletion();
            
            await DOTween.To(() => options.opacity.value,
                x => options.opacity = x,
                0,
                1)
                .AsyncWaitForCompletion();
            
            await DOTween.To(() => quit.opacity.value,
                x => quit.opacity = x,
                0,
                1)
                .AsyncWaitForCompletion();
            
            DisableUI();
        }
    }
}