using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    [SerializeField] UnityEvent StartButtonCallback;
    // Start is called before the first frame update
    void Start()
    {
        uIDocument = GetComponent<UIDocument>();


        //var a = uIDocument.rootVisualElement.Q<Button>("Start");
        //a.clicked += StartButtonCallback.Invoke;
        //uIDocument.rootVisualElement.Query<Button>("Start").First().clicked += StartButtonCallback.Invoke;
        uIDocument.rootVisualElement.Q<Button>("Start").RegisterCallback<MouseUpEvent>((e) => StartButtonCallback.Invoke());
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
