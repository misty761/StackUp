using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animals;
    public float spawnTimeMin = 5f;
    public float spawnTimeMax = 10f;

    float randomTime;
    float timeSpawn;
    public readonly float posX = 4f;

    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(5f, 10f);
        timeSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Playing) return;

        timeSpawn += Time.deltaTime;

        if (timeSpawn > randomTime)
        {
            timeSpawn = 0f;
            randomTime = Random.Range(spawnTimeMin, spawnTimeMax);
            int index = Random.Range(0, animals.Length);
            ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
            float offset = 2f;
            float maxY = itemSpawner.transform.position.y + GameManager.instance.offsetCamera - offset;
            float minY = GameManager.instance.stackedHeight + offset;
            if (maxY < minY)
            {
                maxY = (maxY + minY) / 2;
                minY = maxY;
            }
            float posY = Random.Range(minY, maxY);
            int direction = Random.Range(0, 2);     // 0:화면 왼쪽에 동물 생성, 1:화면 오른쪼겡 동물 생성
            // 화면 왼쪽에 동물 생성
            if (direction == 0)
            {
                Vector3 pos = new Vector3(-posX, posY, 0);
                GameObject goAnimal = Instantiate(animals[index], pos, Quaternion.Euler(Vector3.zero));
                AnimalMove animal = goAnimal.GetComponent<AnimalMove>();
                animal.isRight = true;
            }
            else
            {
                Vector3 pos = new Vector3(posX, posY, 0);
                GameObject goAnimal = Instantiate(animals[index], pos, Quaternion.Euler(Vector3.zero));
                AnimalMove animal = goAnimal.GetComponent<AnimalMove>();
                animal.isRight = false;
            }
        }
    }
}
