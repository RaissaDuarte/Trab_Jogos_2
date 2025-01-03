using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCamera : MonoBehaviour
{

    public float sensibilidadeMouse = 100f;
    public float anguloMinimo = -90f;
    public float anguloMaximo = 90f;

    public Transform transformPlayer;

    float rotacao = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        rotacao -= mouseY;
        rotacao = Mathf.Clamp(rotacao, anguloMinimo, anguloMaximo);
        transform.localRotation = Quaternion.Euler(rotacao,0,0);

        transformPlayer.Rotate(Vector3.up * mouseX);
    }
}
