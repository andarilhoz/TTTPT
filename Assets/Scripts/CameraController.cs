using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Tempos posicao;
	public float ySmoothTime = 1.5F;
	private Vector3 destino;
	public float distanciaCameraParedes = 13;
	public GameObject paredeEsquerda;
	public GameObject paredeDireita;

	void Start() {
		posicao = Tempos.PASSADO;
	}
	
	void FixedUpdate () {
		Vector3 novaPosicao = Vector3.Lerp(transform.position, destino, Time.deltaTime * ySmoothTime);
		transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, GetLimiteHorizontalEsquerda(), GetLimiteHorizontalDireita()), novaPosicao.y, novaPosicao.z);
	}

	public void ChangeTarget(Vector3 posicao) {
		this.destino = new Vector3(posicao.x, posicao.y + 2, transform.position.z);
	}

	private float GetLimiteHorizontalEsquerda() {
		return paredeEsquerda.transform.position.x + distanciaCameraParedes;
	}

	private float GetLimiteHorizontalDireita() {
		return paredeDireita.transform.position.x - distanciaCameraParedes;
	}
}
