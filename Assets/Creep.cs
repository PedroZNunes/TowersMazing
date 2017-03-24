using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Actor {

    public Health health;
    
    [SerializeField]
    private Movement movement;

    private PathFinder pathFinder;
    private Queue<Vector3> path;


    private void Awake () {
        pathFinder = GameObject.FindGameObjectWithTag (MyTags.pathFinder.ToString ()).GetComponent<PathFinder> ();
    }


    private void Start () {
        StartCoroutine (StartMoving ());
    }


    private IEnumerator StartMoving () {
        while (PathFinder.isGridAvailable == false) {
            yield return null;
        }

        path = pathFinder.CalculatePath (transform.position);
        StartCoroutine (movement.MoveAlongPath (path));
    }
}


