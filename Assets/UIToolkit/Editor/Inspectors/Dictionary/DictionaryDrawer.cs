using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


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

        new Rigidbody();

        var vsTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        vsTree.CloneTree(myInspector);

        var keysList = property.FindPropertyRelative("keys");

        myInspector.Q<Button>("Add").clickable.clicked += () =>
        {
            keysList.arraySize++;
            property.serializedObject.ApplyModifiedProperties();
        };

        myInspector.Q<Button>("Remove").clickable.clicked += () =>
        {
            if(keysList.arraySize > 0) keysList.arraySize--;
            property.serializedObject.ApplyModifiedProperties();
        };

        return myInspector;
    }
}
