using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField] private Text CharName;
    [SerializeField] private Image CharPortrait;

    public void SetButtonDetails(string textString, Sprite portraitSprite)
    {
        CharName.text = textString;
        CharPortrait.sprite = portraitSprite;
    }

    public void OnClick()
    {

    }
}
