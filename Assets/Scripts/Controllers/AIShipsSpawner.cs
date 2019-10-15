using UnityEngine;

public class AIShipsSpawner : MonoBehaviour
{
    public GameObject AIShip;
    public float spawnsPerSecond;

    private float timeToSpawn;

    private void Start()
    {
        timeToSpawn = 1 / spawnsPerSecond;
    }

    private void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0f)
        {
            Instantiate(AIShip);
            timeToSpawn += 1 / spawnsPerSecond;
        }
    }
}
