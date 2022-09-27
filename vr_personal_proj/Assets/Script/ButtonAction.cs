using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject shift;
    public GameObject e;
    public GameObject q;
    private GameObject current = null;

    public void onLeft() {
        if (current) {
            current.SetActive(false);
        }
        current = left;
        current.SetActive(true);
    }
    
    public void onRight() {
        if (current) {
            current.SetActive(false);
        }
        current = right;
        current.SetActive(true);
    }

    public void onShift() {
        if (current) {
            current.SetActive(false);
        }
        current = shift;
        current.SetActive(true);
    }

    public void onE() {
        if (current) {
            current.SetActive(false);
        }
        current = e;
        current.SetActive(true);
    }

    public void onQ() {
        if (current) {
            current.SetActive(false);
        }
        current = q;
        current.SetActive(true);
    }
}
