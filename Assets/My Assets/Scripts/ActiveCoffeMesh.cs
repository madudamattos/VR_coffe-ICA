using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoffeMesh : MonoBehaviour
{
    [SerializeField] GameObject coffeFilter;
    public bool isCoffeFilterActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveCoffe()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        coffeFilter.SetActive(true);
        isCoffeFilterActive = true;
    }

}
