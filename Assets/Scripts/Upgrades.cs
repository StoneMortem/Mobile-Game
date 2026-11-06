using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    [SerializeField] int dmgLvl = 1;
    [SerializeField] int critLvl = 1;
    [SerializeField] int hpLvl = 1;
    [SerializeField] int skillSlots = 0;

    [SerializeField] int fb_damageLvl = 1;
    [SerializeField] int fb_cooldownLvl = 1;
    [SerializeField] int fz_durationLvl = 1;
    [SerializeField] int fz_cooldownLvl = 1;
    [SerializeField] int s_durationLvl = 1;
    [SerializeField] int s_cooldownLvl = 1;

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

    #region text elements
    public TMP_Text goldText;
    public TMP_Text hpText;
    public TMP_Text hpDesc;
    public TMP_Text dmgText;
    public TMP_Text dmgDesc;
    public TMP_Text critText;
    public TMP_Text critDesc;
    public TMP_Text fb_dText;
    public TMP_Text fb_cText;
    public TMP_Text fb_Desc;
    public TMP_Text fz_dText;
    public TMP_Text fz_cText;
    public TMP_Text fz_Desc;
    public TMP_Text s_dText;
    public TMP_Text s_cText;
    public TMP_Text s_Desc;

    public TMP_Text spearDesc;
    public TMP_Text spearText;
    public TMP_Text shieldDesc;
    public TMP_Text shieldText;
    public TMP_Text potionDesc;
    public TMP_Text potionText;


    #endregion

    public string[] Skills;

    public GameObject UDisplay;
    bool displayActive = false;

    PlayerScript PS;
    public int coinsAmt;
    string playerCoins;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PS = GetComponent<PlayerScript>();

        PS.InitStats();
        InitDescriptions();

        Debug.Log("Coins Amt: " + coinsAmt);
        if (playerCoins == null)
        {
            Debug.Log("gold amt to string not null");
            if (coinsAmt.ToString() != null)
            {
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

        GameObject coinObject = GameObject.FindWithTag("CoinUI");

        if (coinObject != null)
        {
            Debug.Log("coin object not null");
            goldText = coinObject.GetComponentInChildren<TextMeshProUGUI>();
            if (goldText.text != null)
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

        dmgCost = PS.dmgLvl * 10f * 1.5f;
        critCost = PS.critLvl * 10f * 1.5f;
        hpCost = PS.hpLvl * 10f * 1.5f;
        skillCost = skillSlots * 10f * 1.5f;

        spearCost = PS.spearLvl * 10f * 1.5f;
        potionCost = PS.potionLvl * 10f * 1.5f;
        shieldCost = PS.shieldLvl * 10f * 1.5f;


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
       if (PS == null)
        {
            PS = GetComponent<PlayerScript>();
        }
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
        spearText.text = spearCost.ToString();
        shieldText.text = shieldCost.ToString();
        potionText.text = potionCost.ToString();
    }

    public void InitDescriptions()
    {
        hpDesc.text = "Increases your maximum health by 10 points" +
            "\nHP: " + (20 + (PS.hpLvl*10)   + " -> " + (20 + (PS.hpLvl +1)*10));
        dmgDesc.text = "Increases your overall damage by 25%" + "\nDMG MULT: " + 1+( PS.dmgLvl*.25) + "x -> " + 1+((PS.dmgLvl + 1)*.25) + "x";
        critDesc.text = "Increases your critical hit chance by 1%." +"\n CRT CHANCE: " +  PS.critLvl + "%  -> " + (PS.critLvl + 1) +"%";
        fb_Desc.text = "Summons a fireball to deal damage after a short delay.";
        fz_Desc.text = "Freezes enemies, preventing them from attacking you for a short duration.";
        s_Desc.text = "Throws a smoke bomb, making your enemies' attacks unable to hit you.";
        spearDesc.text = "Increases the base damage of your spear attacks." + "\nDMG: " + (2 * PS.spearLvl) + " -> " + (2 * (PS.spearLvl + 1));
        shieldDesc.text = "Reduces the damage you take from enemy attacks.";
        potionDesc.text = "Restores a portion of your health when consumed.";
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
            PS.dmgLvl++;
            PS.SetDmg(2 * PS.dmgLvl * 1.25f);
            dmgCost = dmgLvl * 10f * 1.5f;
            PS.goldAmt -= (int)dmgCost;
            Debug.Log("Damage upgraded to level " + dmgLvl + ". Gold remaining: " + PS.goldAmt);
            PS.dmgLvl++;
        }
        else
        {
            Debug.Log("Not enough gold to upgrade damage. Gold needed: " + dmgCost + ", Gold available: " + PS.goldAmt);
        }
        PS.SaveStats();
    }

    public void UpgradeCrit()
    {

        if (PS.goldAmt >= critCost)
        {
            critLvl++;
            critCost = critLvl * 10f * 1.5f;
            PS.goldAmt -= (int)critCost;
            PS.SetCrit(critLvl);
            Debug.Log("Crit chance upgraded to level " + critLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade crit. Gold needed: " + critCost + ", Gold available: " + PS.goldAmt);
        }
        //save changes
        PS.SaveStats();
        //reset descriptions to reflect new stats
        InitDescriptions();
    }

    public void UpgradeHealth()
    {
        if (PS.goldAmt >= hpCost)
        {
            hpLvl++;
            hpCost = hpLvl * 10f * 1.5f;
            PS.hpLvl++;
            PS.SetMaxHP(20 + (PS.hpLvl * 10));
            PS.goldAmt -= (int)hpCost;
            Debug.Log("Health upgraded to level " + hpLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade health. Gold needed: " + hpCost + ", Gold available: " + PS.goldAmt);
        }
        PS.SaveStats();
        InitDescriptions();
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
            PS.fb_dmgLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)fb_damageCost;
            Debug.Log("FB_damage upgraded to level " + fb_damageLvl + ". Gold remaining: " + PS.goldAmt);
            fb_damageCost = fb_damageLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);

        }
        InitDescriptions();
    }

    public void UpgradeFBCooldown()
    {
        if (PS.goldAmt >= fb_cooldownCost)
        {
            fb_cooldownLvl++;
            PS.fb_cdLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)fb_cooldownCost;
            Debug.Log("FB_cooldown upgraded to level " + fb_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
            fb_cooldownCost = fb_cooldownLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeFZDuration()
    {
        if (PS.goldAmt >= fz_durationCost)
        {
            fz_durationLvl++;
            PS.fz_durLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)fz_durationCost;
            Debug.Log("FZ_duration upgraded to level " + fz_durationLvl + ". Gold remaining: " + PS.goldAmt);
            fz_durationCost = fz_durationLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeFZCooldown()
    {
        if (PS.goldAmt >= fz_cooldownCost)
        {
            fz_cooldownLvl++;
            PS.fz_cdLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)fz_cooldownCost;
            Debug.Log("FZ_cooldown upgraded to level " + fz_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
            fz_cooldownCost = fz_cooldownLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeSDuration()
    {
        if (PS.goldAmt >= s_durationCost)
        {
            s_durationLvl++;
            PS.s_durLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)s_durationCost;
            s_durationCost = s_durationLvl * 10f * 1.5f;
            Debug.Log("S_duration upgraded to level " + s_durationLvl + ". Gold remaining: " + PS.goldAmt);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeSCooldown()
    {
        if (PS.goldAmt >= s_cooldownCost)
        {
            s_cooldownLvl++;
            PS.s_cdLvl++;
            PS.SetSpellStats();
            
            PS.goldAmt -= (int)s_cooldownCost;
            Debug.Log("S_cooldown upgraded to level " + s_cooldownLvl + ". Gold remaining: " + PS.goldAmt);
            s_cooldownCost = s_cooldownLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeSpear()
    {
        if (PS.goldAmt >= spearCost)
        {
            PS.spearLvl++;
            PS.SaveItemStats();
            
            PS.goldAmt -= (int)spearCost;
            Debug.Log("Spear upgraded to level " + PS.spearLvl + ". Gold remaining: " + PS.goldAmt);
            spearCost = PS.spearLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradeShield()
    {
        if (PS.goldAmt >= shieldCost)
        {
            PS.shieldLvl++;
            PS.SaveItemStats();

            PS.goldAmt -= (int)shieldCost;
            Debug.Log("Shield upgraded to level " + PS.shieldLvl + ". Gold remaining: " + PS.goldAmt);
            shieldCost = PS.shieldLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.goldAmt);
        }
        InitDescriptions();
    }

    public void UpgradePotion()
    {
        if (PS.goldAmt >= potionCost)
        {
            PS.potionLvl++;
            PS.SaveItemStats();
            PS.goldAmt -= (int)potionCost;
            Debug.Log("Potion upgraded to level " + PS.potionLvl + ". Gold remaining: " + PS.goldAmt);
            potionCost = PS.potionLvl * 10f * 1.5f;
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
        InitDescriptions();
    }

}
