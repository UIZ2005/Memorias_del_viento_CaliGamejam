using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaRecuerdo : MonoBehaviour
{
    public GameObject recuerdo1;
    public GameObject recuerdo2;
    public GameObject recuerdo3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("recuerdo1"))
        {
            ActivarRecuerdo1();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("recuerdo2"))
        {
            ActivarRecuerdo2();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("recuerdo3"))
        {
            ActivarRecuerdo3();
            Destroy(other.gameObject);
        }
    }

    public void ActivarRecuerdo1()
    {
        recuerdo1.SetActive(true);
    }

    public void ActivarRecuerdo2()
    {
        recuerdo2.SetActive(true);
    }

    public void ActivarRecuerdo3()
    {
        recuerdo3.SetActive(true);
    }
}