using Unity.VisualScripting;
using UnityEditor.VisionOS;
using UnityEngine;
using System.Collections;


public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    private PlayerScript player;
    private PlayerMovement playerMovement;
    private EnemyScript currentEnemy;

    // Setters
    public void SetPlayer(PlayerScript player) { this.player = player; }
    public void SetPlayerMovement(PlayerMovement player) { playerMovement = player; }

    private void OnDisable()
    {
        Debug.Log("!!! COMBAT MANAGER DISABLED !!!");
    }

    private void OnDestroy()
    {
        Debug.Log("!!! COMBAT MANAGER DESTROYED !!!");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        currentEnemy = null;
    }

    public void StartCombat(Transform enemy)
    {
        currentEnemy = enemy.GetComponent<EnemyScript>();

        StartCoroutine(CombatLoop());
    }

    public IEnumerator CombatLoop()
    {
        bool bothAlive = true;

        while (bothAlive)
        {
            yield return StartCoroutine(player.TEMPAUTOATTACK(currentEnemy));

            yield return new WaitForSeconds(1f);

            if (!CheckHealth())
            {
                bothAlive = false;
                break;
            }

            yield return StartCoroutine(currentEnemy.Atk());

            yield return new WaitForSeconds(1f);

            if (!CheckHealth())
            {
                bothAlive = false;
                break;
            }
        }

        EndCombat();
    }

    public void EndCombat()
    {
        playerMovement.ExitCombat();

        currentEnemy = null;
    }

    private bool CheckHealth()
    {
        if (currentEnemy.GetHealth() > 0 && player.GetHealth() > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
