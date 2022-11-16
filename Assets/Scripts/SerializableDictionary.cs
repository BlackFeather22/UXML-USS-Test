using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey,TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    TKey defaultKey;
    TValue defaultValue;

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey,TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        while(keys.Count != values.Count)
        {
            if(keys.Count > values.Count)
            {
                if(keys.FindAll((key)=> Equals(key,defaultKey)).Count > 1)
                {
                    Debug.LogError("Please repalce or remove the default key in the dictionary");
                    keys.RemoveAt(keys.Count - 1);
                    break;
                }

                keys[keys.Count - 1] = defaultKey;
                values.Add(defaultValue);
            }
            else if (keys.Count < values.Count)
                values.RemoveAt(values.Count - 1);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }

    }

}

[System.Serializable]
public class Dictionary_GameObject_GameObject: SerializableDictionary<GameObject, GameObject> { }
