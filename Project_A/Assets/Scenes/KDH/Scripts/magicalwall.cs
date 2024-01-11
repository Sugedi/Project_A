using UnityEngine;

public class magicalwall : MonoBehaviour
{
    private void Start()
    {
        // Ignore collisions between the "Player" layer and the default layer (layer 0)
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), 1);
    }

    // Add your player movement and other logic here
}
