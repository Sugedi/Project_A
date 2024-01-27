using UnityEngine;

public class ThreadController : MonoBehaviour
{
    public float ThreadMoveRange = 5f;
    public float ThreadMoveSpeed = 3f;
    public float ThreadObtainRange = 1f;
    private bool isFollowingPlayer = false;

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < ThreadMoveRange)
        {
            isFollowingPlayer = true;
            FollowPlayer(player.transform);
        }
        else
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer && distanceToPlayer < ThreadObtainRange)
        {
            ObtainThread();
        }
    }

    private void FollowPlayer(Transform playerTransform)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, ThreadMoveSpeed * Time.deltaTime);
    }

    private void ObtainThread()
    {
        Debug.Log("You've got a Heart!");
        Destroy(gameObject);
    }
}