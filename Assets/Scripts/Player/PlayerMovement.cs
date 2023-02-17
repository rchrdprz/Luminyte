using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody2D _rb;
    private PlayerInput _action;
    private Vector2 _input, _mousePos;

    [SerializeField] private float _speed = 6f;

    // -- Events -- //
    public event Move OnMovement;
    public delegate void Move(float input);

    public event Rotate OnRotate;
    public delegate void Rotate(Vector2Int direction);

    void Awake()
    {
        _camera = Camera.main;
        _action = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _input = _action.player.move.ReadValue<Vector2>();
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        Rotation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 direction = new(_input.x, _input.y);
        _rb.MovePosition(_rb.position + _speed * Time.fixedDeltaTime * direction);

        OnMovement?.Invoke(direction.sqrMagnitude);
    }

    private void Rotation()
    {
        Vector2 direction = _mousePos - _rb.position;
        float angle = Vector2.SignedAngle(transform.up, direction);

        if (angle < 45 && angle > - 45)
        {   // up //
            OnRotate?.Invoke(new(0, 1));
        }
        else if (angle > 135 || angle < -135)
        {   // down //
            OnRotate?.Invoke(new(0, -1));
        }
        else if (angle < -45 && angle > -135)
        {   // right //
            OnRotate?.Invoke(new(1, 0));
            transform.eulerAngles = new(0, 0f, 0);
        }
        else
        {   // left //
            OnRotate?.Invoke(new (-1, 0));
            transform.eulerAngles = new(0, 180f, 0);
        }
    }

    private void OnEnable() => _action.Enable();

    private void OnDisable() => _action.Disable();
}