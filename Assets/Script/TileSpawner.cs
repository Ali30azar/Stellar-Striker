using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    [ExecuteInEditMode]
    public class TileSpawner : MonoBehaviour
    {
        public GameObject prefabToInstantiate;
        public pattern pattern;
        // [SerializeField] public string PatternName;

        public List<GameObject>
            instantiatedTiles = new List<GameObject>();


        public void RegisterInstantiatedObject(GameObject obj)
        {
            instantiatedTiles.Add(obj);
        }

        public void UnregisterInstantiatedObject(GameObject obj)
        {
            instantiatedTiles.Remove(obj);
        }
    }
}