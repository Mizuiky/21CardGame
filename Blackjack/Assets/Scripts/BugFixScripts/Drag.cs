using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public bool isSelected;
    public float mouseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            isSelected = true;

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        if (isSelected)
        {

            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = -0.01f;

            Vector3 worldPositon = Camera.main.ScreenToWorldPoint(screenPosition);

            transform.position = Vector3.Lerp(transform.position, worldPositon, mouseSpeed * Time.deltaTime);

        }
    }
}
