using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovimentarPersonagem : MonoBehaviour
{

    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 6f;
    public float gravidade = -20f;

    public Transform checaChao;
    public float raioEsfera = 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;

    Vector3 velocidadeCai;

    public Transform cameraTransform;
    private bool estahAbaixado = false;
    private bool levantarBloqueado;
    public float alturaLevantado, alturaAbaixado, posicaoCameraEmPe, posicaoCameraAbaixado;
    public AudioClip somPulo;
    private AudioSource audioSrc;
    public AudioClip somPassos;

    private int vida = 100;
    public Slider sliderVida;

    public bool estahVivo = true;


    void Start()
    {
        controle = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        audioSrc = GetComponent<AudioSource>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }

    void Update(){
        if (!estahVivo){
            return;
        }
        if(vida <= 0){
            // FimDeJogo();
            return;
        }

        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        controle.Move(mover * velocidade * Time.deltaTime);

        ChecarBloqueioAbaixado();

        if(!levantarBloqueado && estaNoChao && Input.GetButtonDown("Jump")){
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            audioSrc.clip = somPulo;
            audioSrc.Play();
        }
        if(!estaNoChao){
            velocidadeCai.y += gravidade * Time.deltaTime;
        }
        controle.Move(velocidadeCai * Time.deltaTime);
        // audioSrc2.clip = somPassos;
        // audioSrc2.Play();
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            AgacharLevantar();
        }
        if ((x != 0 || z != 0) && estaNoChao && !audioSrc.isPlaying) {
        audioSrc.clip = somPassos;
        audioSrc.loop = true;  
        audioSrc.Play();
    } else if ((x == 0 && z == 0) || !estaNoChao) {
        audioSrc.loop = false;
        audioSrc.Stop();
    }

    }

    private void AgacharLevantar(){
        if(levantarBloqueado || estaNoChao == false){
            return;
        }
        estahAbaixado = !estahAbaixado;
        if(estahAbaixado){
            controle.height = alturaAbaixado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraAbaixado, 0);
        }else {
            controle.height = alturaLevantado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraEmPe, 0);
        }
    }

    private void ChecarBloqueioAbaixado(){
       // Debug.DrawRay(cameraTransform.position, Vector3.up*1.1f, Color.red);
        RaycastHit hit;
        levantarBloqueado = Physics.Raycast(cameraTransform.position, Vector3.up, out hit, 1.1f);
    }

    // public void AtualizarVida(int novaVida){
    //     vida = Mathf.CeilToInt(Mathf.Clamp(vida+novaVida, 0, 100));
    //     sliderVida.value = vida;
    // }


    // private void FimDeJogo(){
    //     Time.timeScale = 0;
    //     Camera.main.GetComponent<AudioListener>().enabled = false;
    //     GetComponentInChildren<Glock>().enabled = false;
    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = true;

    //     estahVivo = false;

    //     GerenciarPontuacao.instance.status = "Você perdeu!";
    //     //GerenciarPontuacao.instance.inimigosMortos = GerenciarPontuacao.instance.GetPontuacao();

    //     SceneManager.LoadScene(0);
    // }

    // private void OnTriggerEnter(Collider other){
    // if (other.CompareTag("Casa"))
    // {
    //     Time.timeScale = 0; 
    //     Camera.main.GetComponent<AudioListener>().enabled = false;
    //     GetComponentInChildren<Glock>().enabled = false; 
    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = true;

    //     estahVivo = false; 

    //     GerenciarPontuacao.instance.status = "Você ganhou!";
    //     //GerenciarPontuacao.instance.inimigosMortos = GerenciarPontuacao.instance.GetPontuacao();

        
    //     SceneManager.LoadScene(0);
    //     }
    // }

}
