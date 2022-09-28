using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communicate : MonoBehaviour
{
    public GameObject guideText;
    public GameObject Npc;
    public GameObject questBox;

    public GameObject heroFrame;
    public GameObject foxFrame;

    private GameObject current = null;
    private int count = 1;

    void Update()
    {
        if (Vector3.Distance(transform.position, Npc.transform.position) <= 5) {
            if (questBox.activeSelf == false) {
                guideText.SetActive(true);
            } 
            if(Input.GetKeyDown(KeyCode.F)) {
                guideText.SetActive(false);
                questBox.SetActive(true);

                if(current) {
                    current.SetActive(false);
                }
                current = questBox.transform.GetChild(count).gameObject;
                current.SetActive(true);
                if (count % 2 == 1) {
                    heroFrame.SetActive(true);
                    foxFrame.SetActive(false);
                } else {
                    foxFrame.SetActive(true);
                    heroFrame.SetActive(false);
                }

                if (count < 8) {
                    count++;
                } else {
                    Npc.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        } else {
            guideText.SetActive(false);
            questBox.SetActive(false);
        }
    }
}
