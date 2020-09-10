using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout = null;
    [SerializeField] Ability _startingAbility = null;
    [SerializeField] Ability _newAbilityToTest = null;

    [SerializeField] Transform _testTarget = null;
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
        if (Input.GetMouseButtonDown(0))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        //testing abilties
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

    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }


}
