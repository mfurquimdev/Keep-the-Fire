using UnityEngine;
using System.Collections;

public class InitPosition : MonoBehaviour {
	
	private float x;
	private float y;
	private float z;
	
	// Use this for initialization
	void Start () {
		
		z = 0;
		switch (this.gameObject.tag) {
			
		case "Player":
			//120 x ? [gustavo]
			x = 100;
			y = 65;
			break;
			
		case "Fogueira":
			//30 x ? [gustavo]
			x = 40;
			y = 50;
			break;
			
		case "Ground":
			x = 0;
			y = 0;
			break;
		
		case "Veia":
			x = -70;
			y = 52;
			z = -1;
			break;
		}
		
		this.transform.position = new Vector3(x,y,z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
