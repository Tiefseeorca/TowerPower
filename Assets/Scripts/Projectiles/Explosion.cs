using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Range = 1;
    public int Damage = 1;

    public float LifeTime = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Enemies"));
        foreach (Collider col in collidersInRange) {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy) {
                enemy.GetHit(Damage);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}
