using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffePour : MonoBehaviour
{

    public Transform waterSpawn = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    public bool pourCheck = false;

    private Stream currentStream = null;

    public ControlWaterLevel waterScript;
    private Coroutine fillingCoroutine = null;

    private bool inReach = false;
    private bool isFilling = false;
    private bool isFull = false;

    [SerializeField] ActiveCoffeMesh coffeScript;
    [SerializeField] KettleWaterPour kettleScript;

    private bool coffe;
    private bool kettle;
    private bool kettlePouring = false;


    private void Update()
    {
        pourCheck = PourCheck();

        if (isPouring != pourCheck && !isFull)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
        }

        if (isFull)
        {
            EndPour();
        }

        //encher a caneca
        inReach = DetecMug();

        if (inReach && !isFilling && isPouring)
        {
            fillingCoroutine = StartCoroutine(waterScript.FillMug());
            isFilling = true;
        }
        else if (!inReach || !isPouring || isFull)
        {
            if (fillingCoroutine != null)
            {
                StopCoroutine(fillingCoroutine);
                fillingCoroutine = null;
            }
            isFilling = false;
        }

    }

    private void StartPour()
    {
        
        currentStream = CreateStream();
        currentStream.Begin();
        
    }

    private void EndPour()
    {
        if (currentStream != null)
        {
            currentStream.End();
            currentStream = null;
        }
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, waterSpawn.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    public bool DetecMug()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterSpawn.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {

            if (hit.collider.gameObject.tag == "Water")
            {
                isFull = waterScript.isFull;
                return true;
            }
        }

        return false;
    }

    public bool PourCheck()
    {
        kettle = kettleScript.inFilter;
        coffe = coffeScript.isCoffeFilterActive;

        if (kettleScript.isPouring)
        {
            kettlePouring = true;
        }

        if (kettle && kettlePouring && coffe)
        {
            return true;
        }

        return false;
    }
}
