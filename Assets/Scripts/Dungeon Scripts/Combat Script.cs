using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.EnterCombat();
            }

            // Get enemy in room and make it start combat as well
        }
    }
}
