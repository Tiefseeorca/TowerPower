using System;
using System.Net;
using UnityEngine;

public class Tower : MonoBehaviour {
    // TODO: detect enemies
    // TODO: turn towards enemy
    // TODO: shoot
    // TODO: priority
    // TODO: stats like strength, fire rate, range etc.

    public float Range;
    public float FireRate;
    // SerializeField: makes it so you can edit this value in Unity during runtime despite being private
    [SerializeField] private float _attackCooldown;
    public GameObject Projectile;

    public enum TargetingOption {
        First, Last, Strongest, Weakest, LowestHp
    }

    public TargetingOption Targeting;
    public bool StayLockedOnTarget = false;
    public Enemy ChosenTarget;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (_attackCooldown > 0) {
            _attackCooldown -= Time.deltaTime;
        }
        if (_attackCooldown <= 0) {
            Enemy target;
            if (StayLockedOnTarget && _isTargetInRange(ChosenTarget)) {
                target = ChosenTarget;
            }
            else {
                target = _selectEnemy();
            }

            if (target) {
                _turnTowardsEnemy(target);
                _shoot(target);
            }
            else {
                _attackCooldown = 0.1f;
            }
        }
    }

    private bool _isTargetInRange(Enemy target) {
        Collider c = target.GetComponent<Collider>();
        return (Vector3.Distance(this.transform.position, c.transform.position) <= Range);
    }

    private Collider[] _detectEnemies() {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Enemies"));
        return collidersInRange;
    }
    
    private Enemy _selectEnemy() {
        Collider[] enemies = _detectEnemies();
        Enemy target = null;
        foreach (Collider collider in enemies) {
            Enemy compOfTarget = collider.GetComponent<Enemy>();
            if (compOfTarget) {
                if (compOfTarget.isPreferable(target, Targeting)) {
                    target = compOfTarget;
                }
            }
        }
        return target;
    }

    // returns true if the tower finished turning towards the enemy
    private bool _turnTowardsEnemy(Enemy target) {
        transform.LookAt(target.transform.position, Vector3.up);
        return true;
    }

    private void _shoot(Enemy target) {
        GameObject newProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        Vector3 projectilePos = newProjectile.transform.position;
        projectilePos.y = target.transform.position.y;
        newProjectile.transform.position = projectilePos;
        newProjectile.transform.LookAt(target.transform.position);
        // Not allowed to assign linear velocity to a kinematic rigidbody
        /* Rigidbody projectileRB = newProjectile.GetComponent<Rigidbody>();
         if (projectileRB) {
            projectileRB.linearVelocity = newProjectile.transform.forward * 5;
        }*/
        _attackCooldown = 1 / Mathf.Max(FireRate, 0.001f);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
