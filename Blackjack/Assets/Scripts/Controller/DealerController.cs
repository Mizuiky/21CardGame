using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController
{
    public Sprite [] clubsCards;
    public Sprite [] diamondsCards;
    public Sprite [] heartsCards;
    public Sprite [] spadesCards;

    private Card [] deck;
    private Card[] tempDeck;
    

    public Stack<Card> cardStack;
    public Stack<Card> discartStack;
    
    private int _currentIndex;

    private int [] suffleDeck;

    public DealerController(Sprite[] clubsCards, Sprite[] diamondsCards, Sprite[] heartsCards, Sprite[] spadesCards)
    {
        this.clubsCards = clubsCards;
        this.diamondsCards = diamondsCards;
        this.heartsCards = heartsCards;
        this.spadesCards = spadesCards;
    }

    public void Init()
    {
        deck = new Card[52];
        suffleDeck = new int [52];
        tempDeck = new Card[52];
        cardStack = new Stack<Card>();
        discartStack = new Stack<Card>();

        for (int i = 0; i < 52; i ++)
        {
            suffleDeck[i] = i;
        }

        CreateDeck();
        ShufleDeck();
        //OrganizeDeck(deck);
    }

    private void CreateDeck()
    {
        _currentIndex = 0;

       FillDeck(clubsCards, CardType.Clubs);
       FillDeck(diamondsCards, CardType.Diamonds);
       FillDeck(heartsCards, CardType.Hearts);
       FillDeck(spadesCards, CardType.Spades);
    }

    private void ShufleDeck()
    {
        suffleDeck = suffleDeck.OrderBy(x => Guid.NewGuid()).ToArray();

        for(int i = deck.Length - 1; i >= 0; i--)
        {
            Card card = tempDeck[suffleDeck[i]];
            card._deckPositionNumber = i;

            Debug.Log(suffleDeck[i] + " + " + card.type + card.points + " DeckPositionNumber " + card._deckPositionNumber);

            cardStack.Push(card);

            card.Init();
        }
    }

    private void FillDeck(Sprite [] suitCards, CardType cardType)
    {  
        for(int i = 0; i < 13; i++)
        {
            var card = GameManager.Instance.CreateCard();

            if(card != null)
            {
                if (i == 9 || i == 10 || i == 11)
                    card.points = 10;

                else
                    card.points = i + 1;

                card.type = cardType;
                card.image.sprite = suitCards[i];
                card.ChangeSide(CardSide.Front);

                tempDeck[_currentIndex] = card;

                _currentIndex++;
            }
        }
    }

    public void AddCardToDiscartPile(Card card)
    {
        discartStack.Push(card);
    }

    private void OrganizeDeckGrid(Card [] deck)
    {
        var horizontal = 0f;
        var vertical = 0f;

        var countCardSuit = 0;

        var count = 0;

        for(int i = 0; i < deck.Length; i++)
        {
            if(deck[i] != null)
            {
                deck[i].transform.position = new Vector3(deck[i].transform.position.x + horizontal, deck[i].transform.position.y + vertical, deck[i].transform.position.z);

                if (count > 11)
                {
                    count = 0;
                    countCardSuit++;

                    horizontal = 0;
                    vertical = -3f;

                    vertical = countCardSuit * vertical;
                }
                else
                {
                    horizontal += 3;
                    count++;
                }
            }         
        }
    }

    public Card GiveCard()
    {
        Card card = cardStack.Pop();
        return card;
    }
}
