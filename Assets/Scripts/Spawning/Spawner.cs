using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour {
    public List<Wave> Waves;
    private float _nextSpawnTime;
    public EnemyPathNode FirstNode;
    private bool _isWaveRunning = false;
    public static UnityEvent<int, int> WaveCounterChanged = new();
    private int _waveCounter = -1;

    private void Start() {
        for (int i = 0; i < Waves.Count; i++) {
            Waves[i] = Instantiate(Waves[i]);
        }
        WaveCounterChanged.Invoke(0, Waves.Count);
    }

    private void Update() {
        if (_isWaveRunning) {
            if (_nextSpawnTime < Time.time) {
                if (Waves[_waveCounter].IsFinished()) {
                    if (!FindAnyObjectByType<Enemy>()) {
                        _isWaveRunning = false;
                        Bank.AddMoney.Invoke(Waves[_waveCounter].Reward);
                    }

                    return;
                }
                GameObject newestEnemy = Instantiate(Waves[_waveCounter].GetNextEnemy(), transform.position, Quaternion.identity);
                _nextSpawnTime += Waves[_waveCounter].GetNextDelay();
                newestEnemy.GetComponent<Enemy>().Target = FirstNode;
            }
        }
    }

    [ContextMenu("Start wave")]
    public void StartWave() {
        if (_waveCounter >= Waves.Count-1 || (_waveCounter >= 0 && !Waves[_waveCounter].IsFinished())) return;
        _isWaveRunning = true;
        _nextSpawnTime = Time.time;
        _waveCounter++;
        WaveCounterChanged.Invoke(_waveCounter+1, Waves.Count);
    }
}
