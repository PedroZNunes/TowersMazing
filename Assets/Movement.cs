using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    static public void Move (Actor actor, Vector3 direction) {
        actor.transform.position += direction * actor.speed.baseSpeed / Time.deltaTime;
    }
}
