using System;
using UnityEngine;

namespace Script
{
    public class Lazer : MonoBehaviour
    {
        // public bool _turn;
        [SerializeField] private float timer;
        [SerializeField] private float speed;

        private void Start()
        {
            timer = 1;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                turn();
                this.GetComponent<SpriteRenderer>().color = Color.yellow;
                this.GetComponent<CapsuleCollider2D>().enabled = true;
                Destroy(gameObject, 3f);
            }
        }

        private void turn()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed);
        }
    }
}