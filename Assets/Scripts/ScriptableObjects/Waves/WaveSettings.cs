using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Enemies wave", order = 5)]
public class WaveSettings : ScriptableObject
{
    public int enemiesCount; // Переменная K из условия тех. задания
    public int enemiesRandomDelta; // Переменная X из условия тех. задания
    public float timeBetweenEnemies;

    public GameObject enemyPrefab;
}
