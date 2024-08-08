using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KettleWaterPour : MonoBehaviour
{
    // GameObjects de operacao 
    [SerializeField] Transform waterSpawn = null;
    [SerializeField] GameObject streamPrefab = null;
    private Stream currentStream = null;

    // Encher o recipiente de agua
    private Coroutine fillingCoroutine = null;

    // Variaveis de controle de estado 
    public bool isPouring = false;
    private bool pourCheck = false;

    public bool inFilter = false;
    public bool filterCheck = false;

    private void Update()
    {
        pourCheck = ActiveWater();

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

        if (!filterCheck)
        {
            inFilter = DetectFilter();
        }

    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, waterSpawn.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    public bool ActiveWater()
    {
        if (transform.rotation.x < -0.2f || transform.rotation.x > 0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool DetectFilter()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterSpawn.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {

            if (hit.collider.gameObject.tag == "CoffeFilter")
            {
                filterCheck = true;
                return true;
            }
        }

        return false;
    }

}
