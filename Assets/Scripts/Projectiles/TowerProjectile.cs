using System;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {
    public float Speed;
    public int Damage;
    public float RemainingLifetime = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        _checkRaycast();
        transform.Translate(0, 0, Speed * Time.deltaTime);
        _lifeTimeHandling();
    }

    private void _lifeTimeHandling() {
        RemainingLifetime -= Time.deltaTime;
        if (RemainingLifetime <= 0) {
            _onLifeTimeOver();
        }
    }

    private void _checkRaycast() {
        float distanceThisFrame = Speed * Time.deltaTime;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceThisFrame, LayerMask.GetMask("Enemies"));
        if (hasHit) {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            transform.position = hit.collider.transform.position;
            OnHit(enemy);
        }
    }

    /*private void OnCollisionEnter(Collision enemyCol) {
        Enemy enemy = enemyCol.gameObject.GetComponent<Enemy>();
        if (!enemy) return;
        //enemy.GetHit(Damage);
        OnHit(enemy);
    }*/

    protected virtual void OnHit(Enemy enemy) {
        enemy.GetHit(Damage);
        _onDespawn();
    }

    protected virtual void _onLifeTimeOver() {
        _onDespawn();
    }
    
    private void _onDespawn() {
        Destroy(gameObject);
    }
}
