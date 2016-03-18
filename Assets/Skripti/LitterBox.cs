using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LitterBox : ILitterBox
{
    static List<GameObject> CachedObjects = new List<GameObject>(capacity: 2);
    public GameObject Triggered(GameObject element)
    {
        if (CachedObjects.Contains(element))
        {
            CachedObjects.Remove(element);
            return element;
        }
        if (CachedObjects.Count < 2)
        {
            CachedObjects.Add(element);
        }
        else if (CachedObjects.Count == 2)
        {
            GameObject RemovedObject = CachedObjects.First();
            CachedObjects[0] = CachedObjects[1];
            CachedObjects[1] = element;
            return RemovedObject;
        }
        return null;
    }
}
