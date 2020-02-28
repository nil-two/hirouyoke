using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().enabled = true;
    }

    public void FadeIn()
    {
    }
}
