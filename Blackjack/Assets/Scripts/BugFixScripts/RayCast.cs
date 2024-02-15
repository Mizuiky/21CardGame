using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    private RaycastHit hit;

    public bool isSelected;
    public int layerNumber = 6;
    public int layerMask;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("up");
            GameObject hitObject = GetClickedObject(out hit);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray.origin, ray.direction * 10f);

    }

    public GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;

        
        Debug.Log("layer mask " + layerMask);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            target = hit.collider.gameObject;
            Debug.Log(target.name);
        }

        return target;
    }
}
