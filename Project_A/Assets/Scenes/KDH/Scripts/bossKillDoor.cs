using System.Collections;
using UnityEngine;

public class bossKillDoor : MonoBehaviour
{
    public AnimationClip bossDoorOpenAnimation;
    // ...

    public void BossKillDoorOpen()
    {
        // Check if the doorAnimation is assigned
        if (bossDoorOpenAnimation != null)
        {
            // Play the 'bosskilldooropen' animation
            bossPlayDoorAnimation(bossDoorOpenAnimation);
            SoundManager.instance.PlayAudio("Door2", "SE");

        }
        else
        {
            Debug.LogError("Door Animation not assigned to BossKillDoor script.");
        }
    }
    void bossPlayDoorAnimation(AnimationClip animationClip)
    {
        // Set the specified animation clip to the Animation component
        GetComponent<Animation>().clip = animationClip;

        // Play the animation once
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();
        // Add self-disable method call
        StartCoroutine(DisableAfterAnimation(animationClip.length));
    }

    IEnumerator DisableAfterAnimation(float duration)
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(duration);

        // Disable the script or GameObject
        DisableScript();
    }

    void DisableScript()
    {
        // You can either disable the script or the GameObject itself
        // For disabling the script:
        // enabled = false;

        // For disabling the GameObject:
        gameObject.SetActive(false);
    }
}