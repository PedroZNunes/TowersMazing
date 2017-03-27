using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour {

    [SerializeField]
    private GameObject slowCreepPrefab;

    [SerializeField]
    private GameObject slowTilePrefab;

    public float speedModifier = 0.25f;

	// Use this for initialization
	void OnEnable () {
        Tile parentTile = GetComponentInParent<Tile> ();
        Movement parentMovement = GetComponentInParent<Movement> ();

        if (parentTile != null) {
            parentTile.SetMovementCost (speedModifier);
        } else if (parentMovement != null) {
            parentMovement.SetSpeedModifier (speedModifier);
        }
	}
	

    private void OnTriggerEnter2D ( Collider2D col ) {
        if (col.GetComponent<Actor>() != null) {
            AttachToActor (col.gameObject);
        }
    }

    private void AttachToActor ( GameObject actor ) {
        Instantiate (slowCreepPrefab , actor.transform , false);
    }

}
