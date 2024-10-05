using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Wave wave;
        public GameObject tile; // The prefab to be instantiated.
        [SerializeField] public int rows = 4; // Number of rows in the grid.
        [SerializeField] public int columns = 4; // Number of columns in the grid.
        [SerializeField] public float spacing; // The spacing between grid elements.
        [SerializeField] public List<GameObject> Grids;
        [SerializeField] public List<GameObject> CustomPattern;
        public GameObject[,][] chunkedGrid;
        private WaveData[] WaveData;

        private void Start()
        {
            WaveData = new WaveData[wave.waves.Length];

            for (int waveIndex = 0; waveIndex < wave.waves.Length; waveIndex++)
            {
                WaveData[waveIndex] = wave.waves[waveIndex];
            }
        }

        public void GenerateGrid(int col, int row, float space)
        {
            spacing = space;
            columns = col;
            rows = row;
            float totalGridWidth = (columns - 1) * spacing;
            float totalGridHeight = (rows - 1) * spacing;

            Vector2 gridCenterOffset = new Vector2(totalGridWidth / 2, totalGridHeight / 2 - 1f);

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Vector2 position = new Vector2(x * spacing, y * spacing) - gridCenterOffset;
                    GameObject _tile = Instantiate(tile, position, Quaternion.identity, transform);
                    _tile.transform.Rotate(Vector3.forward, 180f);
                    _tile.GetComponent<Tile>().turn = true;
                    Grids.Add(_tile);
                    
                }
            }
        }

        public void GenerateCustomPattern(int waveIndex)
        {
            for (int i = 0; i < WaveData[waveIndex].customPattern.Pattern.Count; i++)
            {
                
                GameObject _tile = Instantiate(tile, WaveData[waveIndex].customPattern.Pattern[i].position, Quaternion.identity, transform);
                _tile.transform.rotation = WaveData[waveIndex].customPattern.Pattern[i].rotate;
                _tile.transform.position = WaveData[waveIndex].customPattern.Pattern[i].position;
                _tile.transform.Rotate(Vector3.forward, 180f);
                CustomPattern.Add(_tile);
            }
        }


        public void CreateChunks()
        {
            int chunkRows = rows / 2;
            int chunkColumns = columns / 2;
            chunkedGrid = new GameObject[chunkRows, chunkColumns][];

            for (int chunkX = 0; chunkX < chunkColumns; chunkX++)
            {
                for (int chunkY = 0; chunkY < chunkRows; chunkY++)
                {
                    GameObject[] chunk = new GameObject[4];

                    int baseIndex = (chunkX * 2 * rows) + (chunkY * 2);

                    // Populate the chunk with the 4 grid elements
                    chunk[0] = Grids[baseIndex];
                    chunk[1] = Grids[baseIndex + 1];
                    chunk[2] = Grids[baseIndex + rows];
                    chunk[3] = Grids[baseIndex + rows + 1];

                    // Assign the chunk to the chunkedGrid array
                    chunkedGrid[chunkY, chunkX] = chunk;

                    // Optional: Color the chunk to visualize it
                    Color chunkColor = new Color(Random.value, Random.value, Random.value);
                    foreach (var tile in chunk)
                    {
                        tile.GetComponent<SpriteRenderer>().color = chunkColor;
                    }
                }
            }
        }
    }
}