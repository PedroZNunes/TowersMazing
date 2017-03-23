using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	[SerializeField]
    private TilesManager tilesManager;

    Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3> ();
//    List<Vector3> gridPositions = new List<Vector3> ();

    Queue<Vector3> frontier = new Queue<Vector3> ();

    private List<Vector3> basePositions = new List<Vector3> ();
    private Vector3 goalPosition = new Vector3();

    void Awake () {
        basePositions = tilesManager.GetBasePositions ();
    }

    void Start () {
        FindPath (new Vector3 (34f, 11f, 0f));
    }

    public void FindPath (Vector3 startingPosition) {
        frontier.Clear ();
        cameFrom.Clear ();

        frontier.Enqueue (startingPosition);
        cameFrom[startingPosition] = startingPosition; //this might cause issues

        Vector3 currentPosition = new Vector3 ();
        while (frontier.Count > 0) {
            currentPosition = frontier.Dequeue ();
            if (currentPosition == new Vector3 (1f, 10f, 0f))
                Debug.Break ();
            if (basePositions.Contains (currentPosition)) {
                goalPosition = currentPosition;
                break;
            }

            List<Tile> neighbours = tilesManager.Neighbours (currentPosition);
            foreach (Tile tile in neighbours) {
                if (!cameFrom.ContainsKey (tile.GetPosition()) && tile.IsPassable ()) {
                    frontier.Enqueue (tile.GetPosition ());
                    cameFrom[tile.GetPosition()] = currentPosition;
                }
            }
        }
        CreatePath (startingPosition);
    }

    public void CreatePath (Vector3 startingPosition) {
        Vector3 currentPosition = goalPosition;
        List<Vector3> path = new List<Vector3> ();
        path.Add (currentPosition);
        while (currentPosition != startingPosition) {
            currentPosition = cameFrom[currentPosition];
            path.Add (currentPosition);
        }
        path.Reverse ();

        for (int x = 0 ; x < path.Count ; x++) {
            print (path[x]);
        }
    }



}
