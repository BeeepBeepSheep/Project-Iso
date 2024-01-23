using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    public void ActivateAbility(int abilitySlot)
    {
        Debug.Log("Activate ability Num: " +  abilitySlot.ToString());
    }

    public void ActivatePotion(int potionSlot)
    {
        Debug.Log("Activate potion Num: " + potionSlot.ToString());
    }
}
