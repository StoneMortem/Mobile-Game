using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Metrics;
using Unity.VisualScripting;
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private float HP = 100;
    private float maxHP = 100;
    private float dmg = 2;

    public int lvl = 1;
    public int kills = 0;
    private float dodge = 2;
    public int goldAmt;
    private SpriteRenderer Spr;
    EnemyScript ES;
    private Animator animator;
    private float smokeTime = 0f;

    public TextMeshProUGUI coinsText;
    
    // Next attack
    private bool fireball = false;
    private bool smoke = false;

    // Spell Prefabs
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject smokePrefab;

    // UI
    [SerializeField] TMP_Text damageText;
    private Slider healthBar;


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

        damageText.text = " ";

        //PlayerPrefs
        maxHP = PlayerPrefs.GetInt("PlayerHP", 20);
        HP = maxHP;
        dmg = PlayerPrefs.GetInt("PlayerDmg", 2);
        dodge = PlayerPrefs.GetInt("Dodge", 2);

        // UI
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHP;
        healthBar.value = HP;
    }

    void Start()
    {
        Spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        damageText.text = " ";

        GameObject coinObject = GameObject.FindWithTag("CoinUI");

        if (coinObject != null)
        {
            coinsText = coinObject.GetComponentInChildren<TextMeshProUGUI>();
            goldAmt = PlayerPrefs.GetInt("Saved Gold", 0);
        }
        else
        {
            Debug.Log("Could not find coin ui tagged!");
        }
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
 
        if (goldAmt.ToString() != null)
        {
            UpdateCoinsCounter();
        }
        else { Debug.Log("else"); }
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

        StartCoroutine(enemy.Dmg(dmg));

        animator.Play("Player_Attack_Up");

        // Wait for animation to finish
        yield return null;
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        animator.Play("Player_Idle");

        yield return StartCoroutine(Attacking());

        TEMPATK = true;
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
            healthBar.value = HP;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Debug.Log("player collider");
            Destroy(collision.gameObject);
            goldAmt += lvl * 2;
            PlayerPrefs.SetInt("Saved Gold", goldAmt);
            PlayerPrefs.Save();
        }
    }

    public void UpdateCoinsCounter()
    {
        if (coinsText != null)
        {
            coinsText.text = goldAmt.ToString();
            PlayerPrefs.SetInt("Saved Gold", goldAmt);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("update coin else");
        }
    }

    public void GameOver()
    {
        Destroy(gameObject);
        //and probably add ui 
    }
}
