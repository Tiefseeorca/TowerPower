using System;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {
    public ProjectileStats Stats;

    public float RemainingLifetime;

    void Start() {
        RemainingLifetime = Stats.MaxLifetime;
    }

    // Update is called once per frame
    void Update() {
        _checkRaycast();
        transform.Translate(0, 0, Stats.Speed * Time.deltaTime);
        _lifeTimeHandling();
    }

    private void _lifeTimeHandling() {
        RemainingLifetime -= Time.deltaTime;
        if (RemainingLifetime <= 0) {
            _onLifeTimeOver();
        }
    }

    private void _checkRaycast() {
        float distanceThisFrame = Stats.Speed * Time.deltaTime;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceThisFrame, LayerMask.GetMask("Enemies"));
        if (hasHit) {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            transform.position = hit.collider.transform.position;
            OnHit(enemy);
        }
    }

    protected virtual void OnHit(Enemy enemy) {
        enemy.GetHit(Stats.Damage);
        _onDespawn();
    }

    protected virtual void _onLifeTimeOver() {
        _onDespawn();
    }
    
    protected void _onDespawn() {
        Destroy(gameObject);
    }
}
