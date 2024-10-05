using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    [CreateAssetMenu]
    public class Attack : ScriptableObject
    {
        [SerializeField] private GameObject Weapen;
        [SerializeField] private int Number;
        [SerializeField] private float RotateAngel;
        [SerializeField] private bool RandomAngel;

        // [SerializeField] private float attackPriority;
        [SerializeField] public AttackAnalyzer[] Analyzer;

        public void ExecuteAttack(GameObject boss, GameObject player)
        {
            if (RandomAngel)
                while (MathF.Abs(RotateAngel) < 10)
                {
                    RotateAngel = Random.Range(-90.0f, 90.0f);
                }


            for (int x = 0; x < Number; x++)
            {
                var B = Instantiate(Weapen, boss.transform.position, quaternion.identity,
                    GameObject.Find("BombParent").transform);
                B.transform.eulerAngles = new Vector3(0.0f, 0.0f, x * RotateAngel);
            }
        }

        public float CalculateEffectiveness(GameObject boss, GameObject player)
        {
            var bossHealth = boss.GetComponent<BossEnemy>().health;
            var position = boss.transform.position;
            var playerHealth = player.GetComponent<Rocket>().health;
            var bossPos = (Vector2) position;
            var position1 = player.transform.position;
            var playerpos = (Vector2) position1;
            var distance = (position - position1).magnitude;
            Vector3 var = new Vector3(distance, bossHealth, playerHealth);

            var result = AnalyzeScore(bossPos, playerpos, var);

            // Debug.Log(result);
            return result;
        }

        private float AnalyzeScore(Vector2 boss, Vector2 player, Vector3 variable)
        {
            float R = 0;
            foreach (var A in Analyzer)
            {
                R += A.Analyze(boss, player, variable);
            }

            R /= Analyzer.Length;
            Debug.Log("Result " + R);
            return R;
        }
    }
}