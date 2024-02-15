using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RoundManager
{
    private IPlayer [] _players;

    private DealerController _dealer;
    private Transform _cardInitialPosition;

    public Action<int> finishRound;

    private Round _round;
    private int _currentRoundNumber;

    private float moveDuration = 0.8f;
    private float rotationDuration = 1.5f;

    public RoundManager(DealerController dealer, Transform cardInitialPosition, IPlayer [] players)
    {
        _dealer = dealer;
        _cardInitialPosition = cardInitialPosition;

        _players = new IPlayer[players.Length];
        _players = players;

        CreateRound();

        Init();
    }

    public IPlayer GetCurrentPlayer()
    {
        return _players[_round._currentPlayerTurn];
    }

    public void CreateRound()
    {       
        _round = new Round(_players);
        _round._onFinishRound += OnFinishRound;      
    }

    private void Init()
    {
        _dealer.Init();

        _currentRoundNumber = 0;

        for (int i = 0; i < _players.Length; i ++)
        {
            _players[i].OnGetCard += GetNewCard;
            _players[i].OnDiscartCard += DiscartCard;
            _players[i].PlayerNumber = i;
        }

        StartGame();
        CreateRound();
    }

    private void OnFinishRound()
    {
        _currentRoundNumber++;
        //finish current round
        _round.ResetTurns();
    }

    private Card GetNewCard(object sender)
    {
        return _dealer.GiveCard();          
    }

    private void DiscartCard(object sender)
    {
        _dealer.AddCardToDiscartPile((Card)sender);
    }

    private void StartGame()
    {
        GameManager.Instance.InitCoroutine(StartGameCoroutine);
    }

    private IEnumerator StartGameCoroutine()
    {
        for(int i = 0; i < _players.Length; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Card card = _dealer.GiveCard();
                card.transform.SetParent(null);

                AnimateCard(card);

                yield return new WaitForSeconds(1.5f);

                _players[i].AddCard(card);
            }            
        }

        yield return new WaitForSeconds(1f);
    }

    private void AnimateCard(Card card)
    {
        card.transform.DOMove(_cardInitialPosition.position, moveDuration);
        //card.transform.DORotate(new Vector3(0, 0, 0), rotationDuration);
    }
}
