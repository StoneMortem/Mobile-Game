using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonCompleteScript : MonoBehaviour
{
    public static DungeonCompleteScript Instance;
    private TextMeshProUGUI dungeonCompleteText;

    // SETTERS
    public void SetDungeonCompleteText(TextMeshProUGUI dungeonCompleteText) { this.dungeonCompleteText = dungeonCompleteText; }

    void Awake()
    {
        SetDungeonCompleteText(GameObject.Find("Dungeon Complete Text").GetComponent<TextMeshProUGUI>());
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        dungeonCompleteText.text = " ";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraScript.Instance.SetFollowPlayer(false);

            dungeonCompleteText.text = "Dungeon Completed!";

            int level = PlayerPrefs.GetInt("Level", 1);
            PlayerPrefs.SetInt("Level", level + 1);

            StartCoroutine(ToUpgradeScreen(4f));
        }
    }

    private IEnumerator ToUpgradeScreen(float linger)
    {
        yield return new WaitForSeconds(linger);

        SceneManager.LoadSceneAsync(3);
    }

    public void DungeonFailed()
    {
        // Player always gets at least a little gold even when failing
        int currentGold = PlayerPrefs.GetInt("Saved Gold");
        int level = PlayerPrefs.GetInt("Level");
        int coins = Random.Range(1, 4) * level;

        PlayerScript.Instance.FailedDungeonCoins(coins);

        dungeonCompleteText.text = "Dungeon Failed...";
        StartCoroutine(ToUpgradeScreen(4f));
    }

}
