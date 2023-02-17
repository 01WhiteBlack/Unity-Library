using UnityEngine;
public class Test : MonoBehaviour
{
    [SerializeField] SerializationDictionary SerializationDictionary;
    [ContextMenu("打印字典数据")]
    void Print()
    {    
        SerializationDictionary.PrintDictionary();
    }
}
