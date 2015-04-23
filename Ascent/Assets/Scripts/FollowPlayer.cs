using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform target;
	public float xOffset = 0f;
	public float yOffset = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (target.position.x - xOffset, target.position.y - yOffset, -10);
	}
}
