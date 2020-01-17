using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndedUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameEndPanel;
    [SerializeField] private Text _enemiesKilledText;

    private void OnEnable()
    {
        Gamestats.GameEnded += GameEnded;
    }

    private void OnDisable()
    {
        Gamestats.GameEnded -= GameEnded;
    }

    private void GameEnded()
    {
        _gameEndPanel.SetActive(true);
        _enemiesKilledText.text = Gamestats.GetEnemiesKilledCount().ToString();
    }

    public void HidePanel()
    {
        _gameEndPanel.SetActive(false);
    }
}
