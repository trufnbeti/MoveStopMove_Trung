using System.Collections.Generic;
using UnityEngine;

public static class Cache <T>
{
    private static Dictionary<Collider, T> _dict = new Dictionary<Collider, T>();
    public static T GetComponent(Collider collider)
    {
        if (!_dict.ContainsKey(collider)) {
            _dict.Add(collider, collider.GetComponent<T>());
        }

        return _dict[collider];
    }
        
}