using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _goldText;

    private void Awake()
    {
        Gamestats.GoldChanges += GoldChanges;
        Gamestats.HpChanges += HpChanges;
    }

    private void HpChanges(int hp)
    {
        _hpText.text = hp.ToString();
    }

    private void GoldChanges(int gold)
    {
        _goldText.text = gold.ToString();
    }
}
