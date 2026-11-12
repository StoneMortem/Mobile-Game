using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
public class PlayerScript : MonoBehaviour
{

    //Stat notes:
    //HP = 20 + (10 * HPLevel)
    //DmgMultiplier = 1 + (0.25 * DmgLevel)
    //Dmg (basic attack) = DmgMult * (2 * SpearLevel)
    //Dmg (general) = DmgMult * (Base Dmg)
    //Crit Chance = CritLevel%
    //Dodge = 2 * (DodgeLevel)

    //Skill stats: Undecided
    public static PlayerScript Instance;

    //[SerializeField] private TMP_Text coinCounter;
    public TextMeshProUGUI coinsText;

    #region PlayerStats
    private float HP = 20;
    private float maxHP = 20;
    private float dmgMult = 1;

    public int lvl = 1;
    public int kills = 0;
    private float dodge = 2;
    public int gold = 0;
    public int hpLvl = 1;
    public int dmgLvl = 1;
    public int critLvl = 1;
    public int dodgeLvl = 1;

    public int fb_dmgLvl = 1;
    public int fb_cdLvl = 1;
    public int fz_durLvl = 1;
    public int fz_cdLvl = 1;
    public int s_durLvl = 1;
    public int s_cdLvl = 1;

    public int spearLvl = 1;
    public int shieldLvl = 1;
    public int potionLvl = 1;

    #endregion
    private SpriteRenderer Spr;
    EnemyScript ES;
    private Animator animator;
    private float smokeTime = 0f;

    public int goldAmt;

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




        //PlayerPrefs
        maxHP = PlayerPrefs.GetInt("PlayerHP", 20);
        HP = maxHP;
        dmgMult = PlayerPrefs.GetInt("PlayerDmg", 1);
        dodge = PlayerPrefs.GetInt("Dodge", 2);
        InitStats();

        // UI
        if (SceneManager.GetActiveScene().name != "Upgrades, Shop & Coins")
        {
            SpellController.Instance.SetPlayer(gameObject.transform);
            SpellController.Instance.SetPlayerScript(PlayerScript.Instance);
            healthBar = GetComponentInChildren<Slider>();
            healthBar.maxValue = maxHP;
            healthBar.value = HP;
        }

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Upgrades, Shop & Coins")
        {
            damageText = GetComponentInChildren<TMP_Text>();
            damageText.text = " ";
            Spr = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            damageText.text = " ";
        }

        /*if(coinCounter == null)
        {
            coinCounter = GameObject.Find("Coin Amount").GetComponent<TMP_Text>();
        }*/

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
    public void InitStats()
    {
        gold = PlayerPrefs.GetInt("Gold");
        maxHP = PlayerPrefs.GetInt("PlayerHP", 20);
        HP = PlayerPrefs.GetInt("PlayerHP", 20);
        dmgMult = PlayerPrefs.GetInt("PlayerDmg", 1);
        dodge = PlayerPrefs.GetInt("Dodge", 2);
        lvl = PlayerPrefs.GetInt("Level", 1);
        kills = PlayerPrefs.GetInt("Kills", 0);

        hpLvl = PlayerPrefs.GetInt("HPLevel", 1);
        dmgLvl = PlayerPrefs.GetInt("DmgLevel", 1);
        critLvl = PlayerPrefs.GetInt("CritLevel", 1);
        fb_dmgLvl = PlayerPrefs.GetInt("FB_DamageLevel", 1);
        fb_cdLvl = PlayerPrefs.GetInt("FB_CooldownLevel", 1);
        fz_durLvl = PlayerPrefs.GetInt("FZ_DurationLevel", 1);
        fz_cdLvl = PlayerPrefs.GetInt("FZ_CooldownLevel", 1);
        s_durLvl = PlayerPrefs.GetInt("S_DurationLevel", 1);
        s_cdLvl = PlayerPrefs.GetInt("S_CooldownLevel", 1);
        spearLvl = PlayerPrefs.GetInt("SpearLevel", 1);
        shieldLvl = PlayerPrefs.GetInt("ShieldLevel", 1);
        potionLvl = PlayerPrefs.GetInt("PotionLevel", 1);

        PlayerPrefs.Save();
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("PlayerHP", (int)maxHP);
        PlayerPrefs.SetInt("PlayerDmg", (int)dmgMult);
        PlayerPrefs.SetInt("Dodge", (int)dodge);
        PlayerPrefs.SetInt("Level", lvl);
        PlayerPrefs.SetInt("Kills", kills);
        PlayerPrefs.SetInt("HPLevel", hpLvl);
        PlayerPrefs.SetInt("DmgLevel", dmgLvl);
        PlayerPrefs.SetInt("CritLevel", critLvl);
        PlayerPrefs.Save();
    }

