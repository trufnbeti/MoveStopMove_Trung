using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin<T> : ScriptableObject
{
    [SerializeField] private List<T> prefabs = new List<T>();
        
    public T GetPrefab(int index) {
        return prefabs[index];
    }
}
