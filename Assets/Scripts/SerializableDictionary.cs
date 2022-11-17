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



    public virtual void OnBeforeSerialize()
    {
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
                if(keys.FindAll((key)=> Equals(key,defaultKey)).Count > 1)
                {
                    Debug.LogWarning("Please replace or remove the default key in the dictionary");
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
            Add(keys[i], values[i]);
        }

    }

}

[System.Serializable]
public class Dictionary_GameObject_GameObject: SerializableDictionary<GameObject, GameObject> {

    GameObject defaultKeyAndValue = null;

    public override void OnAfterDeserialize()
    {
        defaultKey = defaultKeyAndValue;
        defaultValue = defaultKeyAndValue;
        base.OnAfterDeserialize();
    }
}

[System.Serializable]
public class Dictionary_ints : SerializableDictionary<int, int>
{
}
