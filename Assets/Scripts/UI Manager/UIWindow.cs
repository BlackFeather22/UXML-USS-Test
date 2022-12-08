using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Manager
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIWindow : MonoBehaviour
    {
        public string TypeName;
        
        [SerializeField] protected UIDocument uiDocument;

        protected virtual void OnEnable()
        {
            RegisterCallback();
        }

        private void OnValidate()
        {
            TypeName = GetType().Name;
        }

        private void Awake()
        {
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

        public void EnableUI()
        {
            uiDocument.enabled = true;
            this.enabled = true;
        }

        public void DisableUI()
        {
            uiDocument.enabled = false;
            this.enabled = false;
        }

        public virtual void RegisterCallback() { }

        public abstract Task OpenWindowAsync();
        public abstract Task CloseWindowAsync();

        

    }
}