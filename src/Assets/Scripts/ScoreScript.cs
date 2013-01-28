using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	
	public static float score = 0;
	public static float madeira = 0;
	public static float fogueira = 30;
	
	enum TypeWood{
		small,
		big,
	}
	TypeWood typeWood; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Wood"){
			Destroy (other.gameObject);
			score += 10;
			print (gameObject.rigidbody.mass);
		}
	}*/
}
