using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathUpdatedEventargs : EventArgs {
    public Dictionary<Vector3, Vector3>[] cameFrom;
}


public class PathFinder : MonoBehaviour {

    public static bool isGridAvailable { get; private set; }

    [SerializeField]
    private TilesManager tilesManager;

    private List<Vector3> reachableTiles = new List<Vector3> ();
    private Dictionary<Vector3 , Vector3>[] cameFrom;
    private Queue<Vector3> frontier = new Queue<Vector3> ();
    private List<Vector3> basePositions = new List<Vector3> ();

    public static event EventHandler<PathUpdatedEventargs> PathUpdated;

    void Awake () {
        basePositions = tilesManager.GetBasePositions ();
        cameFrom = new Dictionary<Vector3 , Vector3>[basePositions.Count];
    }
    
    void Start () {
        Subscribe ();
        FindPaths ();
    }


    protected virtual void OnGridUpdated () {
        FindPaths ();
        PathUpdatedEventargs e = new PathUpdatedEventargs () { cameFrom = this.cameFrom };
        if (PathUpdated != null) {
            PathUpdated (this , e);
        }
    }


    public Queue<Vector3> CalculatePath ( Vector3 creepPosition ) {

        if (cameFrom[0].Count == 0) {
            FindPaths ();
        }

        Queue<Vector3>[] paths = CreatePaths (creepPosition);
        return ChoosePath (paths);
    }


    public void FindPaths () {
        reachableTiles.Clear ();
        if (cameFrom.Length >= 0) {
            for (int i = 0 ; i < cameFrom.Length ; i++) {
                cameFrom[i] = new Dictionary<Vector3 , Vector3> ();
            }
        }

        isGridAvailable = false;

        for (int baseIndex = 0 ; baseIndex < basePositions.Count ; baseIndex++) {
            frontier.Clear ();
            frontier.Enqueue (basePositions[baseIndex]);
            cameFrom[baseIndex][basePositions[baseIndex]] = basePositions[baseIndex];
            Vector3 currentPosition = new Vector3 ();

            while (frontier.Count > 0) {
                currentPosition = frontier.Dequeue ();
                if (!reachableTiles.Contains (currentPosition))
                    reachableTiles.Add (currentPosition);
                List<Tile> neighbours = tilesManager.Neighbours (currentPosition);

                foreach (Tile tile in neighbours) {
                    if (!cameFrom[baseIndex].ContainsKey (tile.GetPosition ()) && tile.IsPassable ()) {
                        frontier.Enqueue (tile.GetPosition ());
                        cameFrom[baseIndex][tile.GetPosition ()] = currentPosition;
                    }
                }

            }

        }

    isGridAvailable = true;
    }


    public Queue<Vector3> ChoosePath (Queue<Vector3>[] paths) {
        Queue<Vector3> shortestPath = new Queue<Vector3> ();

        for (int i = 0 ; i < paths.Length ; i++) {
            if (shortestPath.Count == 0) {
                shortestPath = paths[i];
            }
            else if (shortestPath.Count > paths[i].Count) {
                Debug.LogFormat ("Chose path[{0}] with Count:{1} instead of previous one with Count:{2}" , i , paths[i].Count , shortestPath.Count);
                shortestPath = paths[i];
            }
        }

        return shortestPath;
    }


    public Queue<Vector3>[] CreatePaths (Vector3 creepPosition) {
        Queue<Vector3>[] paths = new Queue<Vector3>[cameFrom.Length];

        for (int i = 0 ; i < paths.Length ; i++) {
            paths[i] = new Queue<Vector3> ();
        }

        Vector3 currentPosition = new Vector3 ();
        for (int i = 0 ; i < cameFrom.Length ; i++) {
            currentPosition = creepPosition;
            paths[i].Enqueue (currentPosition);
            while (!basePositions.Contains (currentPosition)) {
                currentPosition = cameFrom[i][currentPosition];
                paths[i].Enqueue (currentPosition);
            }
        }

        return paths;
    }


    public List<Vector3> GetPathingGrid () {
        return reachableTiles;
    }


    private void OnDisable () {
        Unsubscribe ();
    }


    private void Subscribe () {
        //TilesManager.GridUpdated+=OnGridUpdated;
    }


    private void Unsubscribe () {
        //TilesManaget.GridUpdated-=OnGridUpdated;
    }

}
