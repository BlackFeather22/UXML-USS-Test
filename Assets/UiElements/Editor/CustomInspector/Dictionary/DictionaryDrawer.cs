using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.ComponentModel;


//[CustomPropertyDrawer(typeof(Dictionary_ints))]
[CustomPropertyDrawer(typeof(NewMonoBehaviour))]
public class DictionaryDrawer : PropertyDrawer
{
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    EditorGUI.BeginProperty(position, label, property);

    //    base.OnGUI(position, property, label);
    //    var a = CreatePropertyGUI(property);

    //    EditorGUI.PropertyField(position, property);

    //    EditorGUI.EndProperty();


    //}

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        Debug.Log("CreatePropertyGUI");
        // Create a new VisualElement to be the root of our inspector UI

        VisualElement myInspector = new VisualElement();
        var vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UiElements/Editor/CustomInspector/Dictionary/Dictionary.uxml");
        var drawer = vsTree.CloneTree(property.propertyPath);
        myInspector.style.backgroundColor = UnityEngine.Random.ColorHSV();

        myInspector.Q<Button>("Add").clickable.clicked += () =>
        {
            var dictionary = property.serializedObject.FindProperty("keys");
            dictionary.arraySize++;
            property.serializedObject.ApplyModifiedProperties();
        };

        myInspector.Q<Button>("Remove").clickable.clicked += () =>
        {
            var dictionary = property.serializedObject.FindProperty("keys");
            dictionary.arraySize--;
            property.serializedObject.ApplyModifiedProperties();
        };

        myInspector.Add(drawer);


        // Return the finished inspector UI
        return myInspector;


    }
}
