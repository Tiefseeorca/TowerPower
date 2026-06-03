using System;
using UnityEngine;

[Serializable]
public struct EnemyBatch {
    public GameObject EnemyType;
    public int Amount;
    public float DelayPerEnemy;
    public float EndDelay;

    public void DecreaseAmount() {
        Amount--;
    }

    public bool IsEmpty() {
        return Amount == 0;
    }
}
