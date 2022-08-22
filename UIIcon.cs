using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIcon : MonoBehaviour
{   
    public TextMeshProUGUI TextPro;
    public Image image;

    public void SetText(string text)
    {
        TextPro.text = text;
    }
    public void SetSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }
}

