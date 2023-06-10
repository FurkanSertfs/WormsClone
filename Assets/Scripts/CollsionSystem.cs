using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionSystem : MonoBehaviour
{

    [SerializeField]
    int mapSize;
    int[][] map;
    private void Start() {
       
        map = new int[mapSize][];
        for(int i = 0; i < mapSize; i++)
        {

            map[i] = new int[mapSize];
        }
    }
    
}
