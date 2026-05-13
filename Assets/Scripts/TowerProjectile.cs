using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float Speed;

    public int Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision enemyCol) {
        Debug.Log("Projectile has collided");
        Enemy enemy = enemyCol.gameObject.GetComponent<Enemy>();
        if (!enemy) return;
        enemy.GetHit(Damage);
        OnHit();
    }

    private void OnHit() {
        _onDespawn();
    }
    
    private void _onDespawn() {
        Destroy(gameObject);
    }
}
