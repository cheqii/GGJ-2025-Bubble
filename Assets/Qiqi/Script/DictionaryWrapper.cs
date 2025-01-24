using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class DictionaryWrapper<TKey, TValue> where TValue : Object
{
    public List<ListDictionaryWrapper<TKey, TValue>> dictionary;
}

[Serializable]
public class ListDictionaryWrapper<TKey, TValue> : DictionaryWrapper<TKey, TValue> where TValue : Object
{
    public TKey key;
    public TValue value;
}
