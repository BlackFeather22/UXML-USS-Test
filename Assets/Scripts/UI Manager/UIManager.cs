using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UI_Manager
{
    public class UIManager: MonoBehaviour
    {
        public Dictionary<string, UIWindow> UIWindows { get; private set; } = new();

        [SerializeField] private UIWindow startingWindow;
        private UIWindow _openWindow;
        private void Start()
        {
            OpenStartingWindow();
        }

        public void OpenStartingWindow()
        {
            if(startingWindow) OpenWindow(startingWindow);
            else
            {
                Debug.LogError("Please add a reference to a starting window in the UIManager " +
                               "if you want the UIManager to initialize the first UI Window for you",this);
            }
        }
        
        public void OpenWindow(UIWindow uiWindow)
        {
            OpenWindowAsync(uiWindow.TypeName);
        }
        
        public void OpenWindow(string uiWindowName)
        {
            OpenWindowAsync(uiWindowName);
        }
        
        public async Task OpenWindowAsync(UIWindow uiWindow)
        {
            await CloseCurrentWindowAsync();

            if (UIWindows.TryGetValue(uiWindow.TypeName, out var window))
            {
                _openWindow = window;
                await window.OpenWindowAsync();
            }
            else
            {
                Debug.LogError("The requested window does not exist under the UIManager." +
                               "\nPlease add it under the UIManager in the Scene or Prefab");
            }
        }
        
        public async Task OpenWindowAsync(string uiWindowName)
        {
            await CloseCurrentWindowAsync();

            if (UIWindows.TryGetValue(uiWindowName, out var window))
            {
                _openWindow = window;
                await window.OpenWindowAsync();
            }
            else
            {
                Debug.LogError("The requested window does not exist under the UIManager." +
                               "\nPlease add it under the UIManager in the Scene or Prefab");
            }
        }
        
        public void CloseCurrentWindow()
        {
            if(_openWindow) _openWindow.CloseWindowAsync();
        }
        public async Task CloseCurrentWindowAsync()
        {
            if(_openWindow) await _openWindow.CloseWindowAsync();
        }
    }
}