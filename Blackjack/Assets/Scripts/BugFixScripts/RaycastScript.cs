using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    private RaycastHit hit;

    private Ray ray;

    private float distance;

    public bool isMouseOverBlock;

    public void Update()
    {
        CheckRayCast();
    }

    public void CheckRayCast()
    {
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //transform.position = point;

            Debug.DrawRay(ray.origin, Vector3.forward * 200, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                BlockSnow block = hit.collider.gameObject.GetComponent<BlockSnow>();

                if (block != null)
                {
                    block.ChangeColor(true);

                    block.isDragging = true;

                    Debug.Log("was hit" + block.blockNumber.ToString());
                }
            }

        }
    }

        

    public void MoveBlock()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        hit.transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * 15);
    }

}
