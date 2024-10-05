using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [Serializable]
    public class SubWave
    {
        // public float delay;
        // public int cycle;
        public int enemyIndex;
        public float interval;
        public Vector3 GridPattern;
    }


    [Serializable]
    public class WaveData
    {
        public bool UseCustomPattern;
        public pattern customPattern;
        public int PadelLines;
        public MyEnum MyEnum;
        public float waveDelay;
        public SubWave[] subWave;
    }


    public enum MyEnum
    {
        Up,
        Down,
        Left,
        Right
    }


    // public static class gridUtil
    // {
    //     public static int getValue(this GridPosition value)
    //     {
    //         switch (value)
    //         {
    //             case GridPosition.none:
    //                 return 4;
    //             case GridPosition.ButtomLeft:
    //                 return 0;
    //             case GridPosition.ButtomCenter:
    //                 return 1;
    //             case GridPosition.ButtomRight:
    //                 return 2;
    //             case GridPosition.MiddelLeft:
    //                 return 3;
    //             case GridPosition.MiddelCenter:
    //                 return 4;
    //             case GridPosition.MiddelRight:
    //                 return 5;
    //             case GridPosition.TopLeft:
    //                 return 6;
    //             case GridPosition.TopCenter:
    //                 return 7;
    //             case GridPosition.TopRight:
    //                 return 8;
    //             default:
    //                 return 4;
    //         }
    //     }
    // }
}