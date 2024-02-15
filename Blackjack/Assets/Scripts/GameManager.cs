using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _playerQtd;

    [SerializeField]
    private Transform [] _playerPositions;

    [SerializeField]
    private GameObject _playerPFB;

    [SerializeField]
    public GameObject cardPrefab;

    public Transform[] _tableFields;

    public static GameManager Instance;

    private DealerController _dealer;

    private RoundManager _roundManager;
    public RoundManager RoundManager { get { return _roundManager; } }

    private IPlayer [] _players;

    public Sprite[] clubsCards;
    public Sprite[] diamondsCards;
    public Sprite[] heartsCards;
    public Sprite[] spadesCards;

    public delegate IEnumerator CorotineDelegate();

    public void Awake()
    {
        if (Instance == null)
            Instance = GetComponent<GameManager>();
        else
            Destroy(Instance);
    }

    public void Start()
    {
        //Init();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Init();
        }
    }

    private void Init()
    {
        CreatePlayers();

        _dealer = new DealerController(clubsCards, diamondsCards, heartsCards, spadesCards);
        _roundManager = new RoundManager(_dealer, _tableFields[0], _players);
    }

    private void CreatePlayers()
    {
        _players = new IPlayer [_playerQtd];

        for(int i = 0; i < _playerQtd; i++)
        {
            GameObject obj = Instantiate(_playerPFB, _playerPositions[i]);

            if(obj != null)
            {
                IPlayer player = obj.GetComponent<IPlayer>();
                _players[i] = player;

                if(player != null)
                    player.Init();
            }
        }
    }

    public Card CreateCard()
    {
        GameObject obj = Instantiate(cardPrefab);

        if (obj != null)
        {
            Card card = obj.GetComponent<Card>();

            if (card != null)
            {
                card.transform.SetParent(_tableFields[1].transform);

                return card;
            }            
        }

        return null;
    }

    public void InitCoroutine(CorotineDelegate corotine)
    {
        StartCoroutine(corotine());
    }
}
