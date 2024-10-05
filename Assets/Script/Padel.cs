using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace Script
{
    public class Padel : MonoBehaviour
    {
        [SerializeField] private int speed = 4;
        public Vector3 target;
        public Vector3 startPoint;
        public bool ReachedTarget;
        public bool start;
        private bool stop;
        private SplineContainer _spline;
        private float t;
        private float splineLength;
        private float _offsetY = 0;
        private float timer;
        private float dir;
        public bool setOnPosition;
        public int HP;

        void Update()
        {
            if (!start)
            {
                if (!stop)
                {
                    Vector2 _Direction = target - transform.position;
                    transform.Translate(_Direction.normalized * (speed * Time.deltaTime), Space.World);
                    if (Vector3.Distance(target, transform.position) < 0.02f)
                    {
                        stop = true;
                        speed = 1;
                        setOnPosition = true;
                    }
                }

                // timer += Time.deltaTime;
                // if (timer > 3.5f) start = true;
            }

            if (start)
            {
                t += dir * ((speed - 2) * Time.deltaTime / splineLength);
                if (t < 1)
                {
                    Vector3 pos = _spline.EvaluatePosition(t);
                    var transform1 = transform;

                    var transformPosition = transform1.position;
                    transformPosition = pos;
                    transformPosition.y -= _offsetY;
                    transform1.position = transformPosition;
                    Vector3 up = _spline.EvaluateTangent(t);
                    transform.up = up;
                    transform.rotation *= Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    t = 0;
                }

                if (t < 0)
                {
                    t = 1;
                }
            }
        }

        public void SetSpline(SplineContainer spline, float t, float offset, float dir)
        {
            this.dir = dir;
            _offsetY = offset;
            _spline = spline;
            splineLength = spline.CalculateLength();
            this.t = t;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP == 0)
            {
                Spawner.Instance.padels.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}