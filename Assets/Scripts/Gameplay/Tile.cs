using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Tile : MonoBehaviour {

    public GameObject instance { get; private set; }

    [SerializeField]
    protected bool isPassable;
    [SerializeField]
    protected bool isBuildable;
    [SerializeField]
    protected GameObject prefab;
    [SerializeField]
    protected int movementCost = 1;


    protected Vector3 position;
    

    protected void Awake () {
        instance = this.gameObject;
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

    public void SetMovementCost (float movementCostModifier) {
        movementCost = Mathf.FloorToInt (movementCostModifier * movementCost);
    }

}
