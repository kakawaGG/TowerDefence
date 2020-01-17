using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigEnemy : Enemy
{
    [SerializeField] private float _healPower;
    [SerializeField] private float _healPeriod;

    [SerializeField] private float _armor;

    public override void IncreaseStatsByPercent(float percent)
    {
        _maxHealth += _maxHealth * percent;
        _health = _maxHealth;
        _armor += _armor * percent;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(Mathf.Clamp(damage - _armor, 0, damage)); 
        model.transform.DOShakePosition(0.5f, 0.2f, 90);
    }

    protected override void PlayDeathEffect()
    {
        deathEffect.Play();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(transform.position, 1,1,1));
        sequence.Append(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutBack).OnComplete(() => model.SetActive(false)));
    }
}
