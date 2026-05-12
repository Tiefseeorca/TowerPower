using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject EnemyPrefab;
    private float _nextSpawnTime;
    public EnemyPathNode FirstNode;
    public const float SPAWN_COOLDOWN = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        
        if (_nextSpawnTime <= Time.time) {
            SpawnEnemy();
            _nextSpawnTime = Time.time + Spawner.SPAWN_COOLDOWN;
        }
    }

    public void SpawnEnemy() {
        GameObject newSpawn = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        newSpawn.GetComponent<Enemy>().Target = FirstNode;
    }
}
