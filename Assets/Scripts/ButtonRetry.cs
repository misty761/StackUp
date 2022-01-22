using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRetry : MonoBehaviour
{
    /*
    GoogleMobileAdsReward googleAD;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        googleAD = FindObjectOfType<GoogleMobileAdsReward>();

        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            if (googleAD.isAdFailedToLoad)
            {
                isPressed = false;
                GameManager.instance.GameRetry();
                googleAD.MyLoadAD();
            }
            else
            {
                if (googleAD.rewardedAd.IsLoaded())
                {
                    isPressed = false;
                    googleAD.rewardedAd.Show();
                }
            }
        }

        if (googleAD.isRewarded && googleAD.bCloseAD)
        {
            GameManager.instance.GameRetry();
            googleAD.MyLoadAD();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            TouchUp();
        }
    }
    */

    public void TouchUp()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        //isPressed = true;
        GameManager.instance.GameRetry();
    }
}
