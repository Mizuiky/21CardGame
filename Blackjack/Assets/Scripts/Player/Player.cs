using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField]
    private PlayerHand _hand;

    public int points;
    public bool canGetCards;
    public bool hasReachedCardNumber; 
    
    #region Events
    private Action _onFinishTurn;
    public Action OnFinishTurn { get { return _onFinishTurn; } set { _onFinishTurn = value; } }

    public event IPlayer.GetCardEventHandler OnGetCard;

    public event IPlayer.DiscartCardEventHandler OnDiscartCard;
    #endregion

    private bool _hasTurnStart;
    public bool HasTurnStart { get { return _hasTurnStart; } set { _hasTurnStart = value; } }

    private int _playerNumber;
    public int PlayerNumber { get { return _playerNumber; } set { _playerNumber = value; } }

    private int _pointsToReach21;
    public int PointsToReach21 { get { return _pointsToReach21; } }

    public PlayerHand PlayerHand { get { return _hand; } }

    public void Init()
    {
        _hand.Init();
        Reset();
    }

    public void Reset()
    {
        points = 0;
        _pointsToReach21 = 0;

        hasReachedCardNumber = false;

        _hasTurnStart = false;

        canGetCards = false;
    }

    public void FinishPlayerTurn()
    {
        canGetCards = false;
        _hasTurnStart = false;

        OnFinishTurn?.Invoke();
    }

    public void StartPlayerTurn()
    {
        _hasTurnStart = true;
        canGetCards = true;
        //habilitar o drag e drop no discart
    }

    public void GetCard()
    {
        Card card = OnGetCard?.Invoke(this);

        if(card != null)
            AddCard(card);
    }

    public void StopGettingCards()
    {
        hasReachedCardNumber = true;
    }

    public void GetPoints()
    {
        points = _hand.GetCardPoints();

        _pointsToReach21 -= points;
    }

    public void AddCard(Card card)
    {
        _hand.AddCard(card);
    }

    public void DiscartCard(Card card)
    {
        OnDiscartCard?.Invoke(card);
    }
}
