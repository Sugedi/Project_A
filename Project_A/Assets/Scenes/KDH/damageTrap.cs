using UnityEngine;

public class damageTrap : MonoBehaviour
{
    public int damageAmount = 10;
    public float knockbackAmount = 10f;
    public float constantDamageInterval = 1f;

    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the spike trap area
            DealDamageToPlayer(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the spike trap area
            isPlayerInside = false;
        }
    }

    private void DealDamageToPlayer(GameObject player)
    {
        // Add your damage logic here
        // For now, just log a message indicating the damage
        Debug.Log("Player took " + damageAmount + " damage.");
    }

}
