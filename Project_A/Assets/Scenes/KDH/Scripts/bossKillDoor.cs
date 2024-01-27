using UnityEngine;

public class BossKillDoor : MonoBehaviour
{
    public AnimationClip doorOpenAnimation;

    private bool hasOpened = false;  // To track whether the door has been opened

    void Update()
    {
        // Check if there are no colliders with the tag 'enemy' using the door's own BoxCollider
        bool noMonsters = CheckNoMonsters();

        // Play the door opening animation if the door hasn't been opened yet and there are no monsters
        if (!hasOpened && noMonsters)
        {
            PlayDoorAnimation(doorOpenAnimation);
            hasOpened = true;  // Mark the door as opened
        }
    }

    bool CheckNoMonsters()
    {
        // Assuming the door has a BoxCollider
        BoxCollider doorCollider = GetComponent<BoxCollider>();

        if (doorCollider != null)
        {
            Collider[] colliders = Physics.OverlapBox(doorCollider.bounds.center, doorCollider.bounds.extents, Quaternion.identity);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("enemy"))
                {
                    return false;  // There is a collider tagged 'enemy'
                }
            }
        }

        return true;  // No colliders tagged 'enemy' found in the door's own BoxCollider
    }

    void PlayDoorAnimation(AnimationClip animationClip)
    {
        // Set the specified animation clip to the Animation component
        GetComponent<Animation>().clip = animationClip;

        // Play the animation once
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();
    }
}
