using UnityEngine;
using System.Collections;

public class FreezeScript : MonoBehaviour
{
    private float freezeTimer = 5f; // Determined by upgrades later
    private float cooldown = 20f; // Determined by upgrades later

    private void Update()
    {
        freezeTimer -= Time.deltaTime;

        if (freezeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Freeze(EnemyScript enemy)
    {
        enemy.Freeze(freezeTimer);
        Animator freezeAnimator = GetComponent<Animator>();

        if (freezeAnimator == null)
        {
            Debug.LogError("No Animator found on the Freeze prefab!");
            return;
        }

        Debug.Log("Playing Freeze animation on " + gameObject.name);

        freezeAnimator.Play("Freeze");
    }
}
