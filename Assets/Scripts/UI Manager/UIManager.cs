using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
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
            _openWindow = startingWindow;
            if(startingWindow) OpenWindow(startingWindow);
            else
            {
                Debug.LogError("Please add a reference to a starting window in the UIManager " +
                               "if you want the UIManager to initialize the first UI Window for you",this);
            }
        }
        
        public void OpenWindow(UIWindow uiWindow)
        {
            CloseCurrentWindow();

            if (UIWindows.TryGetValue(uiWindow.GetType().Name, out var window))
            {
                _openWindow = window;
                window.PlayOpenAnimation();
            }
            else
            {
                Debug.LogError("The requested window does not exist under the UIManager." +
                               "\nPlease add it under the UIManager in the Scene or Prefab");
            }
        }
        
        public void OpenWindow(string uiWindowName)
        {
            CloseCurrentWindow();

            if (UIWindows.TryGetValue(uiWindowName, out var window))
            {
                _openWindow = window;
                window.PlayOpenAnimation();
            }
            else
            {
                Debug.LogError("The requested window does not exist under the UIManager." +
                               "\nPlease add it under the UIManager in the Scene or Prefab");
            }
        }
        
        public void CloseCurrentWindow()
        {
            if(_openWindow) _openWindow.PlayCloseAnimation();
        }
    }
}