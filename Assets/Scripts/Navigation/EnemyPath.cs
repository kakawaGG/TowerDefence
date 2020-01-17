using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [HideInInspector] public List<Transform> pathPoints;

    private void Awake()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
            pathPoints.Add(transforms[i]);
    }

    public Vector3[] GetPathAsVectorMassive()
    {
        Vector3[] vector3s = new Vector3[pathPoints.Count];
        for(int i = 0; i < pathPoints.Count; i++)
        {
            vector3s[i] = pathPoints[i].position;
        }
        return vector3s;
    }
}
