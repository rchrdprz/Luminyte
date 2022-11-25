using UnityEngine;

public class BerryCollect : ItemCollect
{
    private PlayerInput _playerInput;

    private bool _isActive, _isCollected;

    [SerializeField] private SpriteRenderer _topRenderer;

    private void Awake()
    {
        _playerInput = new();

        _playerInput.player.interact.performed += ctx => Collect();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isActive = false;
    }

    public override void Collect()
    {
        if (!_isActive || _isCollected) return;

        _topRenderer.sprite = _nextSprite;
        _topRenderer.material = _material;
        _inventory.GlowBerries++;
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