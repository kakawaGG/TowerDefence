using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUI : MonoBehaviour
{
    [SerializeField] private Text _nextUpgradeCostText;
    [SerializeField] private UpgradeableTower _tower;

    private void OnEnable()
    {
        StartCoroutine(AddListener());    
    }

    IEnumerator AddListener()
    {
        _tower.Ungraded += Ungraded;
        yield return new WaitForSeconds(0.1f);
    }

    private void OnDisable()
    {
        _tower.Ungraded -= Ungraded;
    }

    private void Ungraded(string nextUpgradeCost)
    {
        _nextUpgradeCostText.text = nextUpgradeCost;
    }

}
