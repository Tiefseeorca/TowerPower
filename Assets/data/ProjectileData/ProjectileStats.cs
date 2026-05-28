using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileStats : ScriptableObject {
	public float Speed;
	public int Damage;
	public float MaxLifetime;
}
