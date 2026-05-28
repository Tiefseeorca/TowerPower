using System;
using UnityEngine;

public class TowerSocket : MonoBehaviour {
    public GameObject PlacedTower;
    private bool _blocked = false;
    
    public bool CanPlace() {
        return !PlacedTower && !_blocked;
    }

    public void Place(GameObject tower) {
        PlacedTower = tower;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = PlacedTower ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.8f);
    }
}
