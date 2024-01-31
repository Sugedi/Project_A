using UnityEngine;

public class spiketrap2 : MonoBehaviour
{  
    int damageAmount = 30;
    float damageCooldown = 0.5f; // Adjust the cooldown duration as needed

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

        // Get the player's Player script
        Player playerScript = player.GetComponent<Player>();

        if (playerScript != null && !playerScript.isDead && !playerScript.isDamage)
        {
            // Apply damage to the player using the player's script
            playerScript.health -= damageAmount;

            

            if (playerScript.health < 0)
            {
                playerScript.health = 0;
                playerScript.Die();
            }

            // Start the damage coroutine
            playerScript.StartCoroutine(playerScript.OnDamage());
        }       
    }
}
