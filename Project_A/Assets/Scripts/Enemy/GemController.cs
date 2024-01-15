using UnityEngine;

public class GemController : MonoBehaviour
{
    public float gemMoveRange = 5f;
    public float gemMoveSpeed = 3f;
    public float gemObtainRange = 1f;
    private bool isFollowingPlayer = false;

    private void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < gemMoveRange)
        {
            isFollowingPlayer = true;
            FollowPlayer(player.transform);
        }
        else
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer && distanceToPlayer < gemObtainRange)
        {
            ObtainGem();
        }
    }

    private void FollowPlayer(Transform playerTransform)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, gemMoveSpeed * Time.deltaTime);
    }

    private void ObtainGem()
    {
        Debug.Log("You've got a gem!");
        Destroy(gameObject);
    }
}
