using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tut1;
    public GameObject tut2;
    public GameObject tut3;
    public GameObject tut4;
    public GameObject tut5;
    public void Tutorial1()
    {
        tut1.SetActive(false);
        tut2.SetActive(true);
    }
    public void Tutorial2()
    {
        tut2.SetActive(false);
        tut3.SetActive(true);
    }
    public void Tutorial3()
    {
        tut3.SetActive(false);
        tut4.SetActive(true);
    }
    public void Tutorial4()
    {
        tut4.SetActive(false);
    }
    public void StartTut5()
    {
        tut5.SetActive(true);
    }
    public void Tutorial5()
    {
        tut5.SetActive(false);
    }
}
