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
    public bool complete = false;
    private int count = 1;

    private GameObject questHUD;
    private GameObject completeHUD;

    void Start() {
        questHUD = GameObject.Find("Fox/Quest Canvas/QuestText");
        completeHUD = GameObject.Find("Fox/Quest Canvas/Complete");
    }

    void Update()
    {
        if (complete && count >= 8) {
            completeHUD.SetActive(true);
        }
        if (Vector3.Distance(transform.position, Npc.transform.position) <= 5) {
            if (questBox.activeSelf == false) {
                guideText.SetActive(true);
            } 
            if(!complete && count <= 8) { //퀘스트 중
                if (Input.GetKeyDown(KeyCode.F)) {
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
                        questHUD.SetActive(false);
                    }
                }
            } else if(complete && count >= 8) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    guideText.SetActive(false);
                    questBox.SetActive(true);
                    current.SetActive(false);

                    if (count < 12) {
                        count++;
                    } else {
                        completeHUD.SetActive(false);
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
                }
            }

        } else {
            guideText.SetActive(false);
            questBox.SetActive(false);
        }
    }
}
