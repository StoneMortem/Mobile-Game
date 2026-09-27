using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private Transform enemy;

    // SETTERS
    public void SetEnemy(Transform enemy) { this.enemy = enemy; }

    // GETTERS
    public Transform GetEnemy() { return enemy; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null && enemy != null)
            {
                player.EnterCombat();
                // Get enemy in room and make it start combat as well
            }

            CombatManager.Instance.StartCombat(enemy);

            gameObject.SetActive(false);
        }
    }
}
