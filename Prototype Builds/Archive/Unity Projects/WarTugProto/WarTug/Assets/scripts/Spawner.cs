using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnPrefab;
    public int timer = 0;
    public int spawnRate = 30;
    public int spawnRateLow;
    public int spawnRateHigh;


    void Start()
    {

    }

    //random prefab instantiated from array when timer reaches spawnrate value.
    void FixedUpdate()
    {

        timer = timer + 1;
        if (timer > spawnRate)
        {
            spawnRate = Random.Range(spawnRateLow, spawnRateHigh);

            Instantiate(spawnPrefab[Random.Range(0, 9)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            timer = 0;
        }

    }

    //destroys gameobject once it leaves screen.
    private void OnBecameInvisible()
    {
        enabled = true;
    }
}
