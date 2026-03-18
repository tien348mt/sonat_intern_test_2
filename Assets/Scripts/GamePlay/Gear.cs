using UnityEngine;

public class Gear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Block block = other.GetComponent<Block>();

        if (block != null)
        {
            block.DestroyByGear();
        }
    }
}