using UnityEngine;

public class BerryCollect : ItemCollect
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<PlayerMovement>(out _))
            Collect();
    }

    public override void Collect()
    {
        base.Collect();

        _inventory.GlowBerries++;
    }
}