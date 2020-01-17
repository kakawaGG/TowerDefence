using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour, IShootable
{
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private ParticleSystem _shootEffect;

    protected float _damage;
    protected float _fireRate; // Скорострельность считается в выстрелах в минуту (Например: 30 означает 30 выстрелов в минуту)

    private List<Enemy> enemies = new List<Enemy>();
    
    protected virtual void OnEnable()
    {
        Initialize();
        StartCoroutine(Shooting());
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void Initialize() { }

    private void OnTriggerEnter(Collider other)
    {
        Enemy potentialEnemy = other.GetComponentInParent<Enemy>();
        if (potentialEnemy)
        {
            enemies.Add(potentialEnemy);
            potentialEnemy.EnemyDead += OnEnemyDead;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy potentialEnemy = other.GetComponentInParent<Enemy>();
        if (potentialEnemy)
        {
            potentialEnemy.EnemyDead -= OnEnemyDead;
            enemies.Remove(potentialEnemy);
        }
    }

    public virtual void Shoot()
    {
        if (enemies.Count > 0)
        {
            Projectile projectile = Instantiate(_projectile);
            projectile.LaunchProjectile(_shootPosition, enemies[0], _damage);
            _shootEffect.Play();

            Debug.Log(gameObject.name +  " выстрелил в противника снарядом типа " + projectile.name);
        }
    }

    protected IEnumerator Shooting()
    {
        Debug.Log("Коррутина стрельбы начата");
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(60 / _fireRate);
        }
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.EnemyDead -= OnEnemyDead;
        enemies.Remove(enemy);
    }
}
