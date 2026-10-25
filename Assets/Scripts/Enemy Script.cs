using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float atkInterval;
    public float health;
    private float maxHealth = 10;
    public int level;
    public float dmg;
    public bool canAtk = true;
    private bool frozen = false;
    private float frozenTime = 0;

    // UI
    [SerializeField] TMP_Text damageText;
    private Slider healthBar;

    //ADDING HANDLING FOR MULTIPLE ATTACKS LATER ON

    private Animator anim;

    GameObject Player;
    PlayerScript PS;
    public SpriteRenderer sr;


    //later, we will probably have an ENUM dictating which enemy type spawns (and any particular stats unique to them)
    //but for now, I just want to create a generic enemy

    // Getters
    public float GetHealth() { return health; }


    private void Awake()
    {
        //Placeholder stat assignment
        atkInterval = 1.5f;
        maxHealth = 10;
        health = maxHealth;
        level = 1;
        dmg = level * 2;

        // UI
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void Start()
    {
        {Player = GameObject.FindGameObjectWithTag("Player");
        PS = Player.GetComponent<PlayerScript>();
        sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
    }


    //void Update()
    //{
    //    if (canAtk)
    //    {
    //        Debug.Log("Attacking Player");
    //        Atk();
            
    //    }
        
    //}

    void Update()
    {
        if (frozen)
        {
            frozenTime -= Time.deltaTime;

            if (frozenTime <= 0f)
            {
                frozen = false;
            }
        }
    }
    public IEnumerator Atk()
    {
        if (!frozen)
        {
            Debug.Log("Enemy attack started");

            anim.Play("Attack");

            yield return null;
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length);

            // anim.Play("Idle");

            StartCoroutine(PS.TakeDamage(dmg));
            yield return new WaitForSeconds(.5f);
            //StartCoroutine(AtkCooldown());
        }
    }
    public IEnumerator Dmg(float damage)
    {
        damageText.text = $"{damage}";

        Debug.Log("Enemy took " + damage + ". Remaining health: " + health);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.Play("Hurt");
        }
        
        StartCoroutine(ColorShift());
        health -= damage;
        healthBar.value = health;

        if (health <= 0)
        {
            canAtk = false;
            Die();

        }
        else {
            yield return new WaitForSeconds(.5f);
            damageText.text = "";
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

    public void Freeze(float frozenTime)
    {
        Debug.Log("FROZEN: " + gameObject.name + " for " + frozenTime + " seconds");

        this.frozenTime = frozenTime;

        frozen = true;
    }
}
