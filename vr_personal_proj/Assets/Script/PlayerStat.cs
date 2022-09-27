using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public float healthPower = 300f;
    public float magicPower = 600f;
    public GameObject hpbar;
    public GameObject mpbar;
    Slider hpSlider;
    Slider mpSlider;

    void Awake()
    {
        hpSlider = hpbar.GetComponent<Slider>();
        mpSlider = mpbar.GetComponent<Slider>();
        StartCoroutine(restore());
    }

    void Update() {
        hpSlider.value = healthPower;
        mpSlider.value = magicPower;    
    }

    IEnumerator restore() { //초당 체력 10, 마나 15 회복
        while(true) {
            yield return new WaitForSeconds(1);
            healthPower += 10;
            magicPower += 15;
            if (healthPower > 300) {
                healthPower = 300;
            }
            if (magicPower > 600) {
                magicPower = 600;
            }
        }
    }
}
