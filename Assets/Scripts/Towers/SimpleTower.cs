using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleTower : Tower
{
    [SerializeField] private UpgradeInfo _towerBaseStats;

    protected override void Initialize()
    {
        base.Initialize();

        _damage = _towerBaseStats.damage;
        _fireRate = _towerBaseStats.fireRate;
    }
}
