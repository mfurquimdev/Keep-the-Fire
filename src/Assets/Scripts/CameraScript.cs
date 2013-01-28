using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	const float zoomPadrao = 9.28f;
	const float zoomLonge = 14f;
	const float zoomperto = 6;
	const float zoomSpeed = 2;
	
	//private float timeImpulso = 0;
	
	private string ultimoZoom;
	
	public GameObject cameraObj;
	
	enum EstadoCamera{
		estatica,
		aproximando,
		distanciando,
		ajustaFoco,
	}
	
	EstadoCamera estadocamera;
	
	// Use this for initialization
	void Start () {
	estadocamera = EstadoCamera.estatica;
	}
	
	// Update is called once per frame
	void Update () {
		switch(estadocamera){
			case EstadoCamera.estatica:
			break;
			
			case EstadoCamera.aproximando:
				Distanciando ();
			break;
			
			case EstadoCamera.distanciando:
				Aproximando ();
			break;
			
			case EstadoCamera.ajustaFoco:
				AjustaFoco ();
			break;
		}
	}
	
	void Distanciando(){
		if(cameraObj.camera.orthographicSize < zoomLonge){
			cameraObj.camera.orthographicSize += zoomSpeed * Time.deltaTime;
			ultimoZoom = "Distanciando";
		} else {
			estadocamera = EstadoCamera.estatica;
		}
	}
	
	void Aproximando(){
		if(cameraObj.camera.orthographicSize > zoomperto){
			cameraObj.camera.orthographicSize -= zoomSpeed * Time.deltaTime;
			ultimoZoom = "Aproximando";
		} else {
			estadocamera = EstadoCamera.estatica;
		}
	}
	
	void AjustaFoco(){
		switch (ultimoZoom){
			case "Aproximando":
				if(cameraObj.camera.orthographicSize < zoomPadrao){
					cameraObj.camera.orthographicSize += zoomSpeed * Time.deltaTime;
				} else {
					cameraObj.camera.orthographicSize = zoomPadrao;
				}
			break;
			
			case "Distanciando":
			if(cameraObj.camera.orthographicSize > zoomPadrao){
					cameraObj.camera.orthographicSize -= zoomSpeed * Time.deltaTime;
				} else {
					cameraObj.camera.orthographicSize = zoomPadrao;
				}
			break;
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Camera+z"){
			estadocamera = EstadoCamera.aproximando;
			Destroy (other.gameObject);
		}
		
		if(other.gameObject.tag == "Camera-z"){
			estadocamera = EstadoCamera.distanciando;
			Destroy (other.gameObject);
		}
		
		if(other.gameObject.tag == "CameraAjustaFoco"){
			estadocamera = EstadoCamera.ajustaFoco;
			Destroy (other.gameObject);
		}
	}
	
}
