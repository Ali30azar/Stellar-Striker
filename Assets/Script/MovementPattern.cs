using System;
using DG.Tweening;
using UnityEngine;

namespace Script
{
    public class MovementPattern : MonoBehaviour
    {
        public static MovementPattern Instance { get; private set; }
        public GridGenerator gridGenerator;
        public bool turn;
        public bool turnTile;
        [SerializeField] public float timer;
        private bool stopChangingPos;
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease moveEaseType;

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

        void Start()
        {
            InitializePositionArrays();
        }

        void Update()
        {
            if (turn)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    turnTile = true;
                }
            }
        }

        public void InitializePositionArrays()
        {
            int chunkRows = gridGenerator.rows / 2;
            int chunkColumns = gridGenerator.columns / 2;
            
            for (int chunkX = 0; chunkX < chunkColumns; chunkX++)
            {
                for (int chunkY = 0; chunkY < chunkRows; chunkY++)
                {
                    GameObject[] chunk = gridGenerator.chunkedGrid[chunkY, chunkX];
                    Vector3 bottomLeft = chunk[0].transform.position;
                    Vector3 topLeft = chunk[1].transform.position;
                    Vector3 bottomRight = chunk[2].transform.position;
                    Vector3 topRight = chunk[3].transform.position;
                    // chunk[0].GetComponent<SpriteRenderer>().color= Color.yellow;
                    // chunk[1].GetComponent<SpriteRenderer>().color= Color.red;
                    // chunk[2].GetComponent<SpriteRenderer>().color= Color.black;
                    // chunk[3].GetComponent<SpriteRenderer>().color= Color.green;
                    foreach (var tile in chunk)
                    {
                        Tile tileScript = tile.GetComponent<Tile>();
                        tileScript.InitializeTargetPoints(bottomLeft, topLeft, bottomRight, topRight);
                        tileScript.turn = true; // Enable movement for each tile
                        tileScript.moveEaseType = moveEaseType;
                        tileScript.moveDuration = moveDuration;
                    }
                }
            }
        }
    }
}