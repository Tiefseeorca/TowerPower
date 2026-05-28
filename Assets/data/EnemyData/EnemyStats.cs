using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject {
    public float Speed;
    public int MaxHp;
    public int Strength;
    public int Reward;
}
