using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShowText;

public class TextUI : TextManager
{
    public void CallText()
    {
        OnCallText?.Invoke();
    }
}
