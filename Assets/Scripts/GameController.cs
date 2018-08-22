using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject jogadorAtual;

	private GameObject jogadorPassado;
	private GameObject jogadorFuturo;
	private CameraController cameraController;

	private GameObject cameraMenu;
	private GameObject cameraJogo;

	public  GameObject endJogo;

	public float changeTimeout;

	private float timeSinceChange;

	private bool iniciado = false;
	private bool tocafilme = false;

	private float timeoutMovie = 3f;
	public GameObject textInicial;
	public GameObject filmeInicial;

	public Animator logo;

	public GameObject logoObject;


	private AudioController audioController;

	public GameObject getJogadorAtual(){
		return jogadorAtual;
	}

	public void toggleJogadorAtual(){
		if(timeSinceChange <= 0){
			timeSinceChange = changeTimeout;
			if(jogadorAtual.Equals(jogadorFuturo)){
				jogadorAtual = jogadorPassado;
				jogadorPassado.SetActive(true);
				cameraController.ChangeTarget(jogadorAtual.transform.position);
				jogadorFuturo.SetActive(false);
			}else{
				jogadorAtual = jogadorFuturo;
				jogadorFuturo.SetActive(true);
				cameraController.ChangeTarget(jogadorAtual.transform.position);
				jogadorPassado.SetActive(false);
			}
			ArrastavelIgnoraPlayers();
			audioController.ChangeTheme();
		}
	}


	void Start()
	{
		
	}
	void Awake()
	{
		jogadorFuturo = GameObject.Find("PlayerFuturo");
		jogadorPassado = GameObject.Find("PlayerPassado");

		jogadorAtual = jogadorPassado;

		jogadorFuturo.SetActive(false);
		jogadorPassado.SetActive(false);

		cameraController = GameObject.Find("Main Game Camera").GetComponent<CameraController>();
		cameraMenu =  GameObject.Find("Main Menu Camera");
		cameraJogo =  GameObject.Find("Main Game Camera");
		audioController  = GameObject.Find("AudioController").GetComponent<AudioController>();
		GameObject.Find("Main Game Camera").SetActive(false);
		filmeInicial.SetActive(false);
		logoObject.SetActive(true);
	}

	IEnumerator StartMovie(){
		logoObject.SetActive(false);
		textInicial.SetActive(false);
		tocafilme = true;
		filmeInicial.SetActive(true);
		yield return new WaitForSeconds(36);
		IniciaJogo();
		
	}

	public void IniciaJogo(){
		iniciado = true;
		cameraJogo.SetActive(true);
		cameraMenu.SetActive(false);
		jogadorAtual.SetActive(true);
		cameraController.ChangeTarget(jogadorAtual.transform.position);
		ArrastavelIgnoraPlayers();
		audioController.Play();
	}

	void ArrastavelIgnoraPlayers(){
		GameObject[] dg = GameObject.FindGameObjectsWithTag("Arrastavel");
		foreach(GameObject drag in dg){
			drag.GetComponent<Arrastavel>().IgnoraPlayer();
		}
	}

	void Update(){
		if(Input.GetAxis("Submit") == 1)
			if(!tocafilme){
				StartCoroutine(StartMovie());
				logoObject.SetActive(false);
			}
			else if(!iniciado && timeoutMovie <= 0) 
				IniciaJogo();

		if(timeSinceChange>=0)
			timeSinceChange -= Time.deltaTime;

		if(Input.GetAxis("Cancel") == 1)
			Application.Quit();
		
		if(tocafilme)
			timeoutMovie -= Time.deltaTime;
	}

	public void Final(){
		endJogo.SetActive(true);
		cameraJogo.SetActive(false);
	}

}

public enum Tempos {
	PASSADO, FUTURO
}