    public void SetSpellStats()
    {
        PlayerPrefs.SetInt("FB_DamageLevel", fb_dmgLvl);
        PlayerPrefs.SetInt("FB_CooldownLevel", fb_cdLvl);
        PlayerPrefs.SetInt("FZ_DurationLevel", fz_durLvl);
        PlayerPrefs.SetInt("FZ_CooldownLevel", fz_cdLvl);
        PlayerPrefs.SetInt("S_DurationLevel", s_durLvl);
        PlayerPrefs.SetInt("S_CooldownLevel", s_cdLvl);
        PlayerPrefs.Save();
    }

    public void SaveItemStats()
    {
        PlayerPrefs.SetInt("SpearLevel", spearLvl);
        PlayerPrefs.SetInt("ShieldLevel", shieldLvl);
        PlayerPrefs.SetInt("PotionLevel", potionLvl);
        PlayerPrefs.Save();
    }

    public void STATRESET()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
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


        //damage calculation: Base Damage (Spear damage) * Dmg Mult , then roll crit chance to see if crit occurs, if so double damage
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber <= critLvl)
        {
            
            
            Debug.Log("CRITICAL HIT! Dealt " + (2 * dmgMult * (spearLvl * 2)) + " damage to " + enemy.name);
            StartCoroutine(enemy.Dmg(2*(dmgMult)*(spearLvl*2)));
        }
        else
        {
            StartCoroutine(enemy.Dmg(dmgMult * (spearLvl * 2)));
        }
        

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

        string textDamage = damage.ToString("F2");

        if (!smoke)
        {
            damageText.text = $"{textDamage}";

            //Debug.Log("Ow! " + currentEnemy.name + " dealt " + damage + "!");
            HP -= damage;
            healthBar.value = HP;
            Spr.color = Color.red;
            yield return new WaitForSeconds(.5f);
            Spr.color = Color.white;
            damageText.text = "";

            if (HP <= 0)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                yield return new WaitForSeconds(.5f);
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
            goldAmt += UnityEngine.Random.Range(3, 6) * lvl;
            PlayerPrefs.SetInt("Saved Gold", goldAmt);
            PlayerPrefs.Save();
        }
    }

    public void FailedDungeonCoins(int coins)
    {
        goldAmt += coins;
        PlayerPrefs.SetInt("Saved Gold", goldAmt);
        PlayerPrefs.Save();
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

    public IEnumerator GameOver()
    {
        animator.Play("Player_Die");

        // Wait for animation to finish
        yield return null;
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        DungeonCompleteScript.Instance.DungeonFailed();
    }

    public void SetDmg(float newDmg)
    {
        dmgMult = newDmg;
    }

    public void SetMaxHP(float newHP) {
        maxHP = newHP;
        HP = newHP;
        
    }

    public void SetDodge(float newDodge)
    {
        dodge = newDodge;
    }
    public void SetCrit(int newCrit)
    {
        critLvl = newCrit;
    }

    public void LateUpdate()
    {
       // coinCounter.text = gold.ToString();
    }
}
