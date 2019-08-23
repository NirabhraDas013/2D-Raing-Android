using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveController : MonoBehaviour
{
    public static WaveController Instance;

    public GameObject[] cars;
    public float posRestrict;

    int carIndex;
    float xPosition;
    int arrayLength;
    float waitTimeBetweenCars;
    Vector3 spawnPosition;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        arrayLength = cars.Length - 1;
        waitTimeBetweenCars = Random.Range(GameManager.Instance.minWaitTimeBetweenCars, GameManager.Instance.maxWaitTimeBetweenCars);
    }

    public void Update()
    {
        waitTimeBetweenCars -= Time.deltaTime;

        if (waitTimeBetweenCars <= 0)
        {
            xPosition = Random.Range(-posRestrict, posRestrict);
            carIndex = Random.Range(0, arrayLength);

            spawnPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
            Instantiate(cars[carIndex], spawnPosition, Quaternion.identity);

            waitTimeBetweenCars = Random.Range(GameManager.Instance.minWaitTimeBetweenCars, GameManager.Instance.maxWaitTimeBetweenCars);
        }
        
    }
}
 