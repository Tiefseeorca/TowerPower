using UnityEngine;

public class Arrow : TowerProjectile {
    public int Pierce = 1;

    protected override void OnHit(Enemy enemy) {
        enemy.GetHit(Stats.Damage);
        if (--Pierce <= 0) {
            _onDespawn();
        }
    }
}
