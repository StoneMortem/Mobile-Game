using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DungeonCompleteScript : MonoBehaviour
{
    // Singleton
    public static DungeonCompleteScript Instance;

    private TextMeshProUGUI dungeonCompleteText;

    // SETTERS
    public void SetDungeonCompleteText(TextMeshProUGUI dungeonCompleteText) { this.dungeonCompleteText = dungeonCompleteText; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraScript.Instance.SetFollowPlayer(false);

            dungeonCompleteText.text = " ";

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
        dungeonCompleteText.text = "Dungeon Failed...";
        StartCoroutine(ToUpgradeScreen(4f));
    }

}
