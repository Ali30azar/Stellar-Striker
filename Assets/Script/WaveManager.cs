using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Script
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance { get; private set; }

        private WaveData[] waveData;

        public GridGenerator gridGenerator;

        // private bool _stopWave;
        [SerializeField] private Wave wave;
        private float[] waveDelay;
        private bool endLevel;
        private bool start;
        private int[][] actionCycle;
        private float[][] subWaveInterval;
        private float[][] subWaveDelay;
        public Transform[] _transform;
        private bool waveIsRunning;
        private bool CheckEndSpawn;
        private int WaveIndex;
        private int enemyNumber;
        private bool custom;
        private bool initialazePattern = true;
        private bool spawnboss;

        private int enemyGroup;
        private int item = 9;

        // public MovementPattern MovementPattern;
        private int y, s, x, EnemyNum;

        private int randomPrise;
        private float _offsetY = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }


            // Initialize the waveData array based on the number of waves
            waveData = new WaveData[wave.waves.Length];
            waveDelay = new float [wave.waves.Length];
            actionCycle = new int[wave.waves.Length][];
            subWaveInterval = new float[wave.waves.Length][];
            subWaveDelay = new float[wave.waves.Length][];

            for (int waveIndex = 0; waveIndex < wave.waves.Length; waveIndex++)
            {
                waveData[waveIndex] = wave.waves[waveIndex];
                waveDelay[waveIndex] = wave.waves[waveIndex].waveDelay;

                int subWaveCount = waveData[waveIndex].subWave.Length;
                subWaveDelay[waveIndex] = new float[subWaveCount];
                actionCycle[waveIndex] = new int[subWaveCount];
                subWaveInterval[waveIndex] = new float[subWaveCount];


                // for (int i = 0; i < subWaveCount; i++)
                // {
                //     subWaveDelay[waveIndex][i] = waveData[waveIndex].subWave[i].delay;
                // }
            }


            gridGenerator.GenerateGrid((int) waveData[WaveIndex].subWave[0].GridPattern.y,
                (int) waveData[WaveIndex].subWave[0].GridPattern.x, waveData[WaveIndex].subWave[0].GridPattern.z);

            gridGenerator.CreateChunks();


            spawnboss = wave.spawnBoss;
        }


        private void Update()
        {
            if (!endLevel)
            {
                if (initialazePattern)
                {
                    custom = waveData[WaveIndex].UseCustomPattern;

                    if (custom)
                    {
                        gridGenerator.GenerateCustomPattern(0);
                        EnemyNum = waveData[WaveIndex].customPattern.Pattern.Count;
                        enemyNumber = EnemyNum;
                    }

                    if (!custom)
                    {
                        gridGenerator.GenerateGrid((int) waveData[WaveIndex].subWave[0].GridPattern.y,
                            (int) waveData[WaveIndex].subWave[0].GridPattern.x,
                            waveData[WaveIndex].subWave[0].GridPattern.z);

                        gridGenerator.CreateChunks();


                        EnemyNum = (int) waveData[WaveIndex].subWave[0].GridPattern.x *
                                   (int) waveData[WaveIndex].subWave[0].GridPattern.y;
                        enemyNumber = EnemyNum;
                    }

                    initialazePattern = false;
                }

                if (CheckEndSpawn)
                {
                    AllEnemisDied();
                }

                if (!waveIsRunning)
                {
                    for (int i = 0; i < wave.waves.Length; i++)
                        waveDelay[i] -= Time.deltaTime;

                    subWaveInterval[WaveIndex][0] =
                        subWaveDelay[WaveIndex][0] + waveData[WaveIndex].subWave[0].interval;
                }

                if (waveDelay[WaveIndex] <= 0 && EnemyNum > 0)
                {
                    start = true;
                    waveIsRunning = true;
                }


                if (start)
                {
                    subWaveInterval[WaveIndex][0] -= Time.deltaTime;


                    start = false;
                    if (EnemyNum > 0)
                    {
                        // number of enemis in one cycle
                        if (!custom)
                        {
                            for (y = 0; y < (enemyNumber) / 2; y++)
                            {
                                Spawner.Instance.spanerNum = 1;
                                Tile tile = gridGenerator.Grids[y].GetComponent<Tile>();
                                Spawner.Instance.EnemySpawner(tile);
                                EnemyNum--;
                            }


                            for (x = (enemyNumber) - 1; x >= (enemyNumber / 2); x--)
                            {
                                Spawner.Instance.spanerNum = 16;
                                Tile tile = gridGenerator.Grids[x].GetComponent<Tile>();
                                Spawner.Instance.EnemySpawner(tile);
                                EnemyNum--;
                            }
                        }

                        if (custom)
                        {
                            for (int i = 0; i < enemyNumber; i++)
                            {
                                Spawner.Instance.spanerNum = 10;
                                Tile tile = gridGenerator.CustomPattern[i].GetComponent<Tile>();
                                Spawner.Instance.EnemySpawner(tile);
                                EnemyNum--;
                            }
                        }


                        actionCycle[WaveIndex][0]++;
                        subWaveDelay[WaveIndex][0] = 0;
                    }


                    for (int i = 0; i < waveData[WaveIndex].PadelLines; i++)
                    {
                        Spawner.Instance.LineSpawner(item, i, waveData[WaveIndex].MyEnum);
                    }

                    if (EnemyNum == 0)
                    {
                        // Spawner.Instance.CheckLastEnemyArrived();
                        if (!custom)
                        {
                            MovementPattern.Instance.turn = true;
                        }

                        CheckEndSpawn = true;
                        // start = false;
                    }


                    // if (enemyAmunt[WaveIndex][waveData[WaveIndex].subWave.Length - 1] ==
                    //     waveData[WaveIndex].subWave[^1].enemyNumber)
                    // {
                    //     if (Spawner.Instance.enemy.Count == 0)
                    //     {
                    //         Debug.Log("All enemis died");

                    //         if (WaveIndex + 1 < wave.waves.Length)
                    //         {
                    //             WaveIndex++;
                    //         }
                    //         else
                    //         {
                    //             endWave = true;
                    //             // LogicManager.Instance.win(WaveIndex + 1);
                    //             print("its here bro" + WaveIndex);
                    //             break;
                    //         }
                    //     }
                    // }
                }
            }
        }


        private void AllEnemisDied()
        {
            if (Spawner.Instance.Enemis.Count == 0)
            {
                if (WaveIndex != waveData.Length - 1)
                {
                    WaveIndex++;
                    CheckEndSpawn = false;
                    waveIsRunning = false;
                    waveDelay[WaveIndex] = waveData[WaveIndex].waveDelay;

                    foreach (var grid in gridGenerator.Grids)
                    {
                        Destroy(grid);
                    }

                    foreach (var tile in gridGenerator.CustomPattern)
                    {
                        Destroy(tile);
                    }

                    foreach (var padel in Spawner.Instance.padels)
                    {
                        if (padel != null)
                        {
                            Destroy(padel.gameObject);
                        }
                    }

                    Spawner.Instance.padels.Clear();
                    gridGenerator.CustomPattern.Clear();
                    gridGenerator.Grids.Clear();


                    custom = waveData[WaveIndex].UseCustomPattern;

                    if (custom)
                    {
                        gridGenerator.GenerateCustomPattern(WaveIndex);
                        EnemyNum = waveData[WaveIndex].customPattern.Pattern.Count;
                        enemyNumber = EnemyNum;
                    }

                    if (!custom)
                    {
                        gridGenerator.GenerateGrid((int) waveData[WaveIndex].subWave[0].GridPattern.y,
                            (int) waveData[WaveIndex].subWave[0].GridPattern.x,
                            waveData[WaveIndex].subWave[0].GridPattern.z);

                        gridGenerator.CreateChunks();


                        EnemyNum = (int) waveData[WaveIndex].subWave[0].GridPattern.x *
                                   (int) waveData[WaveIndex].subWave[0].GridPattern.y;
                        enemyNumber = EnemyNum;


                        MovementPattern.Instance.turn = false;

                        MovementPattern.Instance.timer = 3;

                        MovementPattern.Instance.turnTile = false;

                        MovementPattern.Instance.InitializePositionArrays();
                    }

                    Spawner.Instance.Enemis.Clear();
                }
                else if (spawnboss)
                {
                    Spawner.Instance.BossSpawner();
                    spawnboss = false;
                }
            }
        }
    }
}