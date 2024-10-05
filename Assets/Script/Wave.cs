using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [CreateAssetMenu]
    public class Wave : ScriptableObject
    {
        // public List<patterns> CustomPatterns;
        public bool spawnBoss;
        public WaveData[] waves;
    }
    
}