using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHand : MonoBehaviour
{
    public CardField [] _cardFields;

    [SerializeField]
    private Transform _cardContainer;

    private Card[] _handCards;
    private int _currentActiveCards;
    private int _points;

    private float moveDuration = 2f;
    private float rotationDuration = 3f;
    private float scaleDuration = 0.5f;

    public void Init()
    {
        _currentActiveCards = 0;
        _handCards = new Card[3];
    }

    public void AddCard(Card newCard)
    {
        if (_currentActiveCards < 3)
        {
            _handCards[_currentActiveCards] = newCard;

            newCard.SetCardIndex(_currentActiveCards);

            newCard.SetCardFields(_cardFields);

            newCard.SetCardPosition(_cardFields[_currentActiveCards].transform, CardPositon.Hand);

            newCard.transform.SetParent(_cardContainer);

            _currentActiveCards++;
        }            
    }

    //public void RemoveCard()
    //{
    //    _currentCardPosition--;

    //    _cardIndex[_currentCardPosition] = null;
    //}

    private void DoCardPositionAnimation(Card card)
    {
        card.transform.DOMove(_cardFields[_currentActiveCards].transform.position, moveDuration);
    }

    public int GetCardPoints()
    {
        for(int i = 0; i < _handCards.Length; i++)
        {
            Card card = _handCards[i].GetComponent<Card>();

            if(card != null)
                _points += card.points;
        }

        return _points;
    }
}

