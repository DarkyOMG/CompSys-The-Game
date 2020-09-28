using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool highlighted = false;
    public Color baseColor;
    public bool isLit = false;

    public int value;
    public abstract void Activate();
    private void Awake()
    {
        baseColor = this.gameObject.GetComponent<Renderer>().material.color;
    }
    public void Highlight()
    {
        if (!isLit)
        {
            if (!highlighted)
            {
                this.gameObject.GetComponent<Renderer>().material.color = baseColor * 2;
                highlighted = true;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = baseColor;
                highlighted = false;
            }
        }
    }
}
