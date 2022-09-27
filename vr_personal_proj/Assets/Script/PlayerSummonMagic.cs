using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummonMagic : MonoBehaviour
{
    public GameObject energyBolt_pf;
    private GameObject energyBolt;
    public GameObject energyArrow;
    public GameObject staffFloat;
    PlayerController playerController;

    public void Create() {
        energyBolt = Instantiate(energyBolt_pf, staffFloat.transform.position, transform.rotation);
        //energyBolt.transform.SetParent(staffFloat.transform, false);
    }

    public void EnergyArrowCreate() {
        playerController = GetComponent<PlayerController>();
        for (int i = 0; i < Mathf.Clamp(playerController.countOfArrow, 0, 5); i++) {
            Instantiate(energyArrow, transform.position + transform.forward * i + new Vector3(0,1.2f,0), transform.rotation);
        }
    }
}
