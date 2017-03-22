using System;

[Serializable]
public struct Speed {
    public bool isMoving;
    public float baseSpeed;
}

[Serializable]
public struct Health {
    public int current;
    public int max;
    public bool isInvulnerable;
}