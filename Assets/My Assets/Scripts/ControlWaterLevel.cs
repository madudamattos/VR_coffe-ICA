using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWaterLevel : MonoBehaviour
{

    protected bool isEmpty = true;
    public bool isFull = false;
    private Vector3 initialPosition;

    public ParticleSystem steamEffect;
    public Transform steamRef;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        steamEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {   
        // Logica para ativar e desativar o vapor 
        if (isFull)
        {
            if (!steamEffect.isPlaying)
            {
                steamEffect.Play();  
            }
        }
        else
        {
            if (steamEffect.isPlaying)
            {
                steamEffect.Stop(); 
            }
        }

        steamEffect.transform.position = steamRef.position;
    }

    public IEnumerator FillMug()
    {
        if (isEmpty)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
            isEmpty = false;
        }

        while (!isFull)
        {
            yield return new WaitForSeconds(0.3f);
            transform.localPosition += new Vector3(0, 1f, 0);

            if (transform.localPosition.y >= 23)
            {
                isFull = true;
            }
        }
    }

    public IEnumerator EmptyMug()
    {
        if (isEmpty)
        {
            GetComponent<MeshRenderer>().enabled = false;
            isEmpty = true;
        }

        while (!isEmpty)
        {
            yield return new WaitForSeconds(0.3f);
            transform.localPosition -= new Vector3(0, 1f, 0);

            if (transform.localPosition.y <= initialPosition.y)
            {
                isEmpty = true;
            }
        }
    }

    public void Fill()
    {
        StartCoroutine(FillMug());
        if (isFull)
        {
            StopCoroutine(FillMug());
        }

    }

}
