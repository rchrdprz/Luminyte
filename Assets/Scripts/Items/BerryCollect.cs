using UnityEngine;

public class BerryCollect : ItemCollect
{
    private PlayerInput _playerInput;
    private bool _isActive, _isCollected;

    [SerializeField] private SpriteRenderer _topRenderer;
    [SerializeField] private GameObject _showInteract;

    private void Awake()
    {
        _playerInput = new();
        _playerInput.player.interact.performed += ctx => Collect();

        _showInteract.SetActive(false);
    }

    public override void Collect()
    {
        if (!_isActive || _isCollected) return;

        _topRenderer.sprite = _nextSprite;
        _topRenderer.material = _material;
        _showInteract.SetActive(false);
        _inventory.BranchBerries++;
        _isCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isActive = true;
        if (!_isCollected) _showInteract.SetActive(true);
    } 

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isActive = false;
        _showInteract.SetActive(false);
    } 

    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}