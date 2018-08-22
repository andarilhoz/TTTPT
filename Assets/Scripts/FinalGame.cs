using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGame : MonoBehaviour {

	private float timeout = 5;
	// Update is called once per frame
	void Update () {
		if(timeout <=0 )
			if(Input.anyKey){
				 SceneManager.LoadScene("MainGame");
			}
		timeout -= Time.deltaTime;
	}

}
