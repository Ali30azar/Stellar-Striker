using System;
using Unity.Mathematics;
using UnityEngine;

namespace Script
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float timer;
        public Rigidbody2D Rigidbody2D;
        [SerializeField] private GameObject fire;
        [SerializeField] private GameObject particle;

        void Start()
        {
            Rigidbody2D.AddForce(transform.up * 5f, ForceMode2D.Impulse);
            var t = Transform.FindObjectOfType<ParticleManager>();
            particle = t.gameObject;
        }

        void Update()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                var p = Instantiate(fire, transform.position, quaternion.identity, particle.transform);
                particle.GetComponent<ParticleManager>().particles.Add(p);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var p = Instantiate(fire, transform.position, quaternion.identity);
                particle.GetComponent<ParticleManager>().particles.Add(p);
                Destroy(gameObject);
            }
        }
    }
}