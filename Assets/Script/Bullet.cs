using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class Bullet : MonoBehaviour
    {
        private bool Stop;
        [SerializeField] private int Direction;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int Damage;
        public Rigidbody2D _rigidbody2D;

        [SerializeField] private string Tag;
        // private float damage;

        private void Start()
        {
            _rigidbody2D.velocity = Direction * transform.up * moveSpeed;

            Destroy(gameObject, 2f);
        }

        private RaycastHit2D[] hits = new RaycastHit2D[10];

        void FixedUpdate()
        {
            //var nextframepos = _rigidbody2D.position + (_rigidbody2D.velocity * Time.fixedDeltaTime);


            //   Debug.DrawRay(_rigidbody2D.position, _rigidbody2D.velocity * Time.fixedDeltaTime, Color.red, 1);


            var filter = new ContactFilter2D();
            filter.useTriggers = true;

            var count = Physics2D.Raycast(_rigidbody2D.position, _rigidbody2D.velocity, filter, hits,
                _rigidbody2D.velocity.magnitude * Time.fixedDeltaTime);

            var o = hits[0].collider;
            if (o != null)
            {
              
                if (count > 0)
                {
                    if (o.CompareTag(Tag))
                        return;
                    if (o.GetComponent<BossEnemy>() != null)
                    {
                        o.GetComponent<BossEnemy>().TakeDamage(1);
                        Destroy(gameObject);
                    }   
                    if (o.GetComponent<Padel>() != null)
                    {
                        o.GetComponent<Padel>().TakeDamage(1);
                        Destroy(gameObject);
                    }

                    if (o.GetComponent<Enemy>() != null)
                    {
                        o.GetComponent<Enemy>().TakeDamage(10);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}