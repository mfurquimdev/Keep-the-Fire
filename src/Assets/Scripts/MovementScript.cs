using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementScript : MonoBehaviour {
	private float speed = 400f;
	private float forceJump;
	private float limitJump = 1000f;
	private bool gameOver = false;
	private bool jump;
	private float time = 0;
	public static float burnedWood = 0;
	public Texture2D aviso;
	
	public ArrayList woodCollected;
	
	public Texture2D textura;
	public Texture2D texturaInvertida;
	public Texture2D texturaPeso;
	public Texture2D texturaPesoInvertida;
	public Material playerNormal;
	public Material playerMadeira;
	public Material woodCut;
	public AudioSource woodCutSound;
	public AudioSource passoAudio;
	
	private bool meleeAttackState;
	//Inputs
	private Vector3 InputRotation;
	private Vector3 InputMovement;
	//Elements of Rotation
	private Vector3 tempVector;
	private Vector3 tempVector2;
	//Animation Elements
	public float animationframeRate = 11f;
	public float walkAnimationMin = 1;
	public float walkAnimationMax = 3;
	public float standAnimationMin = 2;
	public float standAnimationMax = 2;
	private float frameNumber = 1;
	private float currentanimation = 1;
	private float animationStand = 0;
	private float animationWalk = 1;
	public float spriteSheetColumns = 3;
	public float spriteSheetRows = 1;
	private Vector2 spriteSheetCount;
	private Vector2 spriteSheetOffSet;
	private float animationTime = 0f;
	private int i = 0;
	
	
	// Use this for initialization
	void Start () {
		woodCollected = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			Movement ();
			PlayerInput ();
			HandleAnimation ();
			if(jump){
				Physic ();
			}
			if (ScoreScript.fogueira > 0) {
				ScoreScript.fogueira -= Time.deltaTime;
			} else {
				ScoreScript.fogueira = 0;
				gameOver = true;
			}
		}
	}
	
	void PlayerInput()
	{
		InputMovement = new Vector3(Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
	}
	
	
	void Movement(){
		if(Input.GetKey (KeyCode.D)){
			if(woodCollected.Count == 0){
				gameObject.renderer.material = playerNormal;
				gameObject.renderer.material.mainTexture = textura;
				gameObject.renderer.material = playerNormal;
				walkAnimationMax = 3;
				spriteSheetColumns = 3;
			} else{
				gameObject.renderer.material = playerMadeira;
				gameObject.renderer.material.mainTexture = texturaPeso;
				gameObject.renderer.material = playerMadeira;
				walkAnimationMax = 4;
				spriteSheetColumns = 4;
			}
			transform.Translate (speed * Time.deltaTime, 0, 0);
		}
		if(Input.GetKey (KeyCode.A)){
			if(woodCollected.Count == 0){
				gameObject.renderer.material = playerNormal;
				gameObject.renderer.material.mainTexture = texturaInvertida;
				walkAnimationMax = 3;
				spriteSheetColumns = 3;
			} else{
				gameObject.renderer.material = playerMadeira;
				gameObject.renderer.material.mainTexture = texturaPesoInvertida;
				walkAnimationMax = 4;
				spriteSheetColumns = 4;
			}
			transform.Translate (-speed * Time.deltaTime, 0, 0);
		}
		if(Input.GetKey (KeyCode.W) && !jump){
			jump = true;
		}
	}
	
	void Physic(){
		time += Time.deltaTime;
		
		if (time < 0.01) {
			transform.Translate (0, forceJump * Time.deltaTime, 0);
		}
		else {
			forceJump -= limitJump * Time.deltaTime;
			transform.Translate (0, forceJump * Time.deltaTime, 0);
		}
				
	}
	
	void DropWood()
	{
		while(woodCollected.Count > 0)
		{
			GameObject woodDropped = woodCollected[woodCollected.Count-1] as GameObject;
			woodCollected.Remove(woodDropped);
			speed += 60f;
			limitJump += 200;
			ScoreScript.score += 10;
			ScoreScript.fogueira += 15;
			ScoreScript.madeira -= 1;
			Destroy (woodDropped);
		}
		
	}
	
	void CollectWood(Collider other)
	{
		other.gameObject.renderer.material = woodCut;
		woodCutSound.Play();
		//Destroy (other.gameObject);
		woodCollected.Add(other.gameObject);
		this.speed -= 60f;
		this.limitJump -= 200f;
		ScoreScript.score += 10f;
		ScoreScript.madeira += 1f;
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Ground"){
			jump = false;
			time = 0;
			forceJump = 300;
		}
	}
	
	//troquei a colisao anterior por uma colisao trigger (não tem contato fisico).
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("OnTriggerEnter");
		if(other.gameObject.tag == "Wood" && woodCollected.Count < 3){
			CollectWood(other);
		}
		if(other.gameObject.tag == "Fogueira" && woodCollected.Count > 0) {
			DropWood();
		}
		if(other.gameObject.tag == "GameOver"){
			gameOver = true;
		}
	}
	
	void Inicio(){
		gameOver = false;
		this.transform.position = new Vector3(0,65,0);
		ScoreScript.score = 0;
		ScoreScript.madeira = 0;
		ScoreScript.fogueira = 30;
	}
	
	void OnGUI(){
		GUI.TextArea (new Rect(100, 36, 120, 22), "Score:    " + ScoreScript.score + " R$");
		GUI.TextArea (new Rect(100, 60, 120, 22), "Fogueira: " + ScoreScript.fogueira.ToString("0.0") + " Kj ");
		GUI.TextArea (new Rect(100, 84, 120, 22), "Madeira:  " + ScoreScript.madeira + " Kg");

		if(gameOver){
			GUI.TextArea (new Rect(Screen.width/2 - 55, Screen.height/2, 110, 20), "Reiniciando jogo.");
			if(GUI.Button (new Rect(Screen.width/2 - 25, Screen.height/2 + 26, 50, 20), "OK")){
				Inicio();
			}
		}
	}
	
	
	void HandleAnimation()
	{
		FindAnimation();
		ProcessAnimation();
	}
	
	void FindAnimation()
	{
		if(InputMovement.magnitude > 0)
		{
			currentanimation = animationWalk;
			passoAudio.Play ();
		}
		else
		{
			currentanimation = animationStand;
			passoAudio.Stop ();
		}
	}
	
	void ProcessAnimation()
	{
		animationTime -= Time.deltaTime;
		
		if(animationTime <= 0)
		{
			frameNumber += 1;
			
			if(currentanimation == animationStand)
			{
				frameNumber = Mathf.Clamp (frameNumber, standAnimationMin, standAnimationMax+1);
				if(frameNumber > standAnimationMax)
				{
					frameNumber = standAnimationMin;
				}
			}			
			if(currentanimation == animationWalk)
			{
				frameNumber = Mathf.Clamp (frameNumber, walkAnimationMin, walkAnimationMax+1);
				if(frameNumber > walkAnimationMax)
				{
					frameNumber = walkAnimationMin;
				}
			}
			animationTime += (1/animationframeRate);
		}
		spriteSheetCount.y = 0;
		for(i = (int) frameNumber; i > 5; i-=5)
		{
			spriteSheetCount.y += 1;
		}
		spriteSheetCount.x = i - 1;
		spriteSheetOffSet = new Vector2(1 - (spriteSheetCount.x/spriteSheetColumns),
			1 - (spriteSheetCount.y/spriteSheetRows));
		renderer.material.SetTextureOffset ("_MainTex", spriteSheetOffSet);
	}
	
	
	
}
