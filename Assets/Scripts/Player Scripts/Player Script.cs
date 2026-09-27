using UnityEngine;
using System.Collections;
using System;
public class PlayerScript : MonoBehaviour
{
    private float HP = 100;
    private float dmg = 2;
    public int gold = 0;
    private SpriteRenderer Spr;
    EnemyScript ES;
    private Animator animator;



    public bool TEMPATK = true;

    //Getters
    public float GetHealth() { return HP; }

    void Start()
    {
        Spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (currentEnemy == null)
    //    {
    //        try
    //        {
    //            currentEnemy = GameObject.FindWithTag("Enemy");
    //            ES = currentEnemy.GetComponent<EnemyScript>();
    //        }
    //        catch 
    //        { //Debug.Log("No Enemy to target");
    //        }
            
    //    }
    //    if (TEMPATK && currentEnemy != null && ES.health >= 0) { StartCoroutine(TEMPAUTOATTACK()); }
        
    //}

    //PLACEHOLDER METHOD
    public IEnumerator TEMPAUTOATTACK(EnemyScript enemy)
    {
        
        TEMPATK = false;

        Debug.Log("Player attack: damaging enemy");

        enemy.Dmg(dmg);

        Debug.Log("Player attack: playing animation");

        animator.Play("Player_Attack_Up");

        // Wait for animation to finish
        yield return null;

        Debug.Log("Player attack: waiting for animation");

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        Debug.Log("Current animation: " + state.fullPathHash);
        Debug.Log("Animation length: " + state.length);

        yield return new WaitForSeconds(state.length);

        Debug.Log("Player attack: animation finished");

        animator.Play("Player_Idle");

        yield return StartCoroutine(Attacking());

        // yield return new WaitForSeconds(.5f);
        TEMPATK = true;

        Debug.Log("Player attack: coroutine finished");
    }

    public IEnumerator Attacking()
    {
        //Debug.Log("Attacking " + currentEnemy.name + " for " + dmg + " damage.");
        Spr.color = Color.yellow;
        yield return new WaitForSeconds(.1f);
        Spr.color = Color.white;
    }

    public IEnumerator TakeDamage(float damage)
    {
        //Debug.Log("Ow! " + currentEnemy.name + " dealt " + damage + "!");
        HP -= damage;
        Spr.color = Color.red;
        if (HP > 0)
        {
            GameOver();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            Spr.color = Color.white;
        }
    }
    

        

    public void GameOver()
    {
        Destroy(gameObject);
        //and probably add ui 
    }

    public void SpawnNewEnemy()
    {
        //placeholder code, this may be put in another script
    }
}
