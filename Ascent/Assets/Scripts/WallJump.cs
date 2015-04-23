using UnityEngine;
using System.Collections;
// create an empty object with a 2dboxCollider and atatch it to the player. make sure the box is on the front of the player(whatever direction he is facing)
//make sure player is tagged "Player"
public class WallJump : MonoBehaviour {
	public static bool onWall = false;
	public float xJumpStrength = 50f;
	public float wallJumpX;
	public float wallJumpY = 50f;
	float tempI;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		tempI = player.rigidbody2D.gravityScale;
	}
	// Update is called once per frame
	void Update () {
		if (Player_control.facingRight) {
			wallJumpX = -xJumpStrength;
		}
		if (!Player_control.facingRight) {
			wallJumpX = xJumpStrength;
		}
	}
	void OnTriggerStay2D (Collider2D other)
	{
		//bool jumpedYet = false;
		if (!Player_control.grounded) {
			if (other.gameObject.tag == "wall") {
				if(Input.GetKey (KeyCode.W)){
				onWall = true;
				Player_control.grounded = false;
				player.rigidbody2D.velocity = new Vector2(0,0);
				player.rigidbody2D.gravityScale = 0;
				}
				if(Input.GetKeyUp (KeyCode.W)){
					onWall = false;
					player.rigidbody2D.gravityScale = 2;
				}
				if (Input.GetKey (KeyCode.Space)) {
					onWall = false;
					player.rigidbody2D.gravityScale = 2;
					player.GetComponent<Rigidbody2D> ().AddForce (new Vector3 (wallJumpX, wallJumpY, 0f));
					player.GetComponent<Player_control> ().Flip ();
				}
			}
		}
	}
}