using UnityEngine;
using System.Collections;
/*create 2 empty objects one with a 2d box collider (make sure to check the is trigger box)
set the one with the box collider under the platform, this is the death box
set the other one at the place you want to spawn
add a spawn tag to the spawn gameobject and Player tag to your player
put this code onto the death box*/
public class death_respawn : MonoBehaviour {
	Vector3 spawnPoint;
	public int a = 0;
	// Use this for initialization
	void Start () {
		GameObject spawnObject = GameObject.FindWithTag("Respawn");
		spawnPoint = spawnObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player")
		{
			other.transform.position = spawnPoint;
			a = 1;
		}
	}
}
