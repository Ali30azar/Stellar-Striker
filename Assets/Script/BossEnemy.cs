using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Script
{
    public class BossEnemy : MonoBehaviour
    {
        [SerializeField] public float health;
        [SerializeField] private int speed;
        [SerializeField] private GameObject tile;
        [SerializeField] private float timer;
        [SerializeField] private float[] sideBound;
        [SerializeField] private int i;
        [SerializeField] private List<GameObject> _tile = new List<GameObject>();
        [SerializeField] private GameObject point;
        [SerializeField] private GameObject player;
        private readonly int[] range = {1, 0};
        private bool stop;
        [SerializeField] public bool move;
        private Vector3 playerPos;
        public List<Attack> attacks = new List<Attack>();


        private void Awake()
        {
            var pr = Transform.FindObjectOfType<Rocket>();
            // print(pr);
            player = pr.gameObject;
        }


        void Start()
        {
            playerPos = player.transform.position;
            sideBound = new float[2];
            sideBound[0] = -5.5f;
            sideBound[1] = 5.5f;
            Vector2 pos = new Vector2(0, 2);
            _tile.Add(Instantiate(tile, pos, Quaternion.identity));
            timer = 3;
        }

        void Update()
        {
            if (move)
            {
                Move();
            }
        }


        private void Move()
        {
            if (stop)
            {
                i = range[i];
                _tile.Add(Instantiate(tile,
                    new Vector2(Random.Range(transform.position.x, sideBound[i]), Random.Range(-1, 2)),
                    Quaternion.identity));

                if (_tile != null)
                {
                    Destroy(_tile[0]);
                    _tile.RemoveAt(0);
                }

                stop = false;
            }

            if (!stop)
            {
                Vector3 _Direction = _tile[0].transform.position - transform.position;
                transform.Translate(_Direction.normalized * (speed * Time.deltaTime), Space.World);
                if (Vector3.Distance(_tile[0].transform.position, transform.position) < 0.1f)
                {
                    stop = true;
                    move = false;
                }
            }
        }

        public void Attack()
        {
            
            int[] e = new int[attacks.Count];
            Attack a = null;
            var max = 0f;

            a = attacks.OrderByDescending(x => x.CalculateEffectiveness(gameObject, player)).FirstOrDefault();

            print(a);
         
            if (a != null) a.ExecuteAttack(gameObject, player);
            a = null;
        }

        public void TakeDamage(int damage)
        {
            if (health <= 0) Destroy(gameObject);
            health -= damage;
        }
    }
}