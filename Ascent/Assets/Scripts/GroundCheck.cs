using UnityEngine;
using System.Collections;
/* first create an empty game object with a 2d box colliders (make sure to check the 'is trigger' box)
 * resize and position the collider box so that it covers the players feet but doesn't reach the left and right edges of the player's collider box
 * make this object a child of the player
 * add this code to the empty game object and make sure all ground surfaces are tagged as 'ground'*/
public class GroundCheck : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "ground" ) {
			Player_control.grounded = true;
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "ground" ) {
			Player_control.grounded = false;
		}
	}
}
