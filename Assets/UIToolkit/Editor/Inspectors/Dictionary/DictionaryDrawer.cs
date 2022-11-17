using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.ComponentModel;


[CustomPropertyDrawer(typeof(Dictionary_ints))]
[CustomPropertyDrawer(typeof(Dictionary_GameObject_GameObject))]
[CustomPropertyDrawer(typeof(Dictionary_Int_Color))]
//[CustomPropertyDrawer(typeof(NewMonoBehaviour))] This works also for properies
public class DictionaryDrawer : PropertyDrawer
{
    readonly string path = "Assets/UIToolkit/Editor/Inspectors/Dictionary/Dictionary.uxml";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement myInspector = new VisualElement();
        
        var vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        vsTree.CloneTree(myInspector);

        myInspector.Q<Button>("Add").clickable.clicked += () =>
        {
            var dictionary = property.FindPropertyRelative("keys");
            dictionary.arraySize++;
            property.serializedObject.ApplyModifiedProperties();
        };

        myInspector.Q<Button>("Remove").clickable.clicked += () =>
        {
            var dictionary = property.FindPropertyRelative("keys");
            if(dictionary.arraySize > 0) dictionary.arraySize--;
            property.serializedObject.ApplyModifiedProperties();
        };

        return myInspector;
    }
}
