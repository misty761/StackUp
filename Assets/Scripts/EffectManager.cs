using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject explosion;
    public GameObject smokePuff;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log(this + " : GameObject is already existed.");
            Destroy(gameObject);
        }
    }
}
