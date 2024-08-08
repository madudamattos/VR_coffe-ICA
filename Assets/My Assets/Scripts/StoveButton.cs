using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveButton : MonoBehaviour
{
    Quaternion currentRotation;
    Vector3 currentAngles;
    private Rigidbody rb;

    bool fireState = false;

    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform refPosition;
    private GameObject fire = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtém a rotação local do objeto
        currentRotation = transform.localRotation;

        // Converte a rotação atual para ângulos de Euler locais
        currentAngles = currentRotation.eulerAngles;

        if (!rb.isKinematic)
        {
            SnapToRightAngle();
            LightFire();
        }

    }

    void SnapToRightAngle()
    {
        // Arredonda os ângulos locais para 0 ou 90 graus em torno do eixo Y local
        if (currentAngles.y >= 45)
        {
            transform.localRotation = Quaternion.Euler(currentAngles.x, 90, currentAngles.z);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(currentAngles.x, 0, currentAngles.z);
        }

    }

    void LightFire()
    {
        // Verifica se o ângulo y está próximo de 90 graus
        if (currentAngles.y >= 45)
        {
            if(fire == null)
            {
                fire = Instantiate(firePrefab, refPosition.position, Quaternion.identity);
            }
            
        }
        // Caso contrário, desativa o fogo
        else
        {
            if (fire != null)
            {
                Destroy(fire);
                fire = null;
            }

        }
    }



}
