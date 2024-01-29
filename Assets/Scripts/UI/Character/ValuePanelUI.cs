using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuePanelUI : MonoBehaviour
{
    [SerializeField] List<ValueUI> attributesValueUIElements;
    [SerializeField] List<ValueUI> statsValueUIElements;
    [SerializeField] Character targetCharacter;

    private void Update()
    {
        UpdatePanel(targetCharacter);
    }

    public void UpdatePanel(Character character)
    {
        for(int i = 0; i < attributesValueUIElements.Count; i++)
        {
            attributesValueUIElements[i].ShowCharacterValue(character);
        }

        for (int i = 0; i < statsValueUIElements.Count; i++)
        {
            statsValueUIElements[i].ShowCharacterValue(character);
        }
    }
}
