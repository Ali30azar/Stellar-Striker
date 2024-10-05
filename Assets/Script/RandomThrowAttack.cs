using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class RandomThrowAttack : Attack
    {
        [SerializeField] private GameObject feather;
        [SerializeField] private float attackPower;
        [SerializeField] private int healthPoint;
        [SerializeField] private float distancePoint;

        public  void ExecuteAttack(GameObject boss, GameObject player)
        {
            for (int x = 0; x < 10; x++)
            {
                var randomRotation = Random.Range(-90.0f, 90.0f);
                Instantiate(feather, boss.transform.position, quaternion.Euler(0.0f, 0.0f, randomRotation),
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

        public  float DistanceRate(float distance) => Mathf.Clamp01(distance / distancePoint);

        public  float PlayerposRate(Vector2 player) => 0;

        public  float BossPosRate(Vector2 boss) => 0;

        public  float BossHealthCheck(float bossHeallth) => bossHeallth < healthPoint ? 1 : 0;
    }
}