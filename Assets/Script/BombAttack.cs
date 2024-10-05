using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Script
{
    public class BombAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bomb;
        [SerializeField] private float attackPower;
        [SerializeField] private int healthPoint;
        [SerializeField] private float bossYPosPoint;

        public  void ExecuteAttack(GameObject boss, GameObject player)
        {
            for (int x = 0; x < 4; x++)
            {
                var randomRotation = Random.Range(-90.0f, 90.0f);
                Instantiate(bomb, boss.transform.position, quaternion.Euler(0.0f, 0.0f, randomRotation),
                    GameObject.Find("BombParent").transform);
            }
        }

        public  float CalculateEffectiveness(GameObject boss, GameObject player)
        {
            var bossHealth = boss.GetComponent<BossEnemy>().health;
            var position = boss.transform.position;
            var bossPos = (Vector2) position;
            var position1 = player.transform.position;
            var playerpos = (Vector2) position1;
            var distance = (position - position1).magnitude;

            DistanceRate(distance);
            PlayerposRate(playerpos);
            BossPosRate(bossPos);
            BossHealthCheck(bossHealth);

            var result = DistanceRate(distance) + PlayerposRate(playerpos) + BossPosRate(bossPos) +
                         BossHealthCheck(bossHealth);
            Debug.Log(result);
            return result;
        }

        public  float DistanceRate(float distance)
        {
            return 0;
        }

        public  float PlayerposRate(Vector2 player)
        {
            if (player.y < 0)
            {
                var c = -1 * (player.y / 5);
                return c;
            }

            return 0;
        }

        public  float BossPosRate(Vector2 boss)
        {
            if (boss.y > bossYPosPoint)
            {
                var c = boss.y / 3;
                return c;
            }

            return 0;
        }

        public  float BossHealthCheck(float bossHeallth)
        {
            if (bossHeallth < healthPoint)
            {
                return 1;
            }

            return 0;
        }
    }
}