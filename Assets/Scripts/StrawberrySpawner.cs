using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberrySpawner : MonoBehaviour
{
    public GameObject strawberryPrefab;
    public float distanceFromPlanet = 20f;
    public float spawnDelay = 1f;
    public Transform strawberryParent;

    //private IEnumerator coroutine;

    void Start()
    {
        StartCoroutine(SpawnStrawberry());
    }

    IEnumerator SpawnStrawberry()
        {
            Vector3 pos = Random.onUnitSphere * distanceFromPlanet;
            GameObject strawberry = Instantiate(strawberryPrefab, pos, Quaternion.identity);
            strawberry.transform.parent = strawberryParent;

            yield return new WaitForSeconds(1f);

            StartCoroutine(SpawnStrawberry());
        }
}
