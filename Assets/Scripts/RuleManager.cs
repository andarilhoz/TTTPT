using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour {

	public GameObject alavancaPassado1;
	public GameObject alavancaPassado2;
	public GameObject alavancaPassado3;
	public GameObject alavancaFuturo1;
	public GameObject alavancaFuturo2;
	public GameObject alavancaFuturo3;
	public GameObject portaoPassado;
	public GameObject portaoFuturo;
	private bool[] sequenciaCorreta;
	private GameObject[] alavancasPassado;
	private GameObject[] alavancasFuturo;

	private bool portaoAbriu;

	public AudioSource portaoSom;
	// Use this for initialization
	void Start () {
		AgruparAlavancas();
		LevelSetup();
	}
	
	// Update is called once per frame
	void Update () {
		VerificaSegredo();
	}

	void LevelSetup() {
		sequenciaCorreta = GerarSequenciaCorreta();
		for (int i = 0; i < sequenciaCorreta.Length; i++) {
			if (sequenciaCorreta[i]) alavancasPassado[i].GetComponent<Interagivel>().Interage();
			alavancasPassado[i].GetComponent<Interagivel>().Desabilitar();
		}
		portaoPassado.GetComponent<Animator>().SetTrigger("Abrir");
	}

	void VerificaSegredo() {
		if (alavanca1EstaCorreta() && alavanca2EstaCorreta() && alavanca3EstaCorreta()) {
			for (int i = 0; i < alavancasFuturo.Length; i++) {
				alavancasFuturo[i].GetComponent<Interagivel>().Desabilitar();
			}
			portaoFuturo.GetComponent<Animator>().SetTrigger("Abrir");
			if(!portaoAbriu){
				portaoAbriu = true;
				portaoSom.Play();
			}
		}
	}

	void AgruparAlavancas() {
		alavancasPassado = new GameObject[]{alavancaPassado1, alavancaPassado2, alavancaPassado3};
		alavancasFuturo = new GameObject[]{alavancaFuturo1, alavancaFuturo2, alavancaFuturo3};
	}

	bool[] GerarSequenciaCorreta() {
		
		bool[] sequencia;
		
		do {
			sequencia = new bool[]{Random.Range(0.0f, 1.0f) > 0.5, Random.Range(0.0f, 1.0f) > 0.5, Random.Range(0.0f, 1.0f) > 0.5};
		} while(sequencia[0] == false && sequencia[1] == false && sequencia[2] == false);
		
		return sequencia;
	}

	bool alavanca1EstaCorreta() {
		return alavancasFuturo[0].GetComponent<Interagivel>().ativo == alavancasPassado[0].GetComponent<Interagivel>().ativo;
	}
	bool alavanca2EstaCorreta() {
		return alavancasFuturo[1].GetComponent<Interagivel>().ativo == alavancasPassado[1].GetComponent<Interagivel>().ativo;
	}
	bool alavanca3EstaCorreta() {
		return alavancasFuturo[2].GetComponent<Interagivel>().ativo == alavancasPassado[2].GetComponent<Interagivel>().ativo;
	}
}
