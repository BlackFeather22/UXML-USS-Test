using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey,TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] protected TKey defaultKey;
    [SerializeField] protected TValue defaultValue;

    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    private bool isTyping;


    public virtual void OnBeforeSerialize()
    {
        if(isTyping) return;

        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey,TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public virtual void OnAfterDeserialize()
    {
        Clear();

        while(keys.Count != values.Count)
        {
            if(keys.Count > values.Count)
            {
                keys[^1] = defaultKey;
                values.Add(defaultValue);
            }
            else if (keys.Count < values.Count)
                values.RemoveAt(values.Count - 1);
        }

        try
        {
            for (int i = 0; i < keys.Count; i++)
            {
                Add(keys[i], values[i]);
            }

            isTyping = false;
        }
        catch
        {
            isTyping = true;
            Debug.LogWarning(
                "There is a duplicate key in the dictionary.\nreplace the duplicate key to avoid unexpected behaviour.");
        }


    }

}

[System.Serializable]
public class Dictionary_GameObject_GameObject: SerializableDictionary<GameObject, GameObject> {}

[System.Serializable]
public class Dictionary_ints : SerializableDictionary<int, int>{}

[System.Serializable]
public class Dictionary_Int_Color : SerializableDictionary<int, Color> { }