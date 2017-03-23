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

    private List<Vector3> gridPositions = new List<Vector3> ();
    private Dictionary<Vector3, Tile> grid = new Dictionary<Vector3, Tile> ();

    private void Start () {
        InitizalizeGrid ();
        MapSetup ();
    }

    private void InitizalizeGrid () {

        gridPositions.Clear ();
        //gridPosition gets only the cells that are not at the edge of the screen
        for (int x = 0 ; x <= COLUMNS ; x++) {
            for (int y = 0 ; y <= ROWS ; y++) {
                gridPositions.Add (new Vector3 (x, y, 0f));
            }
        }
    }

    private void MapSetup () {
        tilesHolder = new GameObject (holderName).transform;
        FillGrid ();
    }

    private void FillGrid () {
        for (int gridIndex = 0 ; gridIndex < gridPositions.Count ; gridIndex++) { //loop through the grid
            GameObject toInstantiate = floorTile;

            if (gridPositions[gridIndex].x == 0 || gridPositions[gridIndex].x == COLUMNS || gridPositions[gridIndex].y == 0 || gridPositions[gridIndex].y == ROWS) {
                if (portalPositions.Contains (gridPositions[gridIndex])) {
                    toInstantiate = portalTile;
                }
                else if (basePositions.Contains (gridPositions[gridIndex])) {
                    toInstantiate = baseTile;
                }
                else {
                    toInstantiate = wallTile;
                }
            }

            if (toInstantiate == null) {
                Debug.LogError ("FillGrid function has nothing to instantiate.");
                continue; 
            }
            


            GameObject instance = Instantiate (toInstantiate, gridPositions[gridIndex], Quaternion.identity, tilesHolder) as GameObject;
            Tile tileToDictionary = instance.GetComponent<Tile> ();

            if (tileToDictionary == null) {
                Debug.LogError ("Tile Component not found.");
            }

            grid.Add (gridPositions[gridIndex], tileToDictionary);
        }
    }


    public List<Vector3> GetPortalPositions () {
        return portalPositions;
    }

    public List<Vector3> GetBasePositions () {
        return basePositions;
    }

    public List<Tile> Neighbours (Vector3 mainNode) {
        List<Tile> neighbours = new List<Tile> ();
        if (grid.ContainsKey(new Vector3 (mainNode.x, mainNode.y + 1, 0f)))
            neighbours.Add (grid[new Vector3 (mainNode.x, mainNode.y + 1, 0f)]);
        if (grid.ContainsKey(new Vector3 (mainNode.x + 1, mainNode.y, 0f)))
            neighbours.Add (grid[new Vector3 (mainNode.x + 1, mainNode.y, 0f)]);
        if (grid.ContainsKey (new Vector3 (mainNode.x, mainNode.y - 1, 0f)))
            neighbours.Add (grid[new Vector3 (mainNode.x, mainNode.y - 1, 0f)]);
        if (grid.ContainsKey (new Vector3 (mainNode.x - 1, mainNode.y, 0f)))
            neighbours.Add (grid[new Vector3 (mainNode.x - 1, mainNode.y, 0f)]);
        return neighbours;
    }
}
