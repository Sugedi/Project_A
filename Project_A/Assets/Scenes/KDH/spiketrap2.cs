using UnityEngine;

public class spiketrap2 : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 2f; // Adjust the cooldown duration as needed

    private float lastDamageTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {
            // Player has entered the spike trap area and cooldown has passed
            DealDamageToPlayer(other.gameObject);
            lastDamageTime = Time.time; // Update the last damage time
        }
    }

    private void DealDamageToPlayer(GameObject player)
    {
        // Add your damage logic here
        // For now, just log a message indicating the damage
        Debug.Log("Player took " + damageAmount + " damage.");

        // If you have a HealthController script, you can uncomment the following lines
        /*
        HealthController healthController = player.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(damageAmount);
        }
        */
    }
}
