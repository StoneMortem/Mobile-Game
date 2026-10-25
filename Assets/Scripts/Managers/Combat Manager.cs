using Unity.VisualScripting;
//using UnityEditor.VisionOS;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    private PlayerScript player;
    private PlayerMovement playerMovement;
    private EnemyScript currentEnemy;

    // Bool
    bool playerDeath = false;

    // Prefabs
    [SerializeField] private GameObject freezePrefab;
    [SerializeField] private GameObject smokePrefab;

    // UI
    [SerializeField] private TextMeshProUGUI completeText;

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

        SpellController.Instance.ActivateSpellButtons(enemy);

        StartCoroutine(CombatLoop());
    }

    public IEnumerator CombatLoop()
    {
        bool bothAlive = true;

        while (bothAlive)
        {
            yield return StartCoroutine(player.ChooseAttack(currentEnemy));

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
        if (!playerDeath)
        {
            playerMovement.ExitCombat();
            SpellController.Instance.DisableSpellButtons();
        } else
        {
            DungeonCompleteScript.Instance.DungeonFailed();

        }

        currentEnemy = null;
    }

    private bool CheckHealth()
    {
        if (currentEnemy.GetHealth() > 0 && player.GetHealth() > 0)
        {
            return true;
        } else
        {
            if (player.GetHealth() < 0)
                playerDeath = true;

            return false;
        }
    }






    // CONDITIONS

    public void FreezeEnemy()
    {
        Vector3 spawnPosition = currentEnemy.transform.position;

        GameObject spawnedFreeze = Instantiate(freezePrefab, spawnPosition, Quaternion.identity);
        FreezeScript freezeScript = spawnedFreeze.GetComponent<FreezeScript>();

        freezeScript.Freeze(currentEnemy);
    }

    public void HidePlayer()
    {
        Vector3 spawnPosition = player.transform.position;

        GameObject spawnedSmoke = Instantiate(smokePrefab, spawnPosition, Quaternion.identity);
        SmokeScript smokeScript = spawnedSmoke.GetComponent<SmokeScript>();

        smokeScript.Hide(player);
    }
}
