using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : MonoBehaviour
{
    public float speed = 20f;
    public GameObject explosion;
    private GameObject camera;
    Rigidbody bulletRigidbody;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        camera = GameObject.Find("Player/Camera");
        bulletRigidbody.velocity = camera.transform.forward * speed;
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.tag == "Monster") {
            BearController bear = other.gameObject.GetComponent<BearController>();
            bear.IsTookDamage = true;
        }
    }
}
