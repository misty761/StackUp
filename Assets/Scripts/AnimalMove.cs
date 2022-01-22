using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float speed = 1f;

    public bool isRight;

    float destroyX;
    float size;
    bool bitTurn;

    private void Start()
    {
        AnimalSpawner animalSpawner = FindObjectOfType<AnimalSpawner>();
        destroyX = animalSpawner.posX + 1f;
        bitTurn = false;
        size = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(size, size, size);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            transform.localScale = new Vector3(-size, size, size);
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < -destroyX || transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (bitTurn == true)
        {
            bitTurn = false;
            isRight = !isRight;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bitTurn = true;
    }
}
