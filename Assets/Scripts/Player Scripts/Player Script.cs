using UnityEngine;
using System.Collections;
using System;
public class PlayerScript : MonoBehaviour
{
    private float HP = 100;
    private float dmg = 2;
    public int gold = 0;
    private SpriteRenderer Spr;
    GameObject currentEnemy = null;
    EnemyScript ES;



    public bool TEMPATK = true;

    void Start()
    {
        Spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null)
        {
            try
            {
                currentEnemy = GameObject.FindWithTag("Enemy");
                ES = currentEnemy.GetComponent<EnemyScript>();
            }
            catch 
            { //Debug.Log("No Enemy to target");
            }
            
        }
        if (TEMPATK && currentEnemy != null && ES.health >= 0) { StartCoroutine(TEMPAUTOATTACK()); }
        
    }

    //PLACEHOLDER METHOD
    public IEnumerator TEMPAUTOATTACK()
    {
        
        TEMPATK = false;
        ES.Dmg(dmg);
        StartCoroutine(Attacking());
        yield return new WaitForSeconds(.5f);
        TEMPATK = true;
    }

    public IEnumerator Attacking()
    {
        Debug.Log("Attacking " + currentEnemy.name + " for " + dmg + " damage.");
        Spr.color = Color.yellow;
        yield return new WaitForSeconds(.1f);
        Spr.color = Color.white;
    }

    public IEnumerator TakeDamage(float damage)
    {
        Debug.Log("Ow! " + currentEnemy.name + " dealt " + damage + "!");
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
