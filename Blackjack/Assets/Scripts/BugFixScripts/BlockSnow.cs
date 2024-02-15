using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSnow : MonoBehaviour
{
    public int blockNumber;

    public MeshRenderer blockMaterial;

    public Color onClickColor;

    public bool isDragging;

    public void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * 10);
            //MoveBlock();
        }
    }

    //public void OnMouseOver()
    //{
    //    isMouseOverBlock = true;
    //    blockMaterial.material.color = onClickColor;
    //}

    //public void OnMouseExit()
    //{
    //    isMouseOverBlock = false;
    //    isDragging = false;
    //    blockMaterial.material.color = Color.white;
    //}

    //public void OnMouseDrag()
    //{
    //    isDragging = true;
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePosition.z = 0;

    //    transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * 10);
    //}

    public void SetBlockNumber(int number)
    {
        blockNumber = number;
    }

    public void ChangeColor(bool isSelected)
    {
        if(isSelected)
            blockMaterial.material.color = onClickColor;
        else
            blockMaterial.material.color = Color.white;
    }

    private void OnMouseExit()
    {
        ChangeColor(false);
        isDragging = false;
    }
}
