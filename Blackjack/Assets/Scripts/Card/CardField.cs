using UnityEngine;

public class CardField : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Color _onMouseOverColor;
    private Color _defaultColor;

    public bool isMouseOver;
    public bool isDiscartDeck;

    public Card currentCard;

    private void Start()
    {
        _defaultColor = _spriteRenderer.color;
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
        _spriteRenderer.color = _onMouseOverColor;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        _spriteRenderer.color = _defaultColor;
    }
}
