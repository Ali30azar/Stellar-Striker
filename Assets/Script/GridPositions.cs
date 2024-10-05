using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class GridPositions : MonoBehaviour
    {
        public static GridPositions Instance { get; private set; }
        [SerializeField] public List<Tile> TileList;
        [SerializeField] private GameObject tile;
        public Camera mainCamera;
        [SerializeField] public int tileNum;
        [SerializeField] public Tile[][] gridTile;
        private Tile _tile;
        public int arrived;
        
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
            Vector2 leftScreenEdge = new Vector2(0, Screen.height / 2);
            Vector2 rightScreenEdge = new Vector2(Screen.width, Screen.height / 2);
            Vector2 topScreenEdge = new Vector2(0, Screen.height);

            Vector2 topWorldEdge = mainCamera.ScreenToWorldPoint(topScreenEdge);
            Vector2 leftWorldEdge = mainCamera.ScreenToWorldPoint(leftScreenEdge);
            Vector2 rightWorldEdge = mainCamera.ScreenToWorldPoint(rightScreenEdge);

            float distance = rightWorldEdge.x - leftWorldEdge.x;

            int numberOfPoints = Mathf.FloorToInt(distance) + 1;

            float distance1 = topWorldEdge.y - transform.position.y;

            int numberOfPoints1 = Mathf.FloorToInt(distance1) + 1;
            // Debug.Log("Number of integer points between the left and right edges: " + numberOfPoints);
            TileList.Clear();

            // Instantiate tiles and add their transforms to GridList
            // for (float j = 0; j < numberOfPoints1 ; j += 1.5f)
            // {
            //     for (int i = 0; i < numberOfPoints; i += 3)
            //     {
            //         Vector3 position = new Vector3(transform.position.x + i + 1.5f, transform.position.y + j, 0);
            //         GameObject newTile = Instantiate(tile, position, Quaternion.identity, transform);
            //         _tile = newTile.AddComponent<Tile>();
            //         _tile.tileNum = tileNum;
            //         // _tile.setPos(newTile.transform);
            //         TileList.Add(_tile);
            //         tileNum++;
            //     }
            // }
                                                        
     
        }

        public void Pattern()
        {
            
            
            
            
        }
    }
}