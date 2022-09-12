using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSelectButton : MonoBehaviour
{
    [SerializeField] private TMP_Text CharName;
    [SerializeField] private Image CharPortrait;
    [SerializeField] private Image SelectedBorder;

    [HideInInspector] public ScriptableUnit ThisUnit;

    public void SetButtonDetails(string textString, Sprite portraitSprite, ScriptableUnit Unit)
    {
        CharName.text = textString;
        CharPortrait.sprite = portraitSprite;
        ThisUnit = Unit;
    }

    public void MouseOverButton()
    {
        BattleSetupManager.Instance.CharacterInfo(CharPortrait.sprite, ThisUnit.BaseVigor, ThisUnit.BaseStamina, ThisUnit.BasePower, ThisUnit.BaseSkill, ThisUnit.BaseArcane, ThisUnit.BaseWill);
    }
        
    public void Clicked()
    {
        if(BattleSetupManager.Instance.SelectedUnits.Count != 0)
        {
            bool WasThisUnitSelected = false;

            foreach(ScriptableUnit Unit in BattleSetupManager.Instance.SelectedUnits)
            {
                if(ThisUnit == Unit)
                {
                    BattleSetupManager.Instance.SelectedUnits.Remove(Unit);
                    WasThisUnitSelected = true;
                    SelectedBorder.enabled = false;
                }
            }
            if(WasThisUnitSelected != true && BattleSetupManager.Instance.SelectedUnits.Count < 6)
            {
                BattleSetupManager.Instance.SelectedUnits.Add(ThisUnit);
                SelectedBorder.enabled = true;
            }
        }
        else
        {
            BattleSetupManager.Instance.SelectedUnits.Add(ThisUnit);
            SelectedBorder.enabled = true;
        }
    }
}
