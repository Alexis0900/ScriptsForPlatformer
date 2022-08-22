using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAbility : MonoBehaviour
{   
    public TextMeshProUGUI TextPro;
    public Image image;

    public UIText ammount;
    public UIText cooldown;
   

    public void SetText(string text)
    {
        TextPro.text = text;
    }
    public void SetSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    public void SetCooldownText(string text)
    {
        cooldown.SetText(text);
    }
    public void SetAmmountText(string text)
    {
        ammount.SetText(text);
    }

}
