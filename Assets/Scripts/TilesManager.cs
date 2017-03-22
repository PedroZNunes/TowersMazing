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
    private GameObject portalTile;
    [SerializeField]
    private GameObject baseTile;
    [SerializeField]
    private GameObject floorTile;
    [SerializeField]
    private GameObject wallTile;
    [SerializeField]
    private List<Vector3> portalPositions = new List<Vector3> ();
    [SerializeField]
    private List<Vector3> basePositions = new List<Vector3> ();
    [SerializeField]
    private string holderName = "Board";

    private Transform tilesHolder;
    private List<Vector3> innerGridPositions = new List<Vector3> ();
    private List<Vector3> outerGridPositions = new List<Vector3> ();


    private void Start () {
        InitizalizeGrid ();
        MapSetup ();
    }

    private void InitizalizeGrid () {
        innerGridPositions.Clear ();
        outerGridPositions.Clear ();
        //gridPosition gets only the cells that are not at the edge of the screen
        for (int x = 0 ; x <= COLUMNS ; x++) {
            for (int y = 0 ; y <= ROWS ; y++) {
                if (x == 0 || x == COLUMNS || y == 0 || y == ROWS) {
                    outerGridPositions.Add (new Vector3 (x, y, 0f));
                    continue;
                }
                innerGridPositions.Add (new Vector3 (x, y, 0f));
            }
        }
    }

    private void MapSetup () {
        tilesHolder = new GameObject (holderName).transform;
        FillGrid ();
        FillOuterWall ();
    }

    private void FillGrid () {
        for (int gridIndex = 0 ; gridIndex < innerGridPositions.Count ; gridIndex++) { //loop through the grid
            GameObject toInstantiate = floorTile;
            if (toInstantiate == null) {
                Debug.LogError ("FillGrid function has nothing to instantiate.");
                continue;
            }
            GameObject instance = Instantiate (toInstantiate, innerGridPositions[gridIndex], Quaternion.identity, tilesHolder) as GameObject;
        }
    }

    private void FillOuterWall () {
        for (int gridIndex = 0 ; gridIndex < outerGridPositions.Count ; gridIndex++) { //loop through the outerWallgrid
            GameObject toInstantiate = wallTile;
            for (int portalIndex = 0 ; portalIndex < portalPositions.Count ; portalIndex++) { //spawn portals
                if (outerGridPositions[gridIndex] == portalPositions[portalIndex]) {
                    toInstantiate = portalTile;
                    break;
                }
            }
            for (int baseIndex = 0 ; baseIndex < basePositions.Count ; baseIndex++) { //spawn base
                if (outerGridPositions[gridIndex] == basePositions[baseIndex]) {
                    toInstantiate = baseTile;
                    break;
                }
            }
            GameObject instance = Instantiate (toInstantiate, outerGridPositions[gridIndex], Quaternion.identity, tilesHolder) as GameObject;
        }
    }

}
