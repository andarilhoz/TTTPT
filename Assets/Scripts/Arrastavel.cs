using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrastavel : MonoBehaviour {

	// Use this for initialization
	public bool podeCarregar = true;

	void Start () {
		IgnoraPlayer();
	}

	public IEnumerator Soltou(){
		podeCarregar = false;
		yield return new WaitForSeconds(0.2f);
		podeCarregar = true;
	}

	public void IgnoraPlayer(){
		GameObject[] playerCollider = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject gameObj in playerCollider){
			Collider2D collider =  gameObj.GetComponentInChildren<Collider2D>();
			Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
		}   
		
	}

}
