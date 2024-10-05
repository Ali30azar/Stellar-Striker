using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Script
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }
        // public GameObject spawner;

        public GameObject enemy;
        public List<Enemy> Enemis;
        public List<Padel> padels;

        // public List<GameObject> Enemies;
        public Vector2[] SpawnPoint;
        public Camera mainCamera;
        public int spanerNum;
        private Vector3 BLeftWorldEdge;
        private Vector3 BRightWorldEdge;
        private Vector3 TLeftWorldEdge;
        private Vector3 TRightWorldEdge;
        public Transform[] Edge;
        [SerializeField] private int SpNum;
        private int SpawnerCount;
        private int topSpawner;
        private int leftSpaner;
        private int rightSpaner;
        public GameObject Padel;
        public GameObject tile;
        public GameObject boss;
        bool side = false;
        

        [SerializeField] private SplineContainer spline;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            // Get the screen positions for the edges
            Vector2 leftScreenEdge = new Vector2(0, 0);
            Vector2 rightScreenEdge = new Vector2(Screen.width, 0);
            Vector2 topScreenEdge = new Vector2(0, Screen.height);
            Vector2 TRightScreenEdge = new Vector2(Screen.width, Screen.height);

            // Convert screen positions to world positions
            BLeftWorldEdge = mainCamera.ScreenToWorldPoint(leftScreenEdge);
            BRightWorldEdge = mainCamera.ScreenToWorldPoint(rightScreenEdge);
            TLeftWorldEdge = mainCamera.ScreenToWorldPoint(topScreenEdge);
            TRightWorldEdge = mainCamera.ScreenToWorldPoint(TRightScreenEdge);


            topSpawner = (int) (Edge[2].position.x - Edge[1].position.x);
            leftSpaner = (int) (Edge[1].position.y - Edge[0].position.y);
            rightSpaner = (int) (Edge[2].position.y - Edge[3].position.y);
            SpawnerCount = topSpawner + rightSpaner + leftSpaner;

            SpawnPoint = new Vector2[SpawnerCount];
        }

        public void EnemySpawner(Tile parent)
        {
            SpNum = 0; //
            for (int i = 0; i < topSpawner; i++)
            {
                SpawnPoint[SpNum].x = Edge[1].position.x + i;
                SpawnPoint[SpNum].y = Edge[1].position.y;
                if (spanerNum == SpNum)
                {
                    var transform1 = parent.transform;
                    GameObject Enemy = Instantiate(enemy, SpawnPoint[SpNum], transform1.rotation, transform1);
                    Enemy.GetComponent<Enemy>().parentTile = parent;
                    Enemis.Add(Enemy.GetComponent<Enemy>());
                }

                SpNum++;
            }
        }

        public void BossSpawner()
        {
            Instantiate(boss, new Vector3(0, 10, 0), Quaternion.identity);
        }
        
        public void LineSpawner(int number, int lines, MyEnum option)
        {
            var direction = (float) Math.Pow(-1, lines);
            for (int i = 0; i < number; i++)
            {
                Vector2 transformPosition;
                GameObject padel = Instantiate(Padel, SpawnPosition(number, option), Quaternion.identity, spline.transform);
                if (side)
                {
                    transformPosition = padel.transform.position;
                    transformPosition.y = transformPosition.y + i;
                }
                else
                {
                    transformPosition = padel.transform.position;
                    transformPosition.x = transformPosition.x - i;
                }


                padel.transform.position = transformPosition;
                GameObject _tile = Instantiate(tile, transform.position, Quaternion.identity, spline.transform);
                //padel.GetComponent<Padel>().target = endPoint;
                //padel.GetComponent<Padel>().startPoint = new Vector2((direction * number), y);
                padel.GetComponent<Padel>().SetSpline(spline, (i * 2) / spline.CalculateLength(), lines, direction);
                _tile.GetComponent<Tile>().SetSpline(spline, (i * 2) / spline.CalculateLength(), lines);
                padel.GetComponent<Padel>().target = _tile.transform.position;
                Vector3 up = spline.EvaluateTangent((i * 2) / spline.CalculateLength());
                padel.transform.up = up;
                padel.transform.rotation *= Quaternion.Euler(0, 0, -90);
                padels.Add(padel.GetComponent<Padel>());
            }
        }

        private Vector2 SpawnPosition(int number, MyEnum option)
        {
            var n = number / 2;
            Vector2 position;
            switch (option)
            {
                case MyEnum.Up:
                    position = new Vector2(n, TLeftWorldEdge.y);
                    side = false;
                    break;
                case MyEnum.Down:
                    position = new Vector2(n, BLeftWorldEdge.y);
                    side = false;
                    break;
                case MyEnum.Left:
                    position = new Vector2(BLeftWorldEdge.x, n);
                    side = true;
                    break;
                case MyEnum.Right:
                    position = new Vector2(TRightWorldEdge.x, n);
                    side = true;
                    break;
                default:
                    position = Vector2.zero;
                    break;
            }

            return position;
        }
    }
}