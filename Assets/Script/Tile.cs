using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Splines;

namespace Script
{
    public class Tile : MonoBehaviour
    {
        // [SerializeField] public Transform tilePos;
        public Vector3[] targetPoint;
        private Vector3 direction;
        public bool turn;
        public int i;
        [SerializeField] private float timer;
        [SerializeField] public float moveDuration;
        [SerializeField] public Ease moveEaseType;
        private SplineContainer _spline;
        private float t = 0f;
        private float splineLength;

        private void Awake()
        {
            targetPoint = new Vector3[4];
        }

        private void Update()
        {
            if (turn)
            {
                if (MovementPattern.Instance.turnTile)
                {
                    RotateTile();
                }
            }
        }

        public void InitializeTargetPoints(Vector3 bottomLeft, Vector3 topLeft, Vector3 bottomRight, Vector3 topRight)
        {
            targetPoint[0] = bottomLeft;
            targetPoint[1] = topLeft;
            targetPoint[2] = bottomRight;
            targetPoint[3] = topRight;

            foreach (var pos in targetPoint)
            {
                direction = pos - transform.position;
                if (direction.magnitude > 0.2f)
                {
                    i++;
                }
                else
                    return;
            }
        }

        private void RotateTile()
        {
            if (i < 0 || i > 3)
            {
                i = 0;
                return;
            }

            Vector3[] targetPoints = {targetPoint[1], targetPoint[3], targetPoint[0], targetPoint[2]};
            direction = targetPoints[i];

            var _direction = direction - transform.position;
            transform.DOMove(direction, moveDuration).SetEase(moveEaseType);
            {
                if (_direction.magnitude < 0.02f)
                {
                    int[] nextIndex = {1, 3, 0, 2};
                    i = nextIndex[i];
                }
            }
        }

        public void SetSpline(SplineContainer spline, float t, float line)
        {
            _spline = spline;
            splineLength = spline.CalculateLength();
            this.t = t;

            if (t < 1)
            {
                Vector3 pos = _spline.EvaluatePosition(t);
                var transform1 = transform;

                var transformPosition = transform1.position;
                transformPosition = pos;
                transformPosition.y -=  line;
                transform1.position = transformPosition;
                Vector3 up = _spline.EvaluateTangent(t);
                transform.up = up;
                transform.rotation *= Quaternion.Euler(0, 0, -90);

            }
            else
            {
                t = 0;
            }
        }

        public void destroyTile()
        {
            Destroy(gameObject);
        }
    }
}