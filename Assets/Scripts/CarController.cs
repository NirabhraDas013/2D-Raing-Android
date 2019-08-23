using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    public float carSpeed;
	public float posRestrict;
	
	Vector3 position;
	
	
	public void Start()
	{
		position = transform.position;
	}
	
	public void Update()
	{
        if (!GameManager.Instance.isGameOver)
        {
            position.x += Input.acceleration.x * carSpeed * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, -posRestrict, posRestrict);

            transform.position = position;
        }
		
	}
}