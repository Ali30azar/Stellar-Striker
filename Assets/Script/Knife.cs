using System;
using UnityEngine;
using DG.Tweening;

namespace Script
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private int speed;
        [SerializeField] private GameObject player;

        private void Start()
        {
            var pr = Transform.FindObjectOfType<Rocket>();
            player = pr.gameObject;
            var transform1 = transform;
            transform1.up = (player.transform.position - transform1.position).normalized;
        }

        void Update()
        {
            Vector2 Direction = transform.up;
            transform.Translate(Direction.normalized * speed * Time.deltaTime, Space.World);
            Destroy(this.gameObject, 2f);
        }
    }
}