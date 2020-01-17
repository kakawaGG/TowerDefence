using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyPath _path;
    [SerializeField] private Wave _wave;
    [SerializeField] private WaveSpawnerSettings _spawnerSettings;

    [SerializeField] private List<WaveSettings> _wavesSettings = new List<WaveSettings>();

    private Queue<Wave> _wavesQueue = new Queue<Wave>();
    private Wave _curentWave;

    private List<Wave> _waves = new List<Wave>();
    private List<Enemy> _enemies = new List<Enemy>();

    public void StartSpawn()
    {
        StartCoroutine(WaveStartingSpawnEnemies());
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    public void StopSpawnAndClear()
    {
        StopAllCoroutines();
        KillAllEnemies();
        DestroyAllWaves();
    }

    private void KillAllEnemies()
    {
        foreach (Wave wave in _waves)
        {
            wave.KillAllEnemies();
        }
    }

    private void DestroyAllWaves()
    {
        foreach (Wave wave in _waves)
        {
            Destroy(wave.gameObject);
        }
        _waves.Clear();
        _wavesQueue.Clear();
    }

    private IEnumerator WaveStartingSpawnEnemies()
    {
        for(int i = 0; i < _wavesSettings.Count; i++)
        {
            Wave wave = Instantiate(_wave, transform);
            float increasedStatsPercent = _spawnerSettings.increaceStatPercentPerWave * (i+1);
            wave.Initialize(_wavesSettings[i], _path, increasedStatsPercent * 0.1f);
            _waves.Add(wave);

            Debug.Log("Волна №" + i + " запущена и увеличена в статах на " + increasedStatsPercent + " процентов");
        }

        _wavesQueue = new Queue<Wave>(_waves.ToArray());

        for (int i = 0; i < _waves.Count; i++)
        {
            _curentWave = _wavesQueue.Dequeue();
            _curentWave.StartEnemySpawn();
            yield return new WaitForSeconds(_spawnerSettings.timeBetweenWaves);
        }
    }
}
