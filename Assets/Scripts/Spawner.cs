using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }
    void SpawnObject()
    {
        GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        //obj.GetComponent<Rigidbody>().linearVelocity = new Vector3(-2, 0, 0); // Move forward
    }

}
