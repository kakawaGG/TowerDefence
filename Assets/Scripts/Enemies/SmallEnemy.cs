using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallEnemy : Enemy
{
    public override void IncreaseStatsByPercent(float percent)
    {
        _maxHealth += _maxHealth * percent;
        _health = _maxHealth;
        _goldCost += _goldCost * (int)percent;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        model.transform.DOShakePosition(0.5f, 0.5f, 90);
    }

    protected override void PlayDeathEffect()
    {
        deathEffect.Play();
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutBack).OnComplete(() => model.SetActive(false));
    }
}
