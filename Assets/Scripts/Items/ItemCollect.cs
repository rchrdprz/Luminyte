using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] protected ItemInventory _inventory;

    [Header("Prefab References")]
    [SerializeField] protected Material _material;
    [SerializeField] protected Sprite _nextSprite;

    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public virtual void Collect() 
    {
        _renderer.sprite = _nextSprite;
        _renderer.material = _material;
        _collider.enabled = false;
    }
}