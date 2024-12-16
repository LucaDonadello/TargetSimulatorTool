using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject Ammo;

    void Update()
    {
        
    }

    public void ammoSpawner()
    {
        Vector3 spawnPosition = new((float)0.372, (float)1.211, (float)17.836);
        Instantiate(Ammo, spawnPosition, Quaternion.identity);
        Debug.Log("Ammo created");
    }
}
