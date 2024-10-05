using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

namespace Script
{
    [Serializable]
    public class AttackAnalyzer
    {
        // public float var;

        public Operation[] operation;

        // private void OnValidate()
        // {
        //     GetScore(var);
        // }

        // public float Analyze(float Var)
        // {
        //     foreach (var op in operations)
        //     {
        //         if (op.Calculate(Var) == 0)
        //         {
        //             return 0;
        //         }
        //     }
        //
        //     // Debug.Log("true");
        //     return 1;
        // }

        public float Analyze(Vector2 agent, Vector2 player, Vector3 variable)
        {
            float R = 0;
            foreach (var op in operation)
            {
                var r = op.Calculate(op.OP(agent, player, variable));

                R += r;

                // if (r <= 0)
                // {
                //     Debug.Log("Not Match " + r);
                //     return 0;
                // }
            }

            R /= operation.Length;
            // Debug.Log(operation[0].parametr + "= " + R);
            return R;
        }
    }

    [Serializable]
    public class Operation
    {
        public Parametr parametr;

        public AnimationCurve curve;

        // [Header("deprecated dont use")] 
        // public Operand operation;
        // public float value;


        public float Calculate(float var)
        {
            float k = 0;
            foreach (var key in curve.keys)
            {
                if (key.value > k)
                {
                    k = key.value;
                }
            }

            k = Mathf.Max(1, Mathf.Abs(k));

            return curve.Evaluate(var) / k;

            // switch (operation)
            // {
            //     case Operand.Nope:
            //         return 0;
            //     case Operand.Eq:
            //         if (Math.Abs(var - value) < 0.1f)
            //             return 1;
            //         break;
            //     case Operand.NotEq:
            //         if (Math.Abs(var - value) > 0.1f)
            //             return 1;
            //         break;
            //     case Operand.Greater:
            //         if (var > value)
            //             return 1;
            //         break;
            //     case Operand.Less:
            //         if (var < value)
            //             return 1;
            //         break;
            //     case Operand.GEq:
            //         if (var >= value)
            //             return 1;
            //         break;
            //     case Operand.LEq:
            //         if (var <= value)
            //             return 1;
            //         break;
            //     default:
            //         return 0;
            // }
            //
            // return 0;
        }

        public float OP(Vector2 boss, Vector2 player, Vector3 var)
        {
            switch (parametr)
            {
                case Parametr.None:
                    return 0;
                case Parametr.Boss_x:
                    return boss.x;
                case Parametr.Boss_y:
                    return boss.y;
                case Parametr.Player_x:
                    return player.x;
                case Parametr.Player_y:
                    return player.y;
                case Parametr.Distance:
                    return var.x;
                case Parametr.BossHealth:
                    return var.y;
                case Parametr.PlayerHealth:
                    return var.z;
                default:
                    return 0;
            }
        }


        // private float GetUtility(float variable)
        // {
        //     var result = variable / anyAdad;
        //
        //     return result;
        // }
    }

    public enum Parametr
    {
        None,
        Boss_y,
        Boss_x,
        Player_y,
        Player_x,
        Distance,
        BossHealth,
        PlayerHealth
    }

    public enum Operand
    {
        Nope,
        Eq,
        NotEq,
        Greater,
        Less,
        GEq,
        LEq
    }
}