using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    public TextMeshProUGUI TextPro;
    public void SetText(string text)
    {
        TextPro.text = text;
    }

}
