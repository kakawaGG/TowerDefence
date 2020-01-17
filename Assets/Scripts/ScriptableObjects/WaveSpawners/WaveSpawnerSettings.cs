using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSpawnerSettings", menuName = "WaveSpawnerSettings", order = 5)]
public class WaveSpawnerSettings : ScriptableObject
{
    public float timeBetweenWaves;
    public float increaceStatPercentPerWave;
}
