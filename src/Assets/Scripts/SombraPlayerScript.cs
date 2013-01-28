using UnityEngine;
using System.Collections;

public class SombraPlayerScript : MonoBehaviour {
	
	public Transform playerObj;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(playerObj.position.x - 160, 100, -40);
	}
}
