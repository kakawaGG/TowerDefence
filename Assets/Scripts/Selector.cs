using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask layerMask;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                ISelectable selectable = hit.transform.GetComponentInParent<ISelectable>();
                if(selectable != null) selectable.DoWhenSelected();
            }
            Debug.Log("Луч выделения выстрелил");
        }
    }
}
