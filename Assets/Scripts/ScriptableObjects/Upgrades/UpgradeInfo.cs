using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade info", order = 5)]
public class UpgradeInfo : ScriptableObject
{
    public int cost;
    public float damage;
    public float fireRate;

    public GameObject model;
}
