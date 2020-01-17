using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Transform _startPosition;
    protected Enemy _targetEnemy;
    protected float _damage;

    public virtual void LaunchProjectile(Transform startPosition, Enemy targetEnemy, float damage)
    {

    }
}
