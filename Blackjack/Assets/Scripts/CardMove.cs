using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    [SerializeField]
    private float _moveDurationValue;
    [SerializeField]
    private float _cardRotationValue;

    [SerializeField]
    private float _rotationAngle;

    [SerializeField]
    private float _toPositionMoveValue;
    [SerializeField]
    private float _toRotationValue;

    [SerializeField]
    public CardField [] _cardFields;

    public bool isSelected;

    public int deckIndex;
    public int discartIndex;
    public int handIndex;

    public int deckPositionNumber;

    private Vector3 _deckPosition;
    private Vector3 _discartPosition;
    private Vector3 _handPosition;
    private Quaternion _handRotation;

    private bool _isActive;
    public bool IsActive { get { return _isActive; } set { _isActive = value; } }

    private Vector3 _toPosition;
    private Quaternion _toRotation;
    private Vector3 _toScale;

    private Vector3 mousePosition;
    private Vector3 worldPositionFar;
    private Vector3 worldPositionNear;

    private GameObject selectedField;

    public LayerMask layerToHit;

    private Ray ray;

    public void Init(int number)
    {
        deckPositionNumber = number;

        deckIndex = 0;
        discartIndex = -1;
        handIndex = -1;

        _deckPosition = GameManager.Instance._tableFields[1].position;
        _discartPosition = GameManager.Instance._tableFields[2].position;

        _cardFields = new CardField[3];

        _toPosition = new Vector3(0, 0, -0.01f);
        _toRotation = Quaternion.Euler(0, 180, 0);
        _toScale = new Vector3(1, 1, 1);

        _isActive = true;
    }

    public void Update()
    {
        if(_isActive)
        {
            if (!isSelected)
                MoveToPosition();

            if (Input.GetMouseButtonUp(0))
                isSelected = false;
        }       
    }

    private void MoveToPosition()
    {
        _toPosition = new Vector3(0, 0, -0.01f);
        _toRotation = Quaternion.Euler(0, 180, 0);
        _toScale = new Vector3(1, 1, 1);

        if (deckIndex > -1)
        {
            _toPosition = _deckPosition;
            _toPosition.z = -0.01f - deckPositionNumber * 0.03f;

            _toScale = new Vector3(0.8f, 0.8f, 1);
        }

        else if(discartIndex > -1)
        {
            _toPosition = _discartPosition;
            _toPosition.z = -0.01f - deckPositionNumber * 0.03f;

            _toScale = new Vector3(0.8f, 0.8f, 1);
        }

        else if(handIndex > -1)
        {
            Debug.Log("hand position");

            _toPosition = _handPosition;
            _toRotation = _handRotation;
            _toScale = new Vector3(1.02f, 1.02f, 1);
        }

        if (transform.position != _toPosition)
            transform.position = Vector3.Lerp(transform.position, _toPosition, Time.deltaTime * _toPositionMoveValue);

        if (transform.rotation != _toRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, _toRotation, Time.deltaTime * _toRotationValue);

        if (transform.localScale != _toScale)
            transform.localScale = Vector3.Lerp(transform.localScale, _toScale, Time.deltaTime * _toPositionMoveValue);
    }

    void OnMouseDrag()
    {
        Debug.Log("is dragging");
        isSelected = true;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = -0.01f - deckPositionNumber * 0.03f;

        Quaternion toRotation = Quaternion.Euler((mousePosition.y - transform.position.y) * _rotationAngle, -(mousePosition.x - transform.position.x) * _rotationAngle, mousePosition.z);

        transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * _moveDurationValue);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * _cardRotationValue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 20f);
    }

    public void SetHandPosition(Vector3 handPosition)
    {
        _handPosition = handPosition;
    }

    private void OnMouseUp()
    {
        Debug.Log("on mouse up");

        RaycastHit hit = CastRay();

        _cardFields = GameManager.Instance.RoundManager.GetCurrentPlayer().PlayerHand._cardFields;

        if (hit.collider != null && hit.collider.CompareTag("CardField"))
        {
            Debug.Log("hit");
            selectedField = hit.collider.gameObject;

            CardField field = selectedField.GetComponent<CardField>();

            if(field.isMouseOver)
            {
                if(field.isDiscartDeck)
                {
                    deckIndex = -1;
                    handIndex = -1;
                    discartIndex = 0;
                }
                else
                {
                    _handPosition = field.transform.position;
                    _handRotation = field.transform.rotation;

                    deckIndex = -1;
                    handIndex = 0;

                    //field.currentCard = this;
                }          
            }
        }      
    }

    private RaycastHit CastRay()
    {
        Vector3 screenPosition = Input.mousePosition;
     
        ray = Camera.main.ScreenPointToRay(screenPosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
        {
            GameObject target = hit.collider.gameObject;
            Debug.Log(target.name);
        }

        return hit;
    }
}
