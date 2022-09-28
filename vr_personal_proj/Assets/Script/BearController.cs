using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    [SerializeField]
    private float walkSpeed = 20;
    //Component
    private Rigidbody myRigid;
    //Control
    private bool IsRunning;
    private bool IsAttack;
    public bool IsTookDamage;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= 20) {
            animator.SetBool("IsRunning", true);
            IsRunning = true;
        }
        if(Vector3.Distance(transform.position, player.transform.position) <= 8) {
            animator.SetBool("IsAttack", true);
            IsAttack = true;
        } else {
            animator.SetBool("IsAttack", false);
            IsAttack = false;
        }

        if(IsRunning && !IsTookDamage && !IsAttack) {
            Moving();
        }
        if(IsTookDamage) {
            StartCoroutine(tookDamage());
        }
    }

    void Moving() {
        transform.LookAt(player.transform);
        Vector3 _velocity = transform.forward * walkSpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    IEnumerator tookDamage() {
        animator.SetBool("IsTookDamage", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("IsTookDamage", false);
        IsTookDamage = false;
    }

    public void Hit() {
        if(Vector3.Distance(transform.position, player.transform.position) <= 8) {
            player.GetComponent<PlayerStat>().healthPower -= 50;
        } 
    }
}