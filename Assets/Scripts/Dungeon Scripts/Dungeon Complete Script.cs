using System.Collections;
using UnityEngine;

public class DungeonCompleteScript : MonoBehaviour
{
    private GameObject dungeonCompleteCanvas;

    // SETTERS
    public void SetDungeonCompleteCanvas(GameObject dungeonCompleteCanvas) { this.dungeonCompleteCanvas = dungeonCompleteCanvas; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraScript.Instance.SetFollowPlayer(false);

            dungeonCompleteCanvas.SetActive(true);

            StartCoroutine(ToUpgradeScreen());
        }
    }

    private IEnumerator ToUpgradeScreen()
    {
        yield return new WaitForSeconds(3f);
        // change scenes
    }

}
