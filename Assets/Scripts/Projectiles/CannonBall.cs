using UnityEngine;

public class CannonBall : TowerProjectile {
    public GameObject Explosion;
    protected override void OnHit(Enemy enemy) {
        base.OnHit(enemy);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
