using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int mapW = 17;
    public int mapH = 9;

    public float xStart = -1050f;
    public float yStart = -435f;

    public float cellWidth = 120f;
    public float cellHeight = 100f;

    public float hRate = 0.13f;
    public int intensity = 10;

    public GameObject prefabStone;
    public GameObject prefabBush;

    private int[,] _objectGrid;
    private List<GameObject> _objectList;
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        _objectGrid = new int[mapW, mapH];
        _objectList = new List<GameObject>();
        for (int j = 0; j < mapH; j++)
            for (int i = 0; i < mapW; i++)
            {
                _objectGrid[i, j] = 0;
                if (i % 2 != 0 && j % 2 != 0)
                {
                    _objectGrid[i, j] = 1;
                }
                else
                {
                    if (Random.Range(0,100) < intensity)
                    {
                        _objectGrid[i, j] = 2;
                    }
                }
                if (_objectGrid[i, j] != 0)
                {
                    GameObject prefab;
                    switch (_objectGrid[i, j])
                    {
                        case 1:
                        default:
                            {
                                prefab = prefabStone;
                                break;
                            }
                        case 2:
                            {
                                prefab = prefabBush;
                                break;
                            }
                    }
                    GameObject gO = Instantiate(prefab, new Vector3(xStart + i * cellWidth + (j * hRate) * cellWidth,yStart + j * cellHeight,0), Quaternion.identity);
                    gO.transform.position = new Vector3(gO.transform.position.x, gO.transform.position.y, gO.transform.position.y * 0.01f);
                    _objectList.Add(gO);
                }
            }
    }
}
