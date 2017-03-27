using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

    [SerializeField]
    protected float baseSpeed = 1;
    protected float speedMod = 1;


    public IEnumerator MoveAlongPath ( Queue<Vector3> path ) {
        while (path.Count > 0) {
            Vector3 destination = path.Dequeue ();

            while (this.transform.position != destination) {
                transform.position = Vector3.MoveTowards (this.transform.position , destination , baseSpeed * speedMod * Time.deltaTime);
                yield return null;
            }

        }
    }

    public void SetSpeedModifier(float speedModifier ) {
        speedMod = speedModifier;
    }

    public float GetCurrentSpeed () {
        return baseSpeed * speedMod;
    }

}
