using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("초기위치")]
    public Transform _transform;

    [Header("Animator")]
    [Tooltip("플레이어의 애니메이션을 조정하기위한 Animator")]
    public Animator anim;
    [Tooltip("망원경 애니메이션을 위한 망원경")]
    public GameObject telescope;

    [HideInInspector]
    [Tooltip("플레이어의 움직임을 조정하기위한 Rigidbody")]
    public Rigidbody2D _rigidbody;

    [Header("Interact")]
    [Tooltip("상호작용 가능한 레이어 설정")]
    public LayerMask interactableLayer;
    [Tooltip("상호작용 영역의 크기")]
    public Vector2 interactionAreaSize = new(2f, 1f);
    [Tooltip("상호작용 가능 횟수")]
    public int interactCounter = 12;


    [Header("Movement")]
    [Tooltip("방향을 나타내는 변수")]
    public Vector2 movement;
    [Tooltip("걷는 속도")]
    public float walkSpeed = 5f;
    [Tooltip("뛰는 속도")]
    public float runSpeed = 8f;

    [Header("UIController")]
    public UIController _uiController;

    [Tooltip("인벤토리")]
    public InventoryManager inventoryManager;

    [Tooltip("몇 일차인지에 대한 변수")]
    public int dayCount = 1;

    [Tooltip("요리 성공에 대한 플래그")]
    public int cookCount = 0; //3이면 성공, 2이면 부분 성공 1이면 실패

    [Header("Monologue")]
    [Tooltip("독백 스크립트")]
    public GameObject monologuePanel;
    public static PlayerController Instance { get; private set; } // Singleton 인스턴스

    public void Reset()
    {
        interactCounter = 12;
        cookCount = 0;
        inventoryManager.ShowCurrentRecipe(ReturnRecipeType());
    }

    public Recipe ReturnRecipeType()
    {
        switch (dayCount)
        {
            case 1:
                return RecipeManager.instance.skewers;
            case 2:
                return RecipeManager.instance.steamedFish;
            case 3:
                return RecipeManager.instance.mushroomSoup;
            case 4:
                return RecipeManager.instance.jjaggle;
            default:
                return RecipeManager.instance.skewers;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    public IPlayerState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IPlayerState _idleState, _walkState, _waitState, _runState, _monoState, _seeState;


    private void Start()
    {
        if (_uiController == null)
        {
            _uiController = FindFirstObjectByType<UIController>();
        }
        if (inventoryManager == null)
        {
            inventoryManager = GetComponent<InventoryManager>();
        }

        DirectionUtils.Initialize(this); // 플레이어 Direction 체크하는 함수 초기화

        _waitState = gameObject.AddComponent<PlayerWaitState>();
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        _runState = gameObject.AddComponent<PlayerRunningState>();
        _monoState = gameObject.AddComponent<PlayerMonoState>();
        _seeState = gameObject.AddComponent<PlayerSeeState>();

        _rigidbody = GetComponent<Rigidbody2D>();

        ChangeState(_idleState);

        Reset();
    }


    private void Update()
    {
        UpdateState();
        if (interactCounter == 0)
        {
            CurrentState = _waitState;
            interactCounter = 12;
            switch (dayCount)
            {
                case 1:
                    inventoryManager.CreateRecipeSlots(RecipeManager.instance.skewers);
                    break;
                case 2:
                    inventoryManager.CreateRecipeSlots(RecipeManager.instance.steamedFish);
                    break;
                case 3:
                    inventoryManager.CreateRecipeSlots(RecipeManager.instance.mushroomSoup);
                    break;
                case 4:
                    inventoryManager.CreateRecipeSlots(RecipeManager.instance.jjaggle);
                    break;
            }
            if (_uiController.inventory.activeSelf)
            {
                _uiController.inventory.SetActive(false);
            }
        }
    }

    public void ChangeState(IPlayerState playerState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = playerState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
        CurrentDirection = new(anim.GetFloat("DirX"), anim.GetFloat("DirY"));
    }

    public void Interact()
    {
        Vector2 centerPosition = new(0.0f, 0.0f);
        // X축으로 이동중일 때에 이동하는 방향에 따라 왼쪽, 오른쪽 영역 적용
        if (CurrentDirection.x != 0)
        {
            Vector2 location = new(CurrentDirection.x, 0.0f);
            interactionAreaSize = new(0.4f, 0.3f);
            centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        }
        //Y축으로 이동중일 때에 이동하는 방향에 따라 위, 아래 영역 적용
        else if (CurrentDirection.y != 0)
        {
            Vector2 location = new(0.0f, CurrentDirection.y);
            interactionAreaSize = new(0.5f, 0.3f);
            centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        }


        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(centerPosition, interactionAreaSize, 0f, interactableLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("상호작용 대상: " + hitCollider.gameObject.name);
                // 가장 가까운 하나의 오브젝트와만 상호작용하려면 여기서 break;
                break;
            }
        }
    }


    // 디버그를 위한 기즈모 그리기 (에디터에서만 표시됨)
    private void OnDrawGizmosSelected()
    {
        Vector2 location = new(0.0f, 1f);
        Gizmos.color = Color.yellow;
        Vector2 centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
        centerPosition = (Vector2)transform.position + (-1) * location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
        location = new(anim.GetFloat("DirX"), 0.0f);
        centerPosition = (Vector2)transform.position + (-1) * location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
        centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
    }
}
