using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestats
{
    public delegate void OnGoldChanges(int gold);
    public static event OnGoldChanges GoldChanges;

    public delegate void OnHPChanges(int hp);
    public static event OnHPChanges HpChanges;

    public delegate void OnGameEnded();
    public static event OnGameEnded GameEnded;

    private static int _gold;
    private static int _hp;
    private static int _enemiesKilledCount;

    public static void Initialize(int gold, int hp)
    {
        _gold = gold;
        _hp = hp;
        _enemiesKilledCount = 0;
        GoldChanges?.Invoke(_gold);
        HpChanges?.Invoke(_hp);
    }

    public static bool TrySpendGold(int gold)
    {
        if (_gold - gold > 0)
        {
            _gold -= gold;
            GoldChanges?.Invoke(_gold);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void TryAddGold(int gold)
    {
        _gold += gold;
        GoldChanges?.Invoke(_gold);
    }

    public static void TrySpendHP(int hp)
    {
        Debug.Log("Попытка нанести урон зафиксирована");
        _hp -= hp;
        if (_hp <= 0)
        {
            _hp = 0; GameEnded?.Invoke();
        }
        
        HpChanges?.Invoke(_hp);
    }

    public static int GetEnemiesKilledCount()
    {
        return _enemiesKilledCount;
    }

    public static void AddKilledEnemiesCount()
    {
        _enemiesKilledCount++;
    }
}
