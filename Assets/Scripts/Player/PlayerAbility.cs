using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout = null;
    [SerializeField] Ability _startingAbility = null;
    [SerializeField] Ability _newAbilityToTest = null;

    [SerializeField] Transform _testTarget = null;
    [SerializeField] PlayerMovement playermovement;

    [SerializeField] Slider CooldownSlider;
    [SerializeField] Text CooldownText;

    float cooldown = 2;
    float cooldownValue;
    bool _isCasted = false;
    public bool Casted { get => _isCasted; set => _isCasted = value; }
    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_newAbilityToTest);
        }
    }
    private void Start()
    {
        cooldownValue = 0;
        CooldownSlider.maxValue = cooldown;
        CooldownSlider.value = 0;
    }

    private void Update()
    {
        //groundcheck to prevent player from using fireball while jumping or moving
        if (Input.GetMouseButtonDown(0) && _isCasted == false && playermovement.Grounded == true && playermovement.Moving == false)
        {
            cooldownValue = cooldown;
            CooldownSlider.value = cooldown;
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
            StartCoroutine(AbilityCooldown());
        }
        if(cooldownValue >= 0.001)
        {
            cooldownUI();
        }
    }
    //Spell Cooldown
    IEnumerator AbilityCooldown()
    {
        _isCasted = true;
        yield return new WaitForSecondsRealtime(cooldown);
        _isCasted = false;

    }

    void cooldownUI()
    {
            CooldownText.text = (cooldownValue -= Time.deltaTime).ToString("F2");
            CooldownSlider.value -= Time.deltaTime;
    }

    //test target *ignore
    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }


}
