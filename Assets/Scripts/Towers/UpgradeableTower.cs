using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TowerUpgrade
{
    public UpgradeInfo UpgradeInfo;
    [SerializeField] private GameObject upgradeModel;

    public void HideUpgrade()
    {
        upgradeModel.SetActive(false);
    }

    public void ShowUpgrade()
    {
        upgradeModel.SetActive(true);
    }
}

public class UpgradeableTower : Tower, ISelectable
{
    private const string FULL_UPGRADED = "MAX UPGRADE";

    public delegate void OnUngraded(string nextUpgradeCost);
    public event OnUngraded Ungraded;

    [SerializeField] private List<TowerUpgrade> _upgrades;
    private TowerUpgrade _curentUpgrade;

    protected override void OnEnable()
    {
        HideAllUpgrades();
        Initialize();
        StartCoroutine(Shooting());
    }

    protected override void Initialize()
    {
        _damage = _upgrades[0].UpgradeInfo.damage;
        _fireRate = _upgrades[0].UpgradeInfo.fireRate;
        UpgradeTower(_upgrades[0]); Ungraded?.Invoke(_upgrades[1].UpgradeInfo.cost.ToString());
    }

    private void TryUpgradeTower()
    {
        // Попытка сделать новый апгрейд. Если:
        // - такой апгрейд существует
        // - текущий апгрейд не является максимальным
        // - на апгрейд хватает ресурсов
        // то происходит апгрейд путем показа модели и изменения параметров

        int index = _upgrades.IndexOf(_curentUpgrade);
        if (index != _upgrades.Count - 1 && Gamestats.TrySpendGold(_upgrades[index + 1].UpgradeInfo.cost))
        {
            UpgradeTower(_upgrades[index + 1]);
            if (index + 2 > _upgrades.Count - 1) Ungraded?.Invoke(FULL_UPGRADED); else Ungraded?.Invoke(_upgrades[index + 2].UpgradeInfo.cost.ToString());
        }
        Debug.Log("Попытка апгрейда зафиксирована");
    }

    private void UpgradeTower(TowerUpgrade towerUpgrade)
    {
        _curentUpgrade = towerUpgrade;
        _curentUpgrade.ShowUpgrade();
        _damage = _curentUpgrade.UpgradeInfo.damage;
        _fireRate = _curentUpgrade.UpgradeInfo.fireRate;
    }

    private void HideAllUpgrades()
    {
        foreach (TowerUpgrade towerUpgrade in _upgrades)
            towerUpgrade.HideUpgrade();
    }

    private void ShowAllUpgrades()
    {
        foreach (TowerUpgrade towerUpgrade in _upgrades)
            towerUpgrade.ShowUpgrade();
    }

    public void DropUpgrades()
    {
        HideAllUpgrades();
        UpgradeTower(_upgrades[0]); Ungraded?.Invoke(_upgrades[1].UpgradeInfo.cost.ToString());
    }

    public void DoWhenSelected()
    {
        TryUpgradeTower();
    }
}
