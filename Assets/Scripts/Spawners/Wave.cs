using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private float _increasedStatsPercent; // Процент усиления параметров противника

    private WaveSettings _waveSettings;
    private EnemyPath _path;

    private List<Enemy> _enemies = new List<Enemy>();

    public void Initialize(WaveSettings waveSettings, EnemyPath path, float increasedStatsPercent)
    {
        _waveSettings = waveSettings;
        _path = path;
        _increasedStatsPercent = increasedStatsPercent;
    }

    public void StartEnemySpawn()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        int targetEnemiesCount = Mathf.Abs(_waveSettings.enemiesCount) + Random.Range(0, Mathf.Abs(_waveSettings.enemiesRandomDelta));
        for (int i = 0; i < targetEnemiesCount; i++)
        {
            SpawnEnemy();
            if (i == targetEnemiesCount) yield break;
            yield return new WaitForSeconds(_waveSettings.timeBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(_waveSettings.enemyPrefab, _path.pathPoints[0]);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        IncreaseEnemyStats(enemy, _increasedStatsPercent);
        enemy.EnemyDead += EnemyDead;
        _enemies.Add(enemy);

        enemy.StartMoving(_path);
    }

    private void IncreaseEnemyStats(Enemy enemy, float percent)
    {
        enemy.IncreaseStatsByPercent(percent);
    }

    public void KillAllEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.EnemyDead -= EnemyDead;
            enemy.TakeDamage(-1);
        }
        _enemies.Clear();
    }

    private void EnemyDead(Enemy enemy)
    {
        enemy.EnemyDead -= EnemyDead;
        _enemies.Remove(enemy);
    }
}
