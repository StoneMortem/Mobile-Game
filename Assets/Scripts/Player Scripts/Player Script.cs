using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Metrics;
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private float HP = 100;
    private float maxHP = 100;
    private float dmg = 2;

    public int lvl = 1;
    public int kills = 0;
    private float dodge = 2;
    public int goldAmt = 0;
    private SpriteRenderer Spr;
    EnemyScript ES;
    private Animator animator;
    private float smokeTime = 0f;

    //[SerializeField] private TextMeshProUGUI coinsText;
    public TextMeshProUGUI coinsText;
    CoinsCounter CC;

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
        coinsText.text = "";
        //CoinsCounter.coinsCount = 0;
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

        /* if (coinsText != null && !string.IsNullOrEmpty(coinsText.text)) 
         {
             Debug.Log("amt of gold: " + goldAmt);
             // Debug.Log("if coinsText not null");
             coinsText.text = goldAmt.ToString();
         }*/

        //most recent correct 
        if (goldAmt.ToString() != null)
        {
            Debug.Log("amt of gold: " + goldAmt);
            // Debug.Log("if coinsText not null");
            //coinsText.SetText($"(goldAmt.ToString())");
            //coinsText.text = goldAmt.ToString();
            CoinsCounter.coinsCount = goldAmt;
            UpdateCoinsCounter();
           /* if(coinsText.text != null)
            {
                coinsText.text = "" + goldAmt;
            }*/
           // else { coinsText.text += goldAmt.ToString(); }
        }
        else { Debug.Log("else"); }

        //UpdateCoins();
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





    

        

    public void GameOver()
    {
        Destroy(gameObject);
        //and probably add ui 
    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
            Debug.Log("player collider");
            goldAmt = lvl * 2;
            Destroy(gameObject);
        
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        { 
            Debug.Log("player collider");
            goldAmt = lvl * 2;
            Destroy(collision.gameObject);

           // if (coinsText != null) { coinsText.text = goldAmt.ToString(); }
        }
    }

    public void UpdateCoinsCounter()
    {
        if (coinsText != null)
            {
                Debug.Log("update coin if");
                coinsText.text = "Coins: " + CoinsCounter.coinsCount; 
            }
        else 
            {
                Debug.Log("update coin else");
                if (coinsText == null) { Debug.Log("coinsText is MISSING"); }
                if (CoinsCounter.coinsCount.ToString() == null) { Debug.Log("coinsCount is missing"); }
                if (coinsText.text == null) { Debug.Log("coinsText text is MISSING"); }
            coinsText.text = CoinsCounter.coinsCount.ToString();  
            }

        
        //if (coinsText.text != goldAmt.ToString()) { Debug.Log("Update coins"); coinsText.text = goldAmt.ToString(); }
        //else { Debug.Log("Coins text is null"); }
    }
}
