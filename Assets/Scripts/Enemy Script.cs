using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class EnemyScript : MonoBehaviour
{
    public float atkInterval;
    public float health;
    public int level;
    public float dmg;
    public bool canAtk = true;

    //ADDING HANDLING FOR MULTIPLE ATTACKS LATER ON

    private Animator anim;

    GameObject Player;
    PlayerScript PS;
    public SpriteRenderer sr;
    

    //later, we will probably have an ENUM dictating which enemy type spawns (and any particular stats unique to them)
    //but for now, I just want to create a generic enemy




    private void Awake()
    {
        //Placeholder stat assignment
        atkInterval = 1.5f;
        health = 50;
        level = 1;
        dmg = level * 2;
        

    }

    private void Start()
    {
        {Player = GameObject.FindGameObjectWithTag("Player");
        PS = Player.GetComponent<PlayerScript>();
        sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
    }


    void Update()
    {
        if (canAtk)
        {
            Debug.Log("Attacking Player");
            Atk();
            
        }
        
    }
    private void Atk()
    {
        anim.Play("Attack");
        PS.TakeDamage(dmg);
        StartCoroutine(AtkCooldown());
    }
    public void Dmg(float damage)
    {
        Debug.Log("Enemy took " + damage + ". Remaining health: " + health);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.Play("Hurt");
        }
        
        StartCoroutine(ColorShift());
        health -= damage;

       
        if (health <= 0)
        {
            canAtk = false;
            Die();

        }
        else {
            
        }

    }
    public IEnumerator ColorShift()
    {//turns red on taking damage
     //turns back to white after .2 seconds if it doesn't die.
        sr.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
    }


    private void Die()
    {
        anim.Play("Death");
        PS.gold += level * 2;
        Debug.Log("BLEHH!!! Player gained " + level*2 + "gold!");
        canAtk = false;
        Destroy(gameObject,.5f);
        
        
    }

    public IEnumerator AtkCooldown()
    {
        canAtk = false;
        yield return new WaitForSeconds(atkInterval);
        canAtk = true;
    }
}
