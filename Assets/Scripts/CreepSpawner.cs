using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour {

    public bool isSpawning { get; private set; }

    [SerializeField]
    private float delay = 1f;

    [SerializeField]
    private GameObject creep;
    [SerializeField]
    private string holderName = "Creeps";
    private Transform creepsHolder;
    private WaitForSeconds delayInSeconds;
    private Coroutine spawnCoroutine;
    private List<Vector3> portalPositions;

    [SerializeField]
    private TilesManager tilesManager;

	// Use this for initialization
	void Awake() {
        Debug.Assert (tilesManager != null, "TilesManager not set in inspector");
        delayInSeconds = new WaitForSeconds(delay);
        creepsHolder = new GameObject (holderName).transform;
        portalPositions = tilesManager.GetPortalPositions ();
	}

    void Start () {
        StartSpawning (); //FIX
    }
    	
	// Update is called once per frame
	void Update () {
		
	}

    void StartSpawning () {
        if (spawnCoroutine == null) {
            spawnCoroutine = StartCoroutine (Spawning ());
            isSpawning = true;
        }
    }

    public void StopSpawning () {
        if (spawnCoroutine != null) {
            StopCoroutine (spawnCoroutine);
            isSpawning = false;
        }
    }

    private IEnumerator Spawning () {
        while (true) {
            GameObject toInstantiate = creep;
            Vector3 spawnSpot = portalPositions[Random.Range (0, portalPositions.Count)];
            Instantiate (toInstantiate, spawnSpot, Quaternion.identity, creepsHolder);
            yield return delayInSeconds;
        }
    }
}
