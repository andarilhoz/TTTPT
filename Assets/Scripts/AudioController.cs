using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioSource audioControllerPast;
	public AudioSource audioControllerFuture;
	private GameController gm;

	void Start()
	{
		gm = GameObject.Find("GameController").GetComponent<GameController>();
		//ChangeTheme();
	}

	public void Play(){
		audioControllerPast.Play();
		audioControllerFuture.Stop();
	}

	public void ChangeTheme(){
		if(gm.getJogadorAtual().name == "PlayerPassado"){
			audioControllerPast.Play();
			audioControllerFuture.Stop();
		}
		else{
			audioControllerFuture.Play();
			audioControllerPast.Stop();
		}
	}


	
}
