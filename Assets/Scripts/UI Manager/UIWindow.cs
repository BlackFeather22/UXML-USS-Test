using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Manager
{
    public abstract class UIWindow : MonoBehaviour
    {
        public static string WindowName;
        
        [SerializeField] private UIDocument uiDocument;
        private void Awake()
        {
            WindowName = GetType().Name;
            
            var uiManager = GetComponentInParent<UIManager>();
            
            if(uiManager) uiManager.UIWindows.Add(GetType().Name,this);
            else
            {
                Debug.LogWarning("UIWindows shall be added as a child of UIManager, they don't work on their own",this);
                uiManager = FindObjectOfType<UIManager>();

                if (!uiManager) return;
                
                transform.parent = uiManager.transform;
                uiManager.UIWindows.Add(GetType().Name,this);
                Debug.LogWarning("Therefore, this window is moved under an existing UIManager GameObject ",this);
            }
        }

        public abstract void PlayOpenAnimation();
        public abstract void PlayCloseAnimation();
        

    }
}