using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float time = 0.1f;
    [SerializeField] private float range = 0.1f;
    [SerializeField] Transform target;

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > range)
        {
            Vector3 targetPos = new(target.position.x, target.position.y, -10f);
            Vector3 position = new(transform.position.x, transform.position.y, -10f);

            transform.position = Vector3.Lerp(position, targetPos, time);
        }
    }
}
