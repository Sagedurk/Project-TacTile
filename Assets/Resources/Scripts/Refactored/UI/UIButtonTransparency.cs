using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonTransparency : MonoBehaviour
{
    [SerializeField] float alphaThreshold = 0.5f;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;      
    }



}
