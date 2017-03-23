using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	[SerializeField]
    private TilesManager tilesManager;

    private Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3> ();
    private Queue<Vector3> frontier = new Queue<Vector3> ();
    private List<Vector3> basePositions = new List<Vector3> ();
    private Vector3 goalPosition = new Vector3();

    void Awake () {
        basePositions = tilesManager.GetBasePositions ();
    }

    void Start () {
        Queue<Vector3> a =  CalculatePath (new Vector3 (32f, 11f, 0f));
    }


    public Queue<Vector3> CalculatePath (Vector3 startingPosition) {
        FindPath (startingPosition);
        return CreatePath (startingPosition);
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

    }

    public Queue<Vector3> CreatePath (Vector3 startingPosition) {
        Vector3 currentPosition = goalPosition;
        Queue<Vector3> path = new Queue<Vector3> ();
        path.Enqueue (currentPosition);
        while (currentPosition != startingPosition) {
            currentPosition = cameFrom[currentPosition];
            path.Enqueue (currentPosition);
        }

        while (path.Count > 0) {
            print (path.Dequeue ());
        }
        return path;
    }



}
