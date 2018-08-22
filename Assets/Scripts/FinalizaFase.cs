using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalizaFase : MonoBehaviour {
	public string fase;

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject.Find("GameController").GetComponent<GameController>().Final();
		//Application.Quit();

	}
}
