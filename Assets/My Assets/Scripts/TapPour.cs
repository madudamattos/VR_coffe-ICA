using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPour : MonoBehaviour
{
    [SerializeField] Transform interactable;

    // GameObjects de operacao 
    public Transform waterSpawn = null;
    public GameObject streamPrefab = null;
    private Stream currentStream = null;

    // Encher o recipiente de agua
    public ControlWaterLevel waterScript;
    private Coroutine fillingCoroutine = null;


    // Variaveis de controle de estado 
    protected bool isPouring = false;
    public bool pourCheck = false;

    protected bool inReach = false;
    protected bool isFilling = false;
    protected bool isFull = false;

    public void Start()
    {

    }

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

        // Nesse caso derramar agua vai engatilhar uma acao, e nesse caso essa ação é encher a caneca de água 
        TriggerAction();

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

    // Verifica se existe um recipiente embaixo da corrente de água, para resolver ativar ou nao a rotina de encher o recipiente de água
    protected virtual bool DetecRecipient()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterSpawn.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {

            if (hit.collider.gameObject.tag == "Water")
            {
                isFull = hit.collider.gameObject.GetComponent<ControlWaterLevel>().isFull;
                return true;
            }
        }

        return false;
    }


    // Desencadeia uma ação, como encher o recipiente de água 
    protected virtual void TriggerAction()
    {
        inReach = DetecRecipient();

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

    protected bool IsPouring()
    {
        return isPouring;
    }

    protected bool ActiveWater()
    {
        if (interactable.rotation.x < -0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
