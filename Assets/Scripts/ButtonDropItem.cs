using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDropItem : MonoBehaviour
{
    public bool isButtonDown;

    private void Start()
    {
        isButtonDown = false;
    }

    public void ButtonDown()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, Vector3.zero, 1f);
        isButtonDown = true;
    }

    public void ButtonUp()
    {
        isButtonDown = false;
    }
}
