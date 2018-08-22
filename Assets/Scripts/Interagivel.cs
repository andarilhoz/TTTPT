using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagivel : MonoBehaviour {
	public Animator animator;
	public bool ativo = false;
	private bool desabilitado = false;

	void Start()
	{
		if(ativo){
			animator.SetTrigger("acao");
		}
	}

	public void Interage() {
		if(!desabilitado) {
			animator.SetTrigger("acao");
			ativo = !ativo;
		}
	}

	public void Desabilitar() {
		desabilitado = true;
	}

}
