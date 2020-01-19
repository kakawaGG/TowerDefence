using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    private const string NOT_UPGRADED = "NONE UPGRADE";

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
        base.Initialize();
        if(_upgrades.Count > 0)
            UpgradeTower(_upgrades.First());
        else
            Ungraded?.Invoke(NOT_UPGRADED);
    }

    private void TryUpgradeTower()
    {
        bool isLastUpgrade = _curentUpgrade == _upgrades.Last() ? true : false;

        if (!isLastUpgrade)
        {
            TowerUpgrade nextUpgrade = _upgrades[_upgrades.IndexOf(_curentUpgrade) + 1];
            bool canBuyUpgrade = Gamestats.TrySpendGold(nextUpgrade.UpgradeInfo.cost);

            if (canBuyUpgrade)
            {
                UpgradeTower(nextUpgrade);
            }
        }

        Debug.Log("Попытка апгрейда зафиксирована");
    }

    private void UpgradeTower(TowerUpgrade towerUpgrade)
    {
        if (towerUpgrade != null)
        {
            _curentUpgrade = towerUpgrade;
            _curentUpgrade.ShowUpgrade();
            _damage = _curentUpgrade.UpgradeInfo.damage;
            _fireRate = _curentUpgrade.UpgradeInfo.fireRate;

            if (towerUpgrade == _upgrades.Last())
            {
                Ungraded?.Invoke(FULL_UPGRADED);
            }
            else
            {
                UpgradeInfo upgradeInfo = _upgrades[_upgrades.IndexOf(towerUpgrade) + 1].UpgradeInfo;
                Ungraded?.Invoke(upgradeInfo.cost.ToString());
            }
        }
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
        Initialize();
    }

    public void DoWhenSelected()
    {
        if (_upgrades.Count > 0) TryUpgradeTower();
    }
}
