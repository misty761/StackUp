using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Destroy(other.gameObject);
            ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
            itemSpawner.SpawnItem();
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("Animal"))
        {
            GameManager.instance.GameOver();
        }
    }
}
