using UnityEngine;

namespace Script
{
    public class FindTarget : MonoBehaviour
    {
        [SerializeField] private GameObject[] Fire;
        [SerializeField] private Transform[] _Transform;
        [SerializeField] public float ShootRate;
        [SerializeField] private string Target;
        private float timer;

        void Start()
        {
        }

        void Update()
        {
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(Target))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                for (int i = 0; i < Fire.Length; i++)
                {
                    Instantiate(Fire[i], _Transform[i].position, Quaternion.identity);
                    timer = ShootRate;
                }
            }
        }
    }
}