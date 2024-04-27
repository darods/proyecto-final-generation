using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTarget : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1.0f;
    private bool isActive = true;
    private ObjectPooler objectPooler;

    private ObjectPooler.ObjectsToSpawn[] menuItemsToSpawn = {
        ObjectPooler.ObjectsToSpawn.MenuCookie,
        ObjectPooler.ObjectsToSpawn.MenuPizza,
        ObjectPooler.ObjectsToSpawn.MenuSandwish,
        ObjectPooler.ObjectsToSpawn.MenuSoda,
        ObjectPooler.ObjectsToSpawn.MenuSteak
    };

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (isActive)
        {
            foreach (var item in menuItemsToSpawn)
            {
                SpawnAndActivateObject(item);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnAndActivateObject(ObjectPooler.ObjectsToSpawn objectType)
    {
        GameObject spawnedObject = objectPooler.SpawnFromPool(objectType, transform.position, transform.rotation);
        spawnedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        spawnedObject.GetComponent<Target>().ActivateObject();
    }

    public void StopSpawn()
    {
        isActive = false;
    }
}
