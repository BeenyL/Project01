using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout = null;
    [SerializeField] AbilityLoadout _abilityLoadoutSecondary = null;
    [SerializeField] Ability _defaultAbility = null;
    [SerializeField] Ability _ultimateAbility = null;

    [SerializeField] PlayerMovement playermovement;
    [SerializeField] PlayerProperty playerproperty;

    [SerializeField] Text CooldownText;

    [SerializeField] Image AttackImg;
    [SerializeField] Color defaultColor;
    Color CooldownColor;

    float cooldown = 1;
    float cooldownValue;
    bool _isCasted = false;
    public bool Casted { get => _isCasted; set => _isCasted = value; }
    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_defaultAbility != null)
        {
            _abilityLoadout?.EquipDefaultAbility(_defaultAbility);
        }
        if (_ultimateAbility != null)
        {
            _abilityLoadoutSecondary?.EquipUltimateAbility(_ultimateAbility);
        }
    }
    private void Start()
    {
    }

    private void Update()
    {
        //groundcheck to prevent player from using fireball while jumping or moving
        if (Input.GetMouseButtonDown(0) && _isCasted == false && playermovement.Grounded == true && playermovement.Moving == false && playerproperty.isDead == false)
        {
            cooldownValue = cooldown;
            _abilityLoadout.UseDefaultAbility();
            StartCoroutine(AbilityCooldown());
        }
        if (cooldownValue >= 0.0001)
        {
            cooldownUI();
        }
    }
    //Spell Cooldown
    IEnumerator AbilityCooldown()
    {
        PlayerProperty playerproperty = GetComponentInChildren<PlayerProperty>();
        AttackImg.GetComponent<Image>();
        if(playerproperty.isRage == true)
        {
            cooldownValue = cooldown / 2;
        }
        else
        {
            cooldownValue = cooldown;
        }
        defaultColor.a = .5f;
        _isCasted = true;
        AttackImg.color = defaultColor;
        yield return new WaitForSecondsRealtime(cooldownValue);
        defaultColor.a = 1f;
        AttackImg.color = defaultColor;
        _isCasted = false;

    }

    void cooldownUI()
    {
        CooldownText.text = (cooldownValue -= (1*Time.deltaTime)).ToString("F1");
    }

}
