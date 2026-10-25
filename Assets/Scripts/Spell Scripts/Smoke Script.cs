using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    private float smokeTimer = 5f; // Determined by upgrades later

    void Awake()
    {
        smokeTimer = PlayerPrefs.GetInt("SreezeDuration", 3);
    }

    // Update is called once per frame
    void Update()
    {
        smokeTimer -= Time.deltaTime;

        if (smokeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Hide(PlayerScript player)
    {
        player.Hide(smokeTimer);
        Animator smokeAnimator = GetComponent<Animator>();

        if (smokeAnimator == null)
        {
            Debug.LogError("No Animator found on the Freeze prefab!");
            return;
        }

        Debug.Log("Playing Freeze animation on " + gameObject.name);

        smokeAnimator.Play("Smoke");
    }
}
