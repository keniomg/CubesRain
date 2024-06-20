using System.Collections;
using UnityEngine;

public class RainDropsSpawner : MonoBehaviour
{
    [SerializeField] private RainDropObject _rainDropObject;

    private float _widthSpawnArea;
    private float _lengthSpawnArea;

    private void Awake()
    {
        int numbersOfSide = 2;
        _widthSpawnArea = transform.localScale.x / numbersOfSide;
        _lengthSpawnArea = transform.localScale.z / numbersOfSide;
    }

    private void Start()
    {
        StartCoroutine(SpawnRainDrops());
    }

    private void InstantiateRainDrop()
    {
        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
        float ySpawnPosition = transform.position.y;
        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
        Vector3 spawnPosition = new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition);
        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);
        Instantiate(_rainDropObject, spawnPosition, rotationPosition);
    }

    private IEnumerator SpawnRainDrops()
    {
        while (true)
        {
            float delayValue = 1;
            InstantiateRainDrop();
            yield return new WaitForSeconds(delayValue);
        }
    }
}