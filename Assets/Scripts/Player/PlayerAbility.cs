using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout = null;
    [SerializeField] Ability _startingAbility = null;
    [SerializeField] Ability _newAbilityToTest = null;

    [SerializeField] Transform _testTarget = null;
    [SerializeField] ThirdPersonMovement thirdpersonmovement;
    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_newAbilityToTest);
        }
    }

    private void Update()
    {
        //groundcheck to prevent player from using fireball while jumping or moving
        if (Input.GetMouseButtonDown(0) && thirdpersonmovement.Grounded == true && thirdpersonmovement.Moving == false)
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        //test abilties *ignore
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _abilityLoadout.EquipAbility(_newAbilityToTest);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _abilityLoadout.EquipAbility(_startingAbility);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTarget(_testTarget);
        }
    }

    //test target *ignore
    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }


}
