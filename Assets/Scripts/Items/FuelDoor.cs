using UnityEngine;

public class FuelDoor : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private ItemInventory _inventory;
    [SerializeField] private EndGame _gameWon;

    [Header("Prefab References")]
    [SerializeField] private Sprite _nextsprite;
    [SerializeField] private Sprite _nextBottomSprite;
    [SerializeField] private SpriteRenderer _bottomRenderer;

    [HideInInspector] public bool IsFueled;
    [HideInInspector] public int BerryAmount;
    [HideInInspector] public int FlowerAmount;

    private SpriteRenderer _renderer;
    private PlayerInput _playerInput;

    private bool _isActive;

    public event Fueled OnFueled;
    public delegate void Fueled();

    private void Awake()
    {
        _playerInput = new();
        _renderer = GetComponent<SpriteRenderer>();

        _playerInput.player.interact.performed += ctx => Fuel();

        BerryAmount = _inventory.BerryAmount;
        FlowerAmount = _inventory.FlowerAmount;
    }

    private void Fuel()
    {
        if (!_isActive) return;

        if (!IsFueled)
        {
            if (_inventory.GlowFlowers >= FlowerAmount && _inventory.BranchBerries >= BerryAmount)
            {
                _renderer.sprite = _nextsprite;
                _bottomRenderer.sprite = _nextBottomSprite;
                IsFueled = true;

                OnFueled?.Invoke();
            }
        }
        else _gameWon.End();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent<PlayerMovement>(out _)) return;
        _isActive = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.transform.TryGetComponent<PlayerMovement>(out _)) return;
        _isActive = false; 
    }

    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}