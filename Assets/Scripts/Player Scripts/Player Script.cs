using UnityEngine;
using System.Collections;
using System;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private float HP = 100;
    private float dmg = 2;
    private float dodge = 2;
    public int gold = 0;
    private SpriteRenderer Spr;
    EnemyScript ES;
    private Animator animator;
    private float smokeTime = 0f;

    // Next attack
    private bool fireball = false;
    private bool smoke = false;

    // Spell Prefabs
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject smokePrefab;

    // UI
    [SerializeField] TMP_Text damageText;



    public bool TEMPATK = true;

    //Setters
    public void SetFireball(bool fire) { fireball = fire; }

    //Getters
    public float GetHealth() { return HP; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SpellController.Instance.SetPlayer(gameObject.transform);
        SpellController.Instance.SetPlayerScript(PlayerScript.Instance);

        damageText.text = "";

        HP = PlayerPrefs.GetInt("PlayerHP", 10);
        dmg = PlayerPrefs.GetInt("PlayerDmg", 2);
        dodge = PlayerPrefs.GetInt("Dodge", 2);
    }

    void Start()
    {
        Spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (smoke)
        {
            smokeTime -= Time.deltaTime;

            if (smokeTime <= 0f)
            {
                smoke = false;
            }
        }
    }

    //PLACEHOLDER METHOD
    public IEnumerator ChooseAttack(EnemyScript enemy)
    {
        if (fireball)
            yield return StartCoroutine(FireballAttack(enemy));
        else
            yield return StartCoroutine(TEMPAUTOATTACK(enemy));
    }

    public IEnumerator TEMPAUTOATTACK(EnemyScript enemy)
    {   
        TEMPATK = false;

        Debug.Log("Player attack: damaging enemy");

        StartCoroutine(enemy.Dmg(dmg));

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
        // Random chance to dodge here

        if (!smoke)
        {
            damageText.text = $"{damage}";

            //Debug.Log("Ow! " + currentEnemy.name + " dealt " + damage + "!");
            HP -= damage;
            Spr.color = Color.red;
            if (HP < 0)
            {
                GameOver();
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                Spr.color = Color.white;
                damageText.text = "";
            }
        } else
        {
            damageText.text = "0";
            yield return new WaitForSeconds(.5f);
            damageText.text = "";
        }
    }






    // spells
    IEnumerator FireballAttack(EnemyScript enemy)
    {
        fireball = false;

        Vector3 spawnPosition = enemy.transform.position;

        GameObject spawnedFireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        FireballScript fireballScript = spawnedFireball.GetComponent<FireballScript>();

        yield return StartCoroutine(fireballScript.Fireball(enemy));
    }

    public void Hide(float smokeTime)
    {
        smoke = true;

        this.smokeTime = smokeTime;
    }





    

        

    public void GameOver()
    {
        Destroy(gameObject);
        //and probably add ui 
    }
}
