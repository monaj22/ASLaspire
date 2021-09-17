using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Groups
    public GameObject[] groups;

    public void spawnNext() {
        //Random Index
        int i = Random.Range(0, groups.Length);

        //Spawn group at current Position
        Instantiate(groups[i], transform.position, Quaternion.identity);

    }

    void Start() {
        //Spawn initial group
        spawnNext();
    }
}

