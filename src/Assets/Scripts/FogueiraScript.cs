using UnityEngine;
using System.Collections;

public class FogueiraScript : MonoBehaviour {
	
	public Material Fogueira0;
	public Material Fogueira1;
	public Material Fogueira2;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ScoreScript.fogueira <= 30) {
			this.gameObject.renderer.material = Fogueira0;
		}
		if (ScoreScript.fogueira > 30 && ScoreScript.fogueira <= 60) {
			this.gameObject.renderer.material = Fogueira1;
		}
		if (ScoreScript.fogueira > 60) {
			this.gameObject.renderer.material = Fogueira2;
		}
	}
}
