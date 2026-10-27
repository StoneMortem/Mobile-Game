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

    PlayerScript PS;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PS = GetComponent<PlayerScript>();

        //goldText.text = PS.gold.ToString();
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
        if (PS.gold >= dmgCost)
        {
            dmgLvl++;
            dmgCost = dmgLvl * 10f * 1.5f;
            PS.gold -= (int)dmgCost;
            Debug.Log("Damage upgraded to level " + dmgLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade damage. Gold needed: " + dmgCost + ", Gold available: " + PS.gold);
        }
    }

    public void UpgradeCrit()
    {
        if (PS.gold >= critCost)
        {
            critLvl++;
            critCost = critLvl * 10f * 1.5f;
            PS.gold -= (int)critCost;
            Debug.Log("Attack speed upgraded to level " + critLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade crit. Gold needed: " + critCost + ", Gold available: " + PS.gold);
        }
    }

    public void UpgradeHealth()
    {
        if (PS.gold >= hpCost)
        {
            hpLvl++;
            hpCost = hpLvl * 10f * 1.5f;
            PS.gold -= (int)hpCost;
            Debug.Log("Health upgraded to level " + hpLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade health. Gold needed: " + hpCost + ", Gold available: " + PS.gold);
        }
    }
    public void IncreaseSkillSlots()
    {
        if (PS.gold >= skillCost)
        {
            skillSlots++;
            skillCost *=  100f * 3f;
            PS.gold -= (int)skillCost;
            Debug.Log("Skill slots increased to " + skillSlots + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold to increase skill slots. Gold needed: " + skillCost + ", Gold available: " + PS.gold);
        }
    }

    public void UpgradeFBDamage()
    {
        if (PS.gold >= fb_damageCost)
        {
            fb_damageLvl++;
            fb_damageCost = fb_damageLvl * 10f * 1.5f;
            PS.gold -= (int)fb_damageCost;
            Debug.Log("FB_damage upgraded to level " + fb_damageLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

    public void UpgradeFBCooldown()
    {
        if (PS.gold >= fb_cooldownCost)
        {
            fb_cooldownLvl++;
            fb_cooldownCost = fb_cooldownLvl * 10f * 1.5f;
            PS.gold -= (int)fb_cooldownCost;
            Debug.Log("FB_cooldown upgraded to level " + fb_cooldownLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

    public void UpgradeFZDuration()
    {
        if (PS.gold >= fz_durationCost)
        {
            fz_durationLvl++;
            fz_durationCost = fz_durationLvl * 10f * 1.5f;
            PS.gold -= (int)fz_durationCost;
            Debug.Log("FZ_duration upgraded to level " + fz_durationLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

    public void UpgradeFZCooldown()
    {
        if (PS.gold >= fz_cooldownCost)
        {
            fz_cooldownLvl++;
            fz_cooldownCost = fz_cooldownLvl * 10f * 1.5f;
            PS.gold -= (int)fz_cooldownCost;
            Debug.Log("FZ_cooldown upgraded to level " + fz_cooldownLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

    public void UpgradeSDuration()
    {
        if (PS.gold >= s_durationCost)
        {
            s_durationLvl++;
            s_durationCost = s_durationLvl * 10f * 1.5f;
            PS.gold -= (int)s_durationCost;
            Debug.Log("S_duration upgraded to level " + s_durationLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

    public void UpgradeSCooldown()
    {
        if (PS.gold >= s_cooldownCost)
        {
            s_cooldownLvl++;
            s_cooldownCost = s_cooldownLvl * 10f * 1.5f;
            PS.gold -= (int)s_cooldownCost;
            Debug.Log("S_cooldown upgraded to level " + s_cooldownLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold, Gold available: " + PS.gold);
        }
    }

}
