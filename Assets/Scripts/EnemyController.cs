using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

	public void Start()
	{
	
	}
	
	public void Update()
	{
		transform.Translate(new Vector3(0, 1, 0) * GameManager.Instance.enemySpeed * Time.deltaTime);
	}

}