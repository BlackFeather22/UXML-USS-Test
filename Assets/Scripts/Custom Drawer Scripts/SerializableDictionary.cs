using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SerializableDictionary<TKey,TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] protected TKey keyToAdd;
    [SerializeField] protected TValue valueToAdd;
    
    [SerializeField] protected List<TKey> keys = new();
    [SerializeField] protected List<TValue> values = new();
    
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
        this.Clear();

        while(keys.Count != values.Count)
        {
            if(keys.Count > values.Count)
            {
                // if(keys.FindAll((key)=> Equals(key,keyToAdd)).Count > 1)
                // {
                //     keys.RemoveAt(keys.Count - 1);
                //     break;
                // }

                keys[keys.Count - 1] = keyToAdd;
                values.Add(valueToAdd);
            }
            else if (keys.Count < values.Count)
                values.RemoveAt(values.Count - 1);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            try
            {
                this.Add(keys[i], values[i]);
            }
            catch (Exception e)
            {
                Debug.LogError("Key Already Exists");
            }
            
        }

    }
}
