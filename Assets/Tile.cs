using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Tile : MonoBehaviour {

    [SerializeField]
    protected bool isPassable;
    [SerializeField]
    protected bool isBuildable;
    [SerializeField]
    protected GameObject prefab;

    protected Vector3 position;

    protected void Awake () {
        position = transform.position;
    }

    public bool IsPassable () {
        return isPassable;
    }

    public bool IsBuildable () {
        return isBuildable;
    }

    public GameObject GetPrefab () {
        return prefab;
    }

    public Vector3 GetPosition () {
        return position;
    }

}
