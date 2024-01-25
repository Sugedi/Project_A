using UnityEngine;

public class HeartController : MonoBehaviour
{
    public float heartMoveRange = 5f;
    public float heartMoveSpeed = 3f;
    public float heartObtainRange = 1f;
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

        if (distanceToPlayer < heartMoveRange)
        {
            isFollowingPlayer = true;
            FollowPlayer(player.transform);
        }
        else
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer && distanceToPlayer < heartObtainRange)
        {
            ObtainHeart();
        }
    }

    private void FollowPlayer(Transform playerTransform)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, heartMoveSpeed * Time.deltaTime);
    }

    private void ObtainHeart()
    {
        Debug.Log("You've got a Heart!");
        Destroy(gameObject);
    }
}