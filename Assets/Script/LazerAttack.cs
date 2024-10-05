using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Script
{
    public class LazerAttack : Attack
    {
        [SerializeField] private GameObject lazer;
        [SerializeField] private float attackPower;
        [SerializeField] private int healthPoint;
        [SerializeField] private float bossXPosPoint;
        [SerializeField] private float bossYPosPoint;

        public  void ExecuteAttack(GameObject boss, GameObject player)
        {
            GameObject[] lazers = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                lazers[i] = Instantiate(lazer, boss.transform.position, quaternion.identity,
                    GameObject.Find("BombParent").transform);
                lazers[i].transform.eulerAngles = new Vector3(0.0f, 0.0f, i * 45);
            }

            foreach (var _lazer in lazers)
            {
                // _lazer.GetComponent<Lazer>()._turn = true;
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
            return 0;
        }

        public  float BossPosRate(Vector2 boss)
        {
            if (Mathf.Abs(boss.x) < bossXPosPoint && Mathf.Abs(boss.y) < bossYPosPoint)
            {
                return 1;
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