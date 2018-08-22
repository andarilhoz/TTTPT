using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float velocidade;

	public AudioClip steps;
	public AudioClip lever;

	public AudioSource playerAudio;
	public GameController gameController;
	public Animator playerAnimator;
	public GameObject pontoCarrega;
	public SpriteRenderer rd;
	private Rigidbody2D rb;

	private bool olhandoDireita;
	private Transform carga;
	private bool interagindo = false;

	private bool podeLargar = true;

	private bool carregando = false;


	private CameraController cameraController;
	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		rb = transform.GetComponent<Rigidbody2D>();
		cameraController = GameObject.Find("Main Game Camera").GetComponent<CameraController>();
	}
	void Update () {
		if(Input.GetAxis("Jump") == 1)
			ViajaNoTempo();	
		

		if(carga != null) 
			if(Input.GetAxis("Fire1") == 1 && podeLargar) 
				StartCoroutine(Soltar());


		MoveJogador();
	}

	
	void MoveJogador() {
		float direcao = Input.GetAxisRaw("Horizontal");
		if(direcao != 0){
			playerAnimator.SetBool("andando",true);
			playerAudio.clip = steps;
			if(!playerAudio.isPlaying)
				playerAudio.Play();
			olhandoDireita = direcao >= 0.01 ? false : true;
		}
		else{
			if(playerAudio.clip == steps)
				playerAudio.Stop();
			
			if(playerAnimator.gameObject.activeSelf)
				playerAnimator.SetBool("andando",false);
		}

		if(carga != null)
			carga.position = new Vector3(pontoCarrega.transform.position.x + (olhandoDireita ? -0.25f : 0), carga.position.y, carga.position.z);

		rd.flipX =  olhandoDireita;
		rb.velocity = new Vector2(direcao * velocidade, rb.velocity.y);
		cameraController.ChangeTarget(gameController.jogadorAtual.transform.position);
	}

	 void ViajaNoTempo() {
		gameController.toggleJogadorAtual();
	}
	
	IEnumerator Agarrar(Collider2D other) {
		carregando = true;
		playerAnimator.SetBool("carregando",true);
		other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		other.transform.parent = this.transform;
		other.transform.position = pontoCarrega.transform.position;
		yield return new WaitForSeconds(0.2f);
		carga = other.transform;
	}

	IEnumerator Soltar(){
		carregando = false;
		playerAnimator.SetBool("carregando",false);
		StartCoroutine(carga.GetComponent<Arrastavel>().Soltou());
		carga.position = new Vector3(carga.position.x, carga.position.y, 1);
		carga.parent = null;
		carga.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		carga = null;
		yield return new WaitForSeconds(0.2f);
	}
	bool SouEu(){
		return gameController.getJogadorAtual().transform.name == transform.name;
	}

	bool EstaParado(){
		return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("tyler_idle") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("tyler_idle_with");
	}

	IEnumerator Interage(Transform other) {
		Interagivel objeto = other.GetComponent<Interagivel>();
		playerAnimator.SetTrigger("acao");
		playerAudio.clip = lever;
		playerAudio.Play();
		yield return new WaitForSeconds(0.5f);
		objeto.Interage();
		interagindo = false;
	}

	IEnumerator ColocaItemNoLugar(Transform lugar){
		carregando = false;
		playerAnimator.SetTrigger("acao");
		playerAnimator.SetBool("carregando",false);
		carga.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		carga.transform.parent = lugar;
		StartCoroutine(carga.GetComponent<Arrastavel>().Soltou());
		yield return new WaitForSeconds(0.2f);
		carga.transform.localPosition = Vector3.zero;
		carga.transform.localPosition = carga.transform.localPosition - Vector3.forward;
		carga = null;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.tag == "ColoqueItem")
			podeLargar = false;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.transform.tag == "ColoqueItem")
			podeLargar = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if(Input.GetAxis("Fire1") == 1){
			if (other.transform.tag == "Arrastavel") {
				if (!carregando)
					if(other.GetComponent<Arrastavel>().podeCarregar)
						StartCoroutine(Agarrar(other));
			}

			else if(other.transform.tag == "Interagivel" && interagindo == false){
				interagindo = true;
				StartCoroutine(Interage(other.transform));
			}

			else if(other.transform.tag == "ColoqueItem")
				if(carregando && carga !=null){
					StartCoroutine(ColocaItemNoLugar(other.transform)); 
				}
		}
	}
	
}


