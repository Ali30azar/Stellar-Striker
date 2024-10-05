using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class KnifeAttack : MonoBehaviour
    {
        [SerializeField] private List<GameObject> knifePoint;
        [SerializeField] private GameObject knife;
        [SerializeField] private GameObject point;
        [SerializeField] private float attackPower;
        [SerializeField] private int healthPoint;
        [SerializeField] private float distancePoint;

        private Vector3 playerPos;

        private void Awake()
        {
            var transform1 = transform;
            var position = transform1.position;
            knifePoint.Add(Instantiate(point, position, Quaternion.identity, transform1));
            knifePoint.Add(Instantiate(point, position + new Vector3(0.4f, 0, 0), Quaternion.identity,
                knifePoint[0].transform));
            knifePoint.Add(Instantiate(point, position + new Vector3(-0.4f, 0, 0), Quaternion.identity,
                knifePoint[0].transform));
        }

        public  void ExecuteAttack(GameObject boss, GameObject player)
        {
            var position = boss.transform.position;
            knifePoint[0].transform.position = position;


            playerPos = player.transform.position;


            knifePoint[0].transform.up = (playerPos - knifePoint[0].transform.position).normalized;
            for (int x = 1; x < 3; x++)
            {
                Instantiate(knife, knifePoint[x].transform.position, knifePoint[x].transform.rotation,
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


            var result = DistanceRate(distance) + PlayerposRate(playerpos) + BossPosRate(bossPos) +
                         BossHealthCheck(bossHealth);
            Debug.Log(result);
            return result;
        }


        public  float DistanceRate(float distance)
        {
            // print(distance);
            if (distance > distancePoint)
            {
                return 1f;
            }

            

            var c = distance / distancePoint;
            return c;
        }

        public  float PlayerposRate(Vector2 player)
        {
            return 0;
        }

        public  float BossPosRate(Vector2 boss)
        {
            return 0;
        }

        public  float BossHealthCheck(float bossHeallth)
        {
            if (bossHeallth < healthPoint)
            {
                var c = bossHeallth / 200;
                return c;
            }

            return 0;
        }
    }
}