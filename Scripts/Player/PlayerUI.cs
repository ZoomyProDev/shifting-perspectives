using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{

    public Text promptText;

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
