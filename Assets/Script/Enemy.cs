using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyData data;

        // [SerializeField] public PathCreator PathCreation;
        [SerializeField] private float speed;

        // [SerializeField] private Transform targetPoint;
        [SerializeField] private float joon;
        [SerializeField] private int value;
        public Tile parentTile;
        private bool stop;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform[] Guns;
        private float shootRate = 2;

        private void Awake()
        {
            joon = data.joon;
            speed = data.speed;
            value = data.value;
        }

        void Update()
        {
            shootRate -= Time.deltaTime;
            if (shootRate < 0)
            {
                // Shoot();
                shootRate += 1.5f;
            }
            if (!stop)
            {
                Vector3 _Direction = parentTile.transform.position - transform.position;
                transform.Translate(_Direction.normalized * (speed * Time.deltaTime), Space.World);
                if (Vector3.Distance(parentTile.transform.position, transform.position) < 0.05f)
                {
                    stop = true;
                }
            }
        }

        public void Shoot()
        {
            foreach (var gun in Guns)
            {
                Instantiate(bullet, gun.position, quaternion.identity);
            }
        }

        public void TakeDamage(float damage)
        {
            joon -= damage;
            if (joon <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Spawner.Instance.Enemis.Remove(this);
        }
    }
}