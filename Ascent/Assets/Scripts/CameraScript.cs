using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public Transform player;
	private Camera cam;
	private Player_control playerC;
	// Use this for initialization
	void Start () {
		cam = gameObject.GetComponent<Camera> ();
		playerC = player.GetComponent<Player_control> ();
	}
	
	// Update is called once per frame
	void Update () {
		//		Debug.Log (player.transform.position.x);
		//		Debug.Log (this.transform.position.x + 3f*cam.aspect);
		if (player.position.x > cam.transform.position.x+ cam.aspect) {
			this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(player.position.x,this.transform.position.y, this.transform.position.z),Time.deltaTime);
		}
		if (player.position.x < cam.transform.position.x - 5*cam.aspect) {
			player.rigidbody2D.velocity = (new Vector2(3f, player.rigidbody2D.velocity.y));
			playerC.needsFlip = true;
			StartCoroutine(flipper());
		}
	}
	IEnumerator flipper(){
		yield return new WaitForSeconds (0.3f);
		playerC.needsFlip = false;
	}
	
}
