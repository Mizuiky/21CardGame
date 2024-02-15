using System;

public interface IPlayer
{
    public bool HasTurnStart { get; set; }
    public int PlayerNumber { get; set; }
    public Action OnFinishTurn { get; set; }

    public PlayerHand PlayerHand { get; }

    public delegate Card GetCardEventHandler(object sender);
    public event GetCardEventHandler OnGetCard;

    public delegate void DiscartCardEventHandler(object sender);
    public event DiscartCardEventHandler OnDiscartCard;

    public void Init();
    public void Reset();

    public void GetCard();
    public void AddCard(Card card);

    public void DiscartCard(Card card);

    public void FinishPlayerTurn();
    public void StartPlayerTurn();

    public void GetPoints();
}
