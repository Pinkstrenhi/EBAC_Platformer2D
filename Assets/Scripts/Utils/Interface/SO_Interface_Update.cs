using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SO_Interface_Update : MonoBehaviour
{
    public SO_Interface soInterface;
    public TextMeshProUGUI uiTextValue;

    private void Update()
    {
        uiTextValue.text = soInterface.value.ToString();
    }
}
