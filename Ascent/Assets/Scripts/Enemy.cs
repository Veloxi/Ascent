using UnityEngine;
using System.Collections;

public class Enemy: MonoBehaviour{
	public GameObject enemy;

	public float speed = 0.6f;

	private bool YouShallnotPass;

	void Start(){

	}
	void OnCollisionEnter2D (Collision2D col)
	{
				if (col.gameObject.tag == "wall")
						YouShallnotPass = true;
	}
	void OnCollisonExit2D(Collision2D col){
		YouShallnotPass = false;
	}


	void Update()
	{
			if (YouShallnotPass) {
			this.gameObject.transform.localScale = new Vector3 (-1,1,1);
						var vel = GetComponent<Rigidbody2D> ().velocity;
						vel.x = -1 * speed;
						GetComponent<Rigidbody2D> ().velocity = vel;
				} else {
						var vel = GetComponent<Rigidbody2D> ().velocity;
						vel.x = speed;
						GetComponent<Rigidbody2D> ().velocity = vel;
				}
	}
			
		
}

