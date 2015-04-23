using UnityEngine;
using System.Collections;
public class Player_control: MonoBehaviour {
	public GameObject hook;
	public HookSpot hookSpot;
	public AudioClip jump;
	public AudioSource source;
	public float speed = 6.0f; //horizontal speed
	public float jumpStrength = 50f; //jump strength
	public float jumpSpeed = 10f; //jump velocity (10 jumpSpeed ~ 500 jumpForce)
	public float airForce = 10f;
	public float hookSpeed = 6.0f;
	public float noise = 5.0f;
	public static bool grounded = false;
	public static bool facingRight = true;
	public bool goingUp = false;
	private Animator animator;
	public LayerMask groundMask;	//for detecting Ground layer
	public float groundDist;
	public float groundTestDist;
	public float distToSide;
	public string terrain;			//type of terrain player is standing/walking on
	public bool needsFlip = false;
	
    //NOTE TEST

	// Use this for initialization
	void Start (){
		animator = GetComponent<Animator> ();
		//Sound code
		source = GetComponent<AudioSource> ();
		
		//finds appropriate distance for detecting ground
		groundDist = this.collider2D.bounds.extents.y + .02f;
		distToSide = this.collider2D.bounds.extents.x;
	}
	// Update is called once per frame
	void Update () {
		/*
		* Controls
		* A = move left
		* D = move right
		* Spacebar = jump
		* Middle mouse = neutral
		* Left mouse = attract
		* Right mouse = repel
		*/
		
		CheckGround();
		Grapple();
		/* Note: During grapple, animation is just a continuation of whatever animation
			 * was being used previously. Specifically, when falling after walking off a ledge,
			 * the player uses the walking animation, so the grapple animation is just the 
			 * player continuing to walk.
			 */
		Movement();
	}
	//FLIPS THE CHARACTER AND SETS facingRight TO THE CORRECT VALUE
	public void Flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	public void GoUp(){
		if (this.transform.position.y < hookSpot.grappleLoc.y + 1.5f) {
			this.transform.position = Vector3.Lerp (this.transform.position, new Vector3 (hookSpot.grappleLoc.x, hookSpot.grappleLoc.y + 4f, this.transform.position.z), Time.deltaTime * 4f);
			this.collider2D.enabled = false;
		} else {
			goingUp = false;
			this.collider2D.enabled = true;
		}
	}
	
	//New GroundCheck method looks for "Ground" layer, and uses the tag for terrain type
	void CheckGround(){
		//checks for ground under center of player
		Vector2 myPos = this.rigidbody2D.position;
		RaycastHit2D hitInfo = Physics2D.Raycast (myPos, -Vector2.up, groundDist, groundMask);
		Debug.Log ("Object hitting: " + hitInfo.collider);
		if (hitInfo.collider != null) {
			grounded = true;
			groundTestDist = hitInfo.distance;
		} 
		else {
			//checks for ground under left edge of player
			myPos.x = myPos.x - distToSide;
			hitInfo = Physics2D.Raycast (myPos, -Vector2.up, groundDist, groundMask);
			if (hitInfo.collider != null) {
				grounded = true;
			}
			else{
				//checks for ground under right edge of player
				myPos.x = myPos.x + (2 * distToSide);
				hitInfo = Physics2D.Raycast (myPos, -Vector2.up, groundDist, groundMask);
				if (hitInfo.collider != null) {
					grounded = true;
				}
				else{
					//sets grounded to false if player is not on Ground
					grounded = false;
				}
			}
		}
		
		//check type of terrain player is standing on
		if (grounded) {
			//terrain tag examples
			if(hitInfo.collider.tag == "Snow"){
				terrain = "Snow";
			}
			else if (hitInfo.collider.tag == "Stone"){
				terrain = "Stone";
			}
			else {
				//default terrain type, in case tag is being used for something else, such as Pushable
			}
		}
	}
	
	void Movement(){
		//MOVEMENT CODE TO FOLLOW
		//enforces a max speed (useful for swinging and mid air add force)
		if (rigidbody2D.velocity.x <= speed && rigidbody2D.velocity.x >= -1* speed && !goingUp) {
			//BEGINNING OF MOVE LEFT CODE
			if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
				//move left speed when on the ground and not hooked
				if (grounded && !hook.activeSelf && !needsFlip) {
					var vel = GetComponent<Rigidbody2D> ().velocity;
					vel.x = -1 * speed;
					GetComponent<Rigidbody2D> ().velocity = vel;
					animator.SetInteger("AnimState", 1);
				}
				//move left add force if not hooked and in mid air
				else if(!hook.activeSelf){
					this.rigidbody2D.AddRelativeForce (new Vector2 (-1 * airForce, 0));
				}
				//flip character if not facing left and not hooked
				if(facingRight && !hook.activeSelf && this.rigidbody2D.velocity.x < 0){
					Flip();
				}
				//swing left force add
				if (hook.activeSelf) {
					this.rigidbody2D.AddRelativeForce (new Vector2 (-1 * hookSpeed, 0));
				}
			}//END OF MOVE LEFT CODE
			//ALL GO RIGHT CODE (PRESS D) TO FOLLOW
			else if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {
				//move right speed when on the ground and not hooked
				if (grounded && !hook.activeSelf) {
					var vel = GetComponent<Rigidbody2D> ().velocity;
					vel.x = speed;
					GetComponent<Rigidbody2D> ().velocity = vel;
					animator.SetInteger("AnimState", 1);
				}
				//move right add force if not hooked and in mid air
				else if (!hook.activeSelf) {
					this.rigidbody2D.AddRelativeForce (new Vector2 (airForce, 0));
				}
				//flip character if not facing right and not hooked
				if(!facingRight && !hook.activeSelf && this.rigidbody2D.velocity.x > 0){
					Flip();
				}
				//swing right force add
				if (hook.activeSelf) {
					this.rigidbody2D.AddRelativeForce (new Vector2 (hookSpeed, 0));
				}
			}//END OF MOVE RIGHT CODE
			//STOP HORIZONTAL MOVEMENT IF NOT MOVING WHILE GROUNDED
			else if(!(hook.activeSelf) && grounded && !needsFlip){
				var vel = GetComponent<Rigidbody2D>().velocity;
				vel.x = 0;
				GetComponent<Rigidbody2D>().velocity = vel;
				animator.SetInteger("AnimState", 0);
			}
		}
		//JUMP CODE
		if (grounded && Input.GetKey (KeyCode.W) && !goingUp) {
			animator.SetInteger("AnimState", 2);
			source.PlayOneShot (jump, noise);
			//this.GetComponent<Rigidbody2D>().AddForce (new Vector3 (0f, jumpStrength, 0f));
			var vel = GetComponent<Rigidbody2D>().velocity;
			vel.y = jumpSpeed;
			GetComponent<Rigidbody2D>().velocity = vel;
			grounded = false;
		}//END OF JUMP CODE
		//END OF MOVEMENT CODE
	}
	
	void Grapple (){
		//BEGINNING OF GRAPPLE CODE (OUTSIDE OF WHAT IS IN THE MOVEMENT CODE)
		if (Input.GetKey (KeyCode.Space) && hookSpot.canGrapple && !grounded && !goingUp) {
			hook.SetActive (true);//enables hook object which will stay in place and is hinge jointed to the player
			GetComponent<Rigidbody2D> ().fixedAngle = false;
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			hook.SetActive(false);//disables the hook object
		}
		//Begin grapple jump up
		if (hook.activeSelf && Input.GetKey (KeyCode.Space) && Input.GetKeyDown (KeyCode.W) && !goingUp) {
			goingUp = true;
			hook.SetActive(false);
		}
		if (goingUp) {
			GoUp();
		}
		//END OF GRAPPLE CODE
		//BEGINS ROTATE BACK OVER TIME CODE
		if (transform.rotation != Quaternion.Euler (0.0f, 0.0f, 0.0f) && grounded) {//ensures correct rotation when you hit the ground
			transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			GetComponent<Rigidbody2D> ().fixedAngle = true;
		}else if (!hook.activeSelf && transform.rotation != Quaternion.Euler (0.0f, 0.0f, 0.0f)) {
			transform.rotation = Quaternion.Lerp (this.transform.rotation, Quaternion.Euler (0.0f, 0.0f, 0.0f), Time.deltaTime * 15f);
		} else if(!hook.activeSelf) {
			GetComponent<Rigidbody2D> ().fixedAngle = true;
		}//END ROTATE BACK OVER TIME CODE
	}
}

/*
 * Line 10: jumpSpeed for velocity-based jumpSpeed
 * Lines 18-21: new variables for GroundCheck method
 * Line 22: new variable for holding current terrain type
 * Lines 31-33: finds edges of Player collider on start
 * Changes in Update: Movement and Grapple code moved to separate methods to clean up Update method
 * Line 73: Start of new GroundCheck method
 * Line 117: start of Movement method
 * Line 186: start of Grapple method
 */
