using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyPlayer : MonoBehaviour
{
    public static DestroyPlayer Instance;

    private GameObject[] destroyables;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;

    public GameObject fireParticle;
    public GameObject explosion;
    public GameObject firePrefab;
    public AudioSource crashAudio;
    public AudioSource fireAudio;
    public AudioSource explosionAudio;
    public AudioSource bgAudio;

    private void Start()
    {
        Instance = this;
    }

    public void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.tag == "Enemy")
        {
            Die();
        }
	}
	
	public void Die()
	{
        GameObject explosionPrefab = Instantiate(explosion, transform.position, new Quaternion(90, 180, 0, 0));
        firePrefab = Instantiate(fireParticle, transform.position, new Quaternion(180, 0, 0, 0));

        Destroy(explosionPrefab, 1.0f);

        crashAudio.Play();
        fireAudio.Play();
        explosionAudio.Play();
        bgAudio.Stop();

        destroyables = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject destruct in destroyables)
            Destroy(destruct);

        TrackController.Instance.trackSpeed = 0;
        GameManager.Instance.spawner.SetActive(false);
        OnPlayerDied();
	}
}