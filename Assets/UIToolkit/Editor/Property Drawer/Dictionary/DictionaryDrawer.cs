using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.ComponentModel;
using Codice.Client.BaseCommands;


[CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
public class DictionaryDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        Debug.Log("CreatePropertyGUI");
        // Create a new VisualElement to be the root of our inspector UI

        VisualElement myInspector = new VisualElement();
        var vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIToolkit/Editor/Property Drawer/Dictionary/Dictionary.uxml");
        var drawer = vsTree.CloneTree(property.propertyPath);

        drawer.Q<Button>("Add").clickable.clicked += () =>
        {
            var dictionary = property.FindPropertyRelative("keys");
            dictionary.arraySize++;
            property.serializedObject.ApplyModifiedProperties();
        };

        drawer.Q<Button>("Remove").clickable.clicked += () =>
        {
            var dictionary = property.FindPropertyRelative("keys");
            dictionary.arraySize--;
            property.serializedObject.ApplyModifiedProperties();
        };

        myInspector.Add(drawer);


        // Return the finished inspector UI
        return myInspector;


    }
}
