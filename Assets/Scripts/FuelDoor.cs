using UnityEngine;

public class FuelDoor : MonoBehaviour
{
    [SerializeField] int _flowerAmount = 1, _berryAmount = 1;

    [SerializeField] private Sprite _nextsprite;
    [SerializeField] private Sprite _nextTopSprite;
    [SerializeField] private SpriteRenderer _topRenderer;
    [SerializeField] private ItemInventory _inventory;

    private SpriteRenderer _renderer;
    private PlayerInput _playerInput;

    private bool _isActive, _isFueled;

    private void Awake()
    {
        _playerInput = new();
        _renderer = GetComponent<SpriteRenderer>();

        _playerInput.player.interact.performed += ctx => Fuel();
    }

    private void Fuel()
    {
        if (!_isActive || _isFueled) return;

        if (_inventory.GlowFlowers >= _flowerAmount && _inventory.GlowBerries >= _berryAmount)
        {
            _renderer.sprite = _nextsprite;
            _topRenderer.sprite = _nextTopSprite;
            _isFueled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isActive = false;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}