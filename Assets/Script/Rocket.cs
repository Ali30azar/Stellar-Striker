using System;
using UnityEngine;

namespace Script
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] public float speed;
        [SerializeField] private GameObject bulletParent;
        [SerializeField] private GameObject[] Bullet;
        [SerializeField] private Transform[] _Transform;
        [SerializeField] public float ShootRate;
        [SerializeField] private Transform[] screanBunds;
        [SerializeField] public float health;
        private readonly int RIdel = Animator.StringToHash(("Idel"));
        private readonly int RMove = Animator.StringToHash(("MoveRocket"));
        private bool Idel = true;
        private float timer;


        void Update()
        {
            Idel = true;
            HandleMovement();
            if (Idel)
            {
                _animator.Play(RIdel);
            }

            Shoot();
        }

        private void HandleMovement()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetMouseButton(0))
            {
                Idel = false;
                _animator.Play(RMove);
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                var Dir = mousePosition - transform.position;
                if (Dir.sqrMagnitude >= 0.0625f)
                {
                    direction = Dir.normalized;
                }
            }
            else
            {
                float moveVertical = 0f;
                float moveHorizontal = 0f;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Idel = false;
                    _animator.Play(RMove);
                    moveVertical = 1f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    Idel = false;
                    _animator.Play(RMove);
                    moveVertical = -1f;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Idel = false;
                    _animator.Play(RMove);
                    moveHorizontal = -1f;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Idel = false;
                    _animator.Play(RMove);
                    moveHorizontal = 1f;
                }

                direction = new Vector3(moveHorizontal, moveVertical, 0f).normalized;
            }

            if (direction != Vector3.zero)
            {
                MoveCharacter(direction);
            }
        }

        private void MoveCharacter(Vector3 direction)
        {
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

            Vector3 screenPosition = Camera.main.WorldToViewportPoint(newPosition); //? search
            screenPosition.x = Mathf.Clamp01(screenPosition.x);
            screenPosition.y = Mathf.Clamp01(screenPosition.y);
            Vector3 clampedPosition = Camera.main.ViewportToWorldPoint(screenPosition);

            transform.position = clampedPosition;
        }

        private void Shoot()
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse1))
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    for (int i = 0; i < Bullet.Length; i++)
                    {
                        Instantiate(Bullet[i], _Transform[i].position, Quaternion.identity, bulletParent.transform);
                        timer = ShootRate;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                timer = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 3) return;
            if (other.gameObject.CompareTag("Enemy"))
                Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }
}