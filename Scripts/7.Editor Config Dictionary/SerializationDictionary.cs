using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SerializationDictionary : ISerializationCallbackReceiver
{
    [Serializable]
    public class DictionaryElement
    {
        public int key;
        public string value;
    }

    [SerializeField] List<DictionaryElement> dictionaryElements = new List<DictionaryElement>();

    private Dictionary<int, string> dictionary = new Dictionary<int, string>();
   
    public Dictionary<int, string> Dictionary { get => dictionary; }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<int, string>();
        
        var length = dictionaryElements.Count;
       
        for (int i = 0; i < length; i++)
        {
            var element = dictionaryElements[i];
            
            var key = element.key;

            if (dictionary.ContainsKey(key))
            {
                Debug.Log(("key值重复", key));
            }
            else
            {
                var value = element.value;
                dictionary.Add(key, value);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void PrintDictionary()
    {
        foreach (var item in dictionary)
        {
            Debug.Log((item.Key, item.Value));
        }
    }
}