using UnityEngine;
using System.Collections;

public class PressurePlateDown : MonoBehaviour {
	private bool isInContact = false;
	private Collider2D plate;
	// Use this for initialization
	//Put onto the player
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnCollisionEnter2D(Collision2D Coll)
	{
		//Introduce a plate, to improve readability
		//Coll.collider being called constantly can be annoying
		plate = Coll.collider;
		//Move the plate
		if (plate.gameObject.tag == "PressurePlate") 
		{
			if (!isInContact) 
			{
				//Move the plate relative to the player, main speed will be
				// Acceleration of the object will be Fplayer (F = Mg) - Normal Force of the plate (Fn = Mg)
				//Pressure plate will need a rigidBody to work, this Body's mass will work as the resistance to
				plate.gameObject.transform.Translate (Vector2.up * (this.rigidbody2D.mass*this.rigidbody2D.gravityScale*9.8f - plate.rigidbody2D.mass*plate.rigidbody2D.gravityScale*9.8f)*(Time.deltaTime-0.001f)*(-1.0f));
				//stretchPos = plate.gameObject.transform.position.y;
				//Required to prevent the plate from moving down permanently.
				isInContact = true;
			}
		} 
	}
	public void OnTriggerEnter2D(Collider2D Coll)
	{
		if(isInContact&&Coll.gameObject.tag=="PressurePlateExit"&&plate != null)
		{
			//Simple, buggy reverse motion code
			plate.gameObject.transform.Translate (Vector2.up * (this.rigidbody2D.mass*this.rigidbody2D.gravityScale*9.8f - plate.rigidbody2D.mass*plate.rigidbody2D.gravityScale*9.8f)*(Time.deltaTime-0.001f));
			isInContact = false;
		}
	}
}