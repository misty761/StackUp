using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    public bool bitChangeItem;

    private void Start()
    {
        bitChangeItem = false;
    }

    public void SpawnItem()
    {
        int random = Random.Range(0, items.Length);
        float posX = Random.Range(-2.1f, 2.1f);
        Vector3 pos = new Vector3(posX, transform.position.y + GameManager.instance.offsetCamera, transform.position.z);
        Instantiate(items[random], pos, transform.rotation);
    }
}
