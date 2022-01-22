using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float speed = 0.1f;
    float originalPosY;
    float targetPosY;

    private void Start()
    {
        originalPosY = transform.position.y;
        targetPosY = originalPosY;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            targetPosY = originalPosY + GameManager.instance.offsetCamera;

            if (targetPosY > transform.position.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }
        else if (GameManager.instance.gameState == GameManager.GameState.GameOver)
        {
            targetPosY = originalPosY;
            speed = 3f;
            if (targetPosY < transform.position.y)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
    }
}
