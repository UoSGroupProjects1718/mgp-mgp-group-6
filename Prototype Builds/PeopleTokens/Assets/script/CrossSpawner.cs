using UnityEngine;
using System.Collections;

public class CrossSpawner : MonoBehaviour
{

    public GameObject[] objectPrefabs;
    public int timer = 0;
    public int spawnRate = 30;


    void Start()
    {

    }

    //random prefab instantiated from array when timer reaches spawnrate value.
    void FixedUpdate()
    {

        timer = timer + 1;
        if (timer > spawnRate)
        {
            spawnRate = Random.Range(150, 240);

            Instantiate(objectPrefabs[Random.Range(0, 2)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            timer = 0;
        }

    }

    //destroys gameobject once it leaves screen.
    private void OnBecameInvisible()
    {
        enabled = true;
    }
}
