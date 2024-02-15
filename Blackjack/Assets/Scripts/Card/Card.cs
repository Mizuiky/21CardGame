using UnityEngine;

public enum CardType
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public enum CardSide
{
    Front,
    Back
}

public enum CardPositon
{
    Deck,
    Discart,
    Hand
}

public class Card : MonoBehaviour
{
    public int points;
    public CardType type;
    public SpriteRenderer image;

    [HideInInspector]
    public Transform position;

    public Vector3 startPosition;

    [SerializeField]
    private int handPosition;

    [SerializeField]
    private CardMove _cardMove;

    public int _deckPositionNumber;

    private Quaternion _newSide;

    public void Init()
    {
        _cardMove.Init(_deckPositionNumber);
    }

    public void SetCardFields(CardField [] fields)
    {
        _cardMove._cardFields = fields;
    }

    public void SetIsActive(bool isActive)
    {
        _cardMove.IsActive = isActive;
    }

    public void SetCardPosition(Transform handPosition, CardPositon cardPositon)
    {
        if(cardPositon == CardPositon.Deck)
        {
            _cardMove.deckIndex = 0;
            _cardMove.discartIndex = -1;
            _cardMove.handIndex = -1;
        }
        else if(cardPositon == CardPositon.Discart)
        {
            _cardMove.deckIndex = -1;
            _cardMove.discartIndex = 0;
            _cardMove.handIndex = -1;
        }
        else if(cardPositon == CardPositon.Hand)
        {
            Debug.Log("hand index set");
            _cardMove.SetHandPosition(handPosition.position);

            _cardMove.deckIndex = -1;
            _cardMove.discartIndex = -1;
            _cardMove.handIndex = 0;
        }
    }

    public void ChangeSide(CardSide side)
    {
        _newSide = Quaternion.identity;

        if (side == CardSide.Front)
            _newSide = Quaternion.identity;

        else
            _newSide = Quaternion.Euler(0, 180, 0); 
        
        transform.rotation = _newSide;
    }

    public void SetCardIndex(int index)
    {
        
    }
}
