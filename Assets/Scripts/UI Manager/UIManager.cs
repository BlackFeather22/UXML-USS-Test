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
            if(startingWindow) startingWindow.PlayOpenAnimation();
            else
            {
                Debug.LogError("Please add a reference to a starting window in the UIManager",this);
            }
        }
        
        public void OpenWindow(string windowType)
        {
            CloseCurrentWindow();

            if (UIWindows.TryGetValue(windowType, out var window)) window.PlayOpenAnimation();
            else
            {
                Debug.LogError("The requested window does not exist under the UIManager." +
                               "\nPlease add it under the UIManager in the Scene or Prefab");
            }
        }
        
        public void CloseCurrentWindow()
        {
            _openWindow.PlayCloseAnimation();
        }
    }
}