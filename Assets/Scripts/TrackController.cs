using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackController : MonoBehaviour
{

    public static TrackController Instance;

	public float trackSpeed;
	
	Vector2 offset;
	
    public void Start()
    {
        Instance = this;
    }

	public void Update()
	{
		offset = new Vector2(0, trackSpeed * Time.time);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}