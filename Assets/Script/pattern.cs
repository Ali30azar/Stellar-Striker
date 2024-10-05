using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [CreateAssetMenu]

    public class pattern: ScriptableObject
    {
        public List<CustomPattern> Pattern;
    }
    
    [Serializable]
    public class CustomPattern
    {
        public CustomPattern(Vector2 position, Quaternion rotation)
        {
            this.position = position;
            this.rotate = rotation;
        }

        public Vector2 position;
        public Quaternion rotate;
    }

}