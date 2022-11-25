using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    public float distance;

    private void Update()
    {
        Heartbeat();
    }

    private void Heartbeat()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius);

        if (hit != null && hit.transform.TryGetComponent<Attack>(out Attack npc))
        {
            distance = Vector3.Magnitude(npc.transform.position - npc.transform.position);
        }
    }
}
