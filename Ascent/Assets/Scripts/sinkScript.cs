using UnityEngine;
using System.Collections;

public class sinkScript : MonoBehaviour {
	
	public float wait = 1.0f;
	public float sinkSpeed = 5.0f;
	bool sink = false;
	float sinkTime;
	float replaceTime;
	public float respawnDelay = 5.0f;
	Vector3 startPosition;
	// Use this for initialization
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			if (!sink){
				replaceTime = Time.time + respawnDelay;
				sinkTime = Time.time + wait;
			}
			sink = true;
		}
	}
	
	
	void Start () {
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (sinkTime < Time.time && sink) {
			this.rigidbody2D.velocity = Vector3.down * sinkSpeed;
		} else if (!sink) {
			this.rigidbody2D.velocity = Vector3.zero;
		}
		if (replaceTime < Time.time) {
			sink = false;
			this.gameObject.transform.position = startPosition;
		}
	}
}