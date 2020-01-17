using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// Данный скрипт может отличаться от уровня к уровню. Он задан для настройки геймплея в зависимости от решений геймдизайнера.
/// Например, необходимо сделать последовательное появление волн с разных точек в зависимости от времени игры или номера волны,
/// либо запустить какое-либо событие (авиаудар, внезапная атака, бонусное усиление).
///</summary> 

public class Level0Gameplay : MonoBehaviour
{
    [SerializeField] private int _startGold;
    [SerializeField] private int _startHP;

    [SerializeField] private List<WaveSpawner> _waveSpawners = new List<WaveSpawner>();
    [SerializeField] private List<UpgradeableTower> _towers = new List<UpgradeableTower>();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        Gamestats.Initialize(_startGold, _startHP);
        Gamestats.GameEnded += GameEnded;
    }

    private void GameEnded()
    {
        ClearGameField();
    }

    public void StartGame()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
            waveSpawner.StartSpawn();
    }

    public void RestartGame()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
        {
            waveSpawner.StopSpawnAndClear();
            waveSpawner.StartSpawn();
        }

        foreach (UpgradeableTower upgradeableTower in _towers)
        {
            upgradeableTower.DropUpgrades();
        }
        Gamestats.Initialize(_startGold, _startHP);
    }

    public void ClearGameField()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
        {
            waveSpawner.StopSpawnAndClear();
        }
    }

    [ContextMenu("KillPlayer")]
    public void KillPlayer()
    {
        Gamestats.TrySpendHP(200);
    }
}