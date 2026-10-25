using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    public static SpellController Instance;

    // Buttons
    [SerializeField] Button fireballButton;
    [SerializeField] Button freezeButton;
    [SerializeField] Button smokeButton;

    // Sliders
    [SerializeField] private Image fireballSlider;
    [SerializeField] private Image freezeSlider;
    [SerializeField] private Image smokeSlider;

    //cooldown handling
    private float fireballCooldown = 5f; // PLACEHOLDER COOLDOWNS
    private float freezeCooldown = 5f;
    private float smokeCooldown = 5f;

    private float fireballTimer = 0;
    private float freezeTimer = 0;
    private float smokeTimer = 0;

    // Scripts
    private PlayerScript playerScript;
    private CombatManager combatManager;

    // Transforms
    private Transform player;
    private Transform enemy;

    //Setters

    public void SetPlayer(Transform player) { this.player = player; }
    public void SetPlayerScript(PlayerScript script) { playerScript = script; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        enemy = null;

        combatManager = CombatManager.Instance;

        DisableSpellButtons();
    }

    void Update()
    {
        UpdateCooldown(ref fireballTimer, fireballCooldown,fireballSlider,fireballButton);
        UpdateCooldown(ref freezeTimer,freezeCooldown,freezeSlider,freezeButton);
        UpdateCooldown(ref smokeTimer,smokeCooldown,smokeSlider,smokeButton);

        if (fireballTimer > 0f)
        {
            fireballTimer -= Time.deltaTime;
        }
        
        if (freezeTimer > 0f)
        {
            freezeTimer -= Time.deltaTime;
        }
        
        if (smokeTimer > 0f)
        {
            smokeTimer -= Time.deltaTime;
        }
    }

    private void UpdateCooldown(ref float timer, float cooldown, Image Slider, Button button)
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(0f, timer);
        }

        // Update visual slider
        if (Slider != null)
        {
            Slider.fillAmount = cooldown > 0f
                ? timer / cooldown
                : 0f;

            Slider.gameObject.SetActive(timer > 0f);
        }

        if (button != null)
        {
            button.interactable = enemy != null && timer <= 0f;
        }
    }

    public void ActivateSpellButtons(Transform enemy)
    {
        this.enemy = enemy;

        if (fireballTimer <= 0f)
            fireballButton.interactable = true;
        if (freezeTimer <= 0f)
            freezeButton.interactable = true;
        if (smokeTimer <= 0f)
            smokeButton.interactable = true;
    }

    public void DisableSpellButtons()
    {
        enemy = null;

        fireballButton.interactable = false;
        freezeButton.interactable = false;
        smokeButton.interactable = false;
    }











    // BUTTON CONTROLS

    public void SetFireball()
    {
        fireballTimer = fireballCooldown;
        playerScript.SetFireball(true);
    }

    public void SetFreeze()
    {
        freezeTimer = freezeCooldown;
        combatManager.FreezeEnemy();
    }

    public void SetSmoke()
    {
        smokeTimer = smokeCooldown;
        combatManager.HidePlayer();
    }

}
