using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour
{
    private int damage = 10; // Determined by upgrades later
    private float cooldown = 20f; // Determined by upgrades later
    public IEnumerator Fireball(EnemyScript enemy)
    {
        Animator fireballAnimator = GetComponent<Animator>();
        fireballAnimator.Play("Fireball");
        AnimatorStateInfo state = fireballAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        StartCoroutine(enemy.Dmg(damage));

        Destroy(gameObject);
    }
}
