using UnityEngine;
using static LeverController;

public class leverDoorOpen : MonoBehaviour
{
    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;

    public AnimationClip doorOpenAnimation;

    private bool hasOpened = false;  // To track whether the door has been opened

    void Update()
    {
        // Check if all levers have isPuzzleAnswer set to true
        bool allLeversActivated = CheckAllLevers();

        // Play the door opening animation if the puzzle answer is correct and the door hasn't been opened yet
        if (allLeversActivated && !hasOpened)
        {
            PlayDoorAnimation(doorOpenAnimation);
            hasOpened = true;  // Mark the door as opened
        }
    }

    bool CheckAllLevers()
    {
        bool lever1Activated = lever1.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever2Activated = lever2.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever3Activated = lever3.GetComponent<LeverController>().isPuzzleAnswer;
        bool lever4Activated = lever4.GetComponent<LeverController>().isPuzzleAnswer;

        return lever1Activated && lever2Activated && lever3Activated && lever4Activated;
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