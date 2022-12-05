using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;


public class AnotherEditor : EditorWindow
{
    static VisualElement editorWindowUI;
    [MenuItem("Custom Editors/First Editor Window")]
    public static void ShowExample()
    {
        AnotherEditor wnd = GetWindow<AnotherEditor>();
        wnd.titleContent = new GUIContent("AnotherEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIToolkit/Editor/Editor Windows/AnotherEditor.uxml");
        editorWindowUI = visualTree.Instantiate();

        editorWindowUI.Q<Button>("so-btn").clicked += showScriptableObjects;

        root.Add(editorWindowUI);
    }

    private void showScriptableObjects()
    {
        Debug.Log("entered method");

        FindAllObjects<Object>(out Object[] a);
        Box list = new Box();
        list.Clear();

        foreach (var scriptable in a)
        {
            var objectTitle = new Label();
            objectTitle.text = scriptable.name;
            objectTitle.AddToClassList("object-title");
            list.Add(objectTitle);

            SerializedObject scriptableObject = new SerializedObject(scriptable);
            SerializedProperty scriptableProperty = scriptableObject.GetIterator();
            scriptableProperty.Next(true);

            while (scriptableProperty.NextVisible(false))
            {
                PropertyField propertyField = new PropertyField(scriptableProperty);
                
                propertyField.SetEnabled(scriptableProperty.name != "m_Script");
                propertyField.Bind(scriptableObject);
                propertyField.AddToClassList("scriptable-obj");

                list.Add(propertyField);
            }

        }

        editorWindowUI.Query<ScrollView>().First().Add(list);
    }

    private void FindAllObjects<T>(out Object[] objects)
    {
        var guids = AssetDatabase.FindAssets("t:SO1");

        objects = new Object[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            objects[i] = AssetDatabase.LoadAssetAtPath<Object>(path);
        }
    }
    

}