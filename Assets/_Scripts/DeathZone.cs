using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	public int pNumber;
	public GameObject deathZoneParticles;
	public Material particleColor;

	public GameObject deathZoneSound;
	private GameManager gameManager;
	private Vector3 originalCameraPos;
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Death zone triggered!");

		
		if (other.gameObject.CompareTag("Ball"))
		{
			// create particle effect
			GameObject particles = Instantiate(deathZoneParticles);

			// set color of particles to match player colors
			ParticleSystem.MainModule main = particles.GetComponent<ParticleSystem>().main;
			main.startColor =  particleColor.color;

			particles.transform.position = other.gameObject.transform.position;


			/* code from https://discussions.unity.com/t/how-do-i-make-the-audio-louder/115349/4
			 * basically makes the audio much closer to the camera while partially using the actual position 
			 * this was used because the audio was far too quiet with using the collision point as the actual position
			 * a prefab was used so the doppler effect could be set to 0 as the screenshake was distorting the sound */
			GameObject sound = Instantiate(deathZoneSound);
			sound.transform.position = .9f*originalCameraPos + 0.1f*other.gameObject.transform.position;

			CameraEffects.Shake(.5f, true);

			// destroy the ball
			Destroy(other.gameObject);
		
			// new ball
			GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

			// add one to other players score
			gameManager.IncrementScore(pNumber);

			Destroy(particles, 5);
			Destroy(sound, 5);
		}
	}

	void Start()
	{
		originalCameraPos = Camera.main.transform.position;
	}
}
