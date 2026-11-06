using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    [SerializeField] float dmgLvl = 1f;
    [SerializeField] float critLvl = 1f;
    [SerializeField] float hpLvl = 1f;
    [SerializeField] float skillSlots = 0f;

    [SerializeField] float fb_damageLvl = 1f;
    [SerializeField] float fb_cooldownLvl = 1f;
    [SerializeField] float fz_durationLvl = 1f;
    [SerializeField] float fz_cooldownLvl = 1f;
    [SerializeField] float s_durationLvl = 1f;
    [SerializeField] float s_cooldownLvl = 1f;

    public float dmgCost;
    public float critCost;
    public float hpCost;
    public float skillCost;

    public float fb_damageCost;
    public float fb_cooldownCost;
    public float fz_durationCost;
    public float fz_cooldownCost;
    public float s_durationCost;
    public float s_cooldownCost;

    public float spearCost;
    public float shieldCost;
    public float potionCost;

    public TMP_Text goldText;
    public TMP_Text hpText;
    public TMP_Text dmgText;
    public TMP_Text critText;
    public TMP_Text fb_dText;
    public TMP_Text fb_cText;
    public TMP_Text fz_dText;
    public TMP_Text fz_cText;
    public TMP_Text s_dText;
    public TMP_Text s_cText;

    public string[] Skills;

    public GameObject UDisplay;
    bool displayActive = false;

    public PlayerScript PS;
    public int coinsAmt;
    string playerCoins;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PS = GetComponent<PlayerScript>();
        // PS = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        //coinsAmt = PS.goldAmt;
        //Debug.Log("Ps gold amt: " + PS.goldAmt);
        Debug.Log("Coins Amt: " + coinsAmt);
        if (playerCoins == null)
        {
            Debug.Log("gold amt to string not null");
            if(coinsAmt.ToString() != null)
            {
                // coinsAmt = PS.goldAmt;
                coinsAmt = PlayerPrefs.GetInt("Saved Gold", 0);
                Debug.Log("Coins Amt: " + coinsAmt);
                playerCoins = coinsAmt.ToString();
            }
           
        }
        if (playerCoins != null)
        {
            Debug.Log("coinsText not null");
            playerCoins = coinsAmt.ToString();
        }
        /*if (PS.coinsText.text != null)
        {
            Debug.Log("coinsText text not null");
            playerCoins = PS.goldAmt.ToString();
        }*/


        GameObject coinObject = GameObject.FindWithTag("CoinUI");

        if (coinObject != null)
        {
            Debug.Log("coin object not null");
            //PS.coinsText = coinObject.GetComponentInChildren<TextMeshProUGUI>();
            goldText = coinObject.GetComponentInChildren<TextMeshProUGUI>();
            if(goldText.text != null)
            {
                goldText.text = playerCoins;
            }
            else
            {
                Debug.Log("ps gold amt to string is null");
            }
        }
        else
        {
            Debug.Log("Could not find coin ui tagged!");
        }

        dmgCost = dmgLvl * 10f * 1.5f;
        critCost = critLvl * 10f * 1.5f;
        hpCost = hpLvl * 10f * 1.5f;
        skillCost = skillSlots * 10f * 1.5f;
        
        fb_damageCost = fb_damageLvl * 10f * 1.5f;
        fb_cooldownCost = fb_cooldownLvl * 10f * 1.5f;
        fz_durationCost = fz_durationLvl * 10f * 1.5f;
        fz_cooldownCost = fz_cooldownLvl * 10f * 1.5f;
        s_durationCost = s_durationLvl * 10f * 1.5f;
        s_cooldownCost = s_cooldownLvl * 10f * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("amt of gold: " + PS.goldAmt);
        //PS.UpdateCoinsCounter();
        //playerCoins = PS.coinsText.text;
        goldText.text = playerCoins;
        hpText.text = hpCost.ToString();
        dmgText.text = dmgCost.ToString();
        critText.text = critCost.ToString();
        fb_dText.text = fb_damageCost.ToString();
        fb_cText.text = fb_cooldownCost.ToString();
        fz_dText.text = fz_durationCost.ToString();
        fz_cText.text = fz_cooldownCost.ToString();
        s_dText.text = s_durationCost.ToString();
        s_cText.text = s_cooldownCost.ToString();
    }

    public void UpgradeDisplay()
    {
        displayActive = !displayActive;
    }

    public void UpgradeDamage()
    {
        if (PS.goldAmt >= dmgCost)
        {
            dmgLvl++;
            dmgCost = dmgLvl * 10f * 1.5f;
            PS.goldAmt -= (int)dmgCost;
            Debug.Log("Damage upgraded to level " + dmgLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade damage. Gold needed: " + dmgCost + ", Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeCrit()
    {
        if (PS.goldAmt >= critCost)
        {
            critLvl++;
            critCost = critLvl * 10f * 1.5f;
            PS.goldAmt -= (int)critCost;
            Debug.Log("Attack speed upgraded to level " + critLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade crit. Gold needed: " + critCost + ", Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeHealth()
    {
        if (PS.goldAmt >= hpCost)
        {
            hpLvl++;
            hpCost = hpLvl * 10f * 1.5f;
            PS.goldAmt -= (int)hpCost;
            Debug.Log("Health upgraded to level " + hpLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade health. Gold needed: " + hpCost + ", Gold available: " + PS.goldAmt);
        }
    }
    public void IncreaseSkillSlots()
    {
        if (PS.goldAmt >= skillCost)
        {
            skillSlots++;
            skillCost *=  100f * 3f;
            PS.goldAmt -= (int)skillCost;
            Debug.Log("Skill slots increased to " + skillSlots + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to increase skill slots. Gold needed: " + skillCost + ", Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeFBDamage()
    {
        if (PS.goldAmt >= fb_damageCost)
        {
            fb_damageLvl++;
            fb_damageCost = fb_damageLvl * 10f * 1.5f;
            PS.goldAmt -= (int)fb_damageCost;
            Debug.Log("FB_damage upgraded to level " + fb_damageLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeFBCooldown()
    {
        if (PS.goldAmt >= fb_cooldownCost)
        {
            fb_cooldownLvl++;
            fb_cooldownCost = fb_cooldownLvl * 10f * 1.5f;
            PS.goldAmt -= (int)fb_cooldownCost;
            Debug.Log("FB_cooldown upgraded to level " + fb_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeFZDuration()
    {
        if (PS.goldAmt >= fz_durationCost)
        {
            fz_durationLvl++;
            fz_durationCost = fz_durationLvl * 10f * 1.5f;
            PS.goldAmt -= (int)fz_durationCost;
            Debug.Log("FZ_duration upgraded to level " + fz_durationLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeFZCooldown()
    {
        if (PS.goldAmt >= fz_cooldownCost)
        {
            fz_cooldownLvl++;
            fz_cooldownCost = fz_cooldownLvl * 10f * 1.5f;
            PS.goldAmt -= (int)fz_cooldownCost;
            Debug.Log("FZ_cooldown upgraded to level " + fz_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeSDuration()
    {
        if (PS.goldAmt >= s_durationCost)
        {
            s_durationLvl++;
            s_durationCost = s_durationLvl * 10f * 1.5f;
            PS.goldAmt -= (int)s_durationCost;
            Debug.Log("S_duration upgraded to level " + s_durationLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

    public void UpgradeSCooldown()
    {
        if (PS.goldAmt >= s_cooldownCost)
        {
            s_cooldownLvl++;
            s_cooldownCost = s_cooldownLvl * 10f * 1.5f;
            PS.goldAmt -= (int)s_cooldownCost;
            Debug.Log("S_cooldown upgraded to level " + s_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
    }

}
