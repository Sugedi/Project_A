using UnityEngine;

public class justReset : MonoBehaviour
{
    // Initial position of the puzzle object
    private Vector3 justInitialPosition;

    void Start()
    {
        // Store the initial position when the scene starts
        justInitialPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the reset trigger
        if (other.CompareTag("ResetTrigger"))
        {
            // Call the function to reset the puzzle object position
            justResetObjectPosition();
        }
    }

    void justResetObjectPosition()
    {
        // Reset the puzzle object's position to the initial position
        transform.position = justInitialPosition;
    }
}
