using UnityEngine;
using UnityEngine.Networking;

public class Powerup : NetworkBehaviour {

	public float moveSpeed = 5f;

	private bool up = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(up){
			transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
		}else{
			transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
		}
		if(transform.localPosition.y > 2){
			up = false;
		}
		if(transform.localPosition.y < 0){
			up = true;
		}
		
	}
}
