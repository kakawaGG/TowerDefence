using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : Projectile
{
    [SerializeField] private LineRenderer _lineRenderer;

    public override void LaunchProjectile(Transform startPosition, Enemy targetEnemy, float damage)
    {
        base.LaunchProjectile(startPosition, targetEnemy, damage);

        _lineRenderer.SetWidth(0, 1);
        StopCoroutine("LineWidthChanger");
        StartCoroutine("LineWidthChanger");
        _lineRenderer.SetPosition(0, startPosition.position);
        _lineRenderer.SetPosition(1, targetEnemy.transform.position);

        targetEnemy.TakeDamage(damage);
    }

    private IEnumerator LineWidthChanger()
    {
        _lineRenderer.SetWidth(0, 1);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
