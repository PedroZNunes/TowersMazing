using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TilesManager : MonoBehaviour {

    const int ROWS = 19;
    const int COLUMNS = 34;

    [Serializable]
    public class Count {
        public int minimum;
        public int maximum;

        public Count (int min, int max) {
            minimum = min;
            maximum = max;
        }
    }
    [SerializeField]
    private Count portalCount = new Count (1, 5);

    [SerializeField]
    private GameObject[] portalTiles;
    [SerializeField]
    private GameObject floorTile;
    [SerializeField]
    private GameObject wallTile;

    private Transform tilesHolder;
    private List<Vector3> gridPositions = new List<Vector3> ();

    private void Start () {
        InitizalizeGrid ();
        MapSetup ();
    }

    private void InitizalizeGrid () {
        gridPositions.Clear ();

        for (int x = 1 ; x < COLUMNS ; x++) {
            for (int y = 1 ; y < ROWS ; y++) {
                gridPositions.Add (new Vector3 (x, y, 0f));

            }
        }
    }

    private void MapSetup () {
        tilesHolder = new GameObject ("Board").transform;
        FillGrid ();
        
    }

    private void FillGrid () {
        for (int x = 0 ; x <= COLUMNS ; x++) {
            for (int y = 0 ; y <= ROWS ; y++) {
                GameObject toInstantiate = floorTile;
                if (x == 0 || x == COLUMNS || y == 0 || y == ROWS) { //build outer walls
                    toInstantiate = wallTile;
                }

                if (toInstantiate == null) {
                    print ("grid toInstantiate gameobject null.");
                    continue;
                }
                GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity, tilesHolder) as GameObject;
   
            }
        }
    }


}
