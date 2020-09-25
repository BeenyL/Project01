using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoadout : MonoBehaviour
{
    public Ability EquippedAbility { get; private set; }
    public Ability UltimateAbility { get; private set; }
    public void EquipDefaultAbility(Ability ability)
    {
        RemoveCurrentAbilityObject();
        CreateNewAbilityObject(ability);
    }
    public void EquipUltimateAbility(Ability ability)
    {
        RemoveCurrentAbilityObject();
        CreateNewAbilityObject(ability);
    }

    public void UseDefaultAbility()
    {
        EquippedAbility.Use(this.transform);
    }

    public void UseUltimateAbility()
    {
        UltimateAbility.Use(this.transform);
    }

    public void RemoveCurrentAbilityObject()
    {
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void CreateNewAbilityObject(Ability ability)
    {
        EquippedAbility = Instantiate(ability, transform.position, Quaternion.identity);
        UltimateAbility = Instantiate(ability, transform.position, Quaternion.identity);
        EquippedAbility.transform.SetParent(this.transform);
        UltimateAbility.transform.SetParent(this.transform);
    }

}
