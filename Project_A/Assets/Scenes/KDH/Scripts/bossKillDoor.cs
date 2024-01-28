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
    }

}