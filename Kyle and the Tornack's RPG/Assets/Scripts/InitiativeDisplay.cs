using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeDisplay : MonoBehaviour
{
    [SerializeField] private Image CharPortrait;
    [SerializeField] private Image CurrentTurnBorder;

    [HideInInspector] public BaseUnit ThisUnit;
    public void SetIcon(Sprite portraitSprite, BaseUnit Unit)
    {
        CharPortrait.sprite = portraitSprite;
        ThisUnit = Unit;
    }

    public void CurrentActiveTurn()
    {
        if(CurrentTurnBorder.enabled == true)
        {
            CurrentTurnBorder.enabled = false;
        }
        else
        {
            CurrentTurnBorder.enabled = true;
        }
    }
}
