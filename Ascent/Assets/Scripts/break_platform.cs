using UnityEngine;
using System.Collections;

/*
 * Created: 3/26
 * Edited: 3/27 (Zack)
 * Changes:
 * Current bugs: 
 * 		The timer will start no matter where you hit the object from.
 * 		(ie, if you jump into it from below it will still go off)
 * */

public class break_platform : MonoBehaviour {
	public float breakTime = 2.0f;

	public bool steppedOn;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if the platform has been stepped on, this will destroy the object
		//after breakTime seconds.
		if(this.steppedOn)
		{
			breakTime -= Time.deltaTime;
			if(breakTime <= 0)
			{
				Destroy(gameObject);
				//animation shit for the platform breaking
			}
		}
	}

	//checks collision
	void OnCollisionEnter2D (Collision2D col)
	{
		steppedOn = true;
	}
}
