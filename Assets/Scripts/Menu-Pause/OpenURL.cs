using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenKNIC()
    {
        Application.OpenURL("https://comp-sc.pnu.edu.ua/");
    }

    public void OpenPNU()
    {
        Application.OpenURL("https://pnu.edu.ua/");
    }

}
