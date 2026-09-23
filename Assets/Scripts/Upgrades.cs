using UnityEngine;

public class Upgrades : MonoBehaviour
{

    [SerializeField] float dmgLvl = 1f;
    [SerializeField] float spdLvl = 1f;
    [SerializeField] float hpLvl = 1f;
    [SerializeField] float skillSlots = 0f;

    public float dmgCost;
    public float spdCost;
    public float hpCost;
    public float skillCost;

    public string[] Skills;

    public GameObject UDisplay;
    bool displayActive = false;

    PlayerScript PS;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PS = GetComponent<PlayerScript>();
        dmgCost = dmgLvl * 10f * 1.5f;
        spdCost = spdLvl * 10f * 1.5f;
        hpCost = hpLvl * 10f * 1.5f;
        skillCost = skillSlots * 10f * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void UpgradeAttackSpeed()
    {
        if (PS.gold >= spdCost)
        {
            spdLvl++;
            spdCost = spdLvl * 10f * 1.5f;
            PS.gold -= (int)spdCost;
            Debug.Log("Attack speed upgraded to level " + spdLvl + ". Gold remaining: " + PS.gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade attack speed. Gold needed: " + spdCost + ", Gold available: " + PS.gold);
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
}
