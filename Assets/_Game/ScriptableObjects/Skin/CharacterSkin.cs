using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSkin<T>
{
    [SerializeField] private List<T> prefabs = new List<T>();
        
    public T GetPrefab(int index) {
        return prefabs[index];
    }
}
