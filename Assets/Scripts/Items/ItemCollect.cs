using UnityEngine.UI;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] protected Sprite _nextSprite;
    [SerializeField] protected ItemInventory _inventory;
    [SerializeField] private Material _material;

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