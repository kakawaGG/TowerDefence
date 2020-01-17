using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const float MICRO_TIME_TO_ROTATE = 0.005f;
    private const float TIME_TO_DESTROY_AFTER_DIE = 2f;

    public delegate void OnEnemyDead(Enemy enemy);
    public event OnEnemyDead EnemyDead;

    public delegate void OnEnemyCompletePath(float goldCost);
    public event OnEnemyCompletePath EnemyCompletePath;

    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _health { get; set; }
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _goldCost;
    [SerializeField] protected int _damageToPlayer;

    private EnemyPath _path { get; set; }
    [SerializeField] protected GameObject model;
    [SerializeField] protected ParticleSystem deathEffect;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void StartMoving(EnemyPath path) {
        _path = path;
        transform.DOPath(_path.GetPathAsVectorMassive(), _moveSpeed, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(MICRO_TIME_TO_ROTATE)
            .OnComplete(() =>
            {
                CompletePath();
            });

        Debug.Log(gameObject.name + " начал путь");
    }

    public virtual void StopMoving() { transform.DOPause(); _animator.speed = 0; }

    public virtual void RestartMoving() { }

    public virtual void IncreaseStatsByPercent(float percent) { }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0) { Die();}
        if (damage < 0) { Die(); }
    }

    protected virtual void Die() {
        StopMoving();
        PlayDeathEffect();
        GiveGoldToPlayer();
        NotifyAboutDeath();
        Gamestats.AddKilledEnemiesCount();
        Destroy(gameObject, TIME_TO_DESTROY_AFTER_DIE);
    }

    protected void CompletePath()
    {
        Debug.Log(transform.name + " завершил путь");
        GiveDamageToPlayer();
        Die();
    }

    private void GiveGoldToPlayer()
    {
        Gamestats.TryAddGold(_goldCost);
    }

    private void GiveDamageToPlayer()
    {
        Gamestats.TrySpendHP(_damageToPlayer);
    }

    protected virtual void PlayDeathEffect()
    {
        deathEffect.Play();
        model.SetActive(false);
    }

    private void NotifyAboutDeath() { EnemyDead?.Invoke(this); Debug.Log(gameObject.name + " is dead"); }
    private void NotifyAboutCompletePath() { EnemyCompletePath?.Invoke(_goldCost); Debug.Log(gameObject.name + " is complete path"); }
}
