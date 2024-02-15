using System;
using System.Collections;
using System.Collections.Generic;
public class Round
{
    private IPlayer[] _players;
    public int _currentPlayerTurn;
    public Action _onFinishRound;

    public Round(IPlayer [] players)
    {
        _players = players;
        _currentPlayerTurn = 0;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].OnFinishTurn += OnFinishTurn;
            _players[i].HasTurnStart = false;
        }
    }

    public void StartRound()
    {
        ChangePlayer();
    }

    public void OnFinishTurn()
    {
        _currentPlayerTurn++;
        ChangePlayer();
    }

    public void OnFinishRound()
    {
        _onFinishRound?.Invoke();
        
    }

    public void ResetTurns()
    {
        for(int i = 0; i <_players.Length; i++)
        {
            _players[i].HasTurnStart = false;
        }

        _currentPlayerTurn = 0;
    }

    private void ChangePlayer()
    {
        if (_currentPlayerTurn > _players.Length - 1)
        {
            OnFinishRound();
            return;
        }
            
        _players[_currentPlayerTurn].StartPlayerTurn();
    }
}
