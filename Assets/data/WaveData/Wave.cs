using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject {
    public List<EnemyBatch> Batches;
    public int Reward;

    public GameObject GetNextEnemy() {
        EnemyBatch batch = Batches[0];
        batch.Amount--;
        Batches[0] = batch;
        return Batches[0].EnemyType;
    }

    public float GetNextDelay() {
        if (Batches[0].IsEmpty()) {
            float delay = Batches[0].EndDelay;
            Batches.RemoveAt(0);
            return delay;
        }
        return Batches[0].DelayPerEnemy;
    }

    public bool IsFinished() {
        return Batches.Count == 0;
    }
}
