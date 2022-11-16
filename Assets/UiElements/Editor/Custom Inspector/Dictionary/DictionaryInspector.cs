using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


[CustomEditor(typeof(Test))]
public class DictionaryInspector : Editor
{
    public VisualTreeAsset m_InspectorXML;
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Add a simple label
        myInspector.Add(new Label("Serializable Dictionary"));

        // Load and clone a visual tree from UXML
        m_InspectorXML.CloneTree(myInspector);

        myInspector.Q<Button>("Add").clickable.clicked += () =>
        {
            var dictionary = serializedObject.FindProperty("dictionary.keys");
            dictionary.arraySize++;
            serializedObject.ApplyModifiedProperties();
        };

        myInspector.Q<Button>("Remove").clickable.clicked += () =>
        {
            var dictionary = serializedObject.FindProperty("dictionary.keys");
            dictionary.arraySize--;
            serializedObject.ApplyModifiedProperties();
        };

        // Return the finished inspector UI
        return myInspector;
    }
}
