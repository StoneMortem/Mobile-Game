using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] TMP_Text nextLevelText;

    private void Awake()
    {
        int level = PlayerPrefs.GetInt("Level", 1);

        nextLevelText.text = $"Next Level: {level}";
    }
}
