using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public float startSpeed = 5.0f;
	public GameObject wallPaddleCollParticles;
	public AudioClip collisionSound;
    // Start is called before the first frame update
	private Vector3 originalCameraPos;
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Paddle"))
		{
			GameObject particles = Instantiate(wallPaddleCollParticles);

			Vector3 point = collision.GetContact(0).point;
			particles.transform.position = point;

			/* code from https://discussions.unity.com/t/how-do-i-make-the-audio-louder/115349/4
			 * basically makes the audio much closer to the camera while partially using the actual position 
			 * this was used because the audio was far too quiet with using the collision point as the actual position */
			AudioSource.PlayClipAtPoint(collisionSound, .9f*originalCameraPos + 0.1f*point ,10f);

			CameraEffects.Shake(.2f, false);

			Destroy(particles, 1);
		}
	}
    void Start()
    {
        // transform.position = new Vector3(1, 1, 1);
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			// pick a random angle
			float angle = Random.Range(40, 140);

			float rand = Random.value;

			int direction = rand <= 0.5f ? -1 : 1;
			
			// make the ball point in that direction on the playing field
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.up * direction);
			// move the ball forward at start speed
			
			rb.velocity = transform.forward * startSpeed;
		}

		originalCameraPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale *= 1.01f;
    }
}
