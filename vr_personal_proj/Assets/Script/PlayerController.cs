using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int JumpPower;
    public int countOfArrow = 0;
    public Animator animator;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float lookSensitivity; 
    [SerializeField]
    private float cameraRotationLimit;  
    [SerializeField]
    private Camera theCamera; 
    //Control variable
    private bool IsJumping;
    private bool isSkillAction;
    private float currentCameraRotationX;  
    private bool IsWindow;
    private bool IsLeftClick;
    private Vector3 rayHitPos;
    private GameObject monster;
    //Component
    private Rigidbody myRigid;
    private Transform myTrans;
    private PlayerStat playerStat;
    //GameObject
    public GameObject charging;
    public GameObject waterEssence;
    public GameObject manaRestore;
    public GameObject purpleBomb;
    public GameObject skillWindow;

    // Start is called before the first frame update
    void Start() {
         myRigid = GetComponent<Rigidbody>();
         myTrans = GetComponent<Transform>();
         playerStat = GetComponent<PlayerStat>();
         Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        if (!isSkillAction && !IsWindow) {
            LeftClickAction();
            RightClick();
            ShiftClick();
            EClick();
            QClick();
        }
        if (!IsLeftClick) {
            CameraRotation();
            CharacterRotation();
        }
        Move();
        Jump();
        SkillWindow();
    }

    private void Move() {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");  
        Vector3 _moveHorizontal = transform.right * _moveDirX; 
        Vector3 _moveVertical = transform.forward * _moveDirZ; 
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        if (_moveDirZ == 0) {
            animator.SetBool("IsForwardRunning", false);
            animator.SetBool("IsBackwardRunning", false);
        } if (_moveDirZ > 0) {
            animator.SetBool("IsForwardRunning", true);
            animator.SetBool("IsBackwardRunning", false);
        } else if (_moveDirZ < 0) {
            animator.SetBool("IsBackwardRunning", true);
            animator.SetBool("IsForwardRunning", false);
        }

        if (_moveDirX == 0) {
            animator.SetBool("IsLeftRunning", false);
            animator.SetBool("IsRightRunning", false);
        } else if (_moveDirX > 0) {
            animator.SetBool("IsRightRunning", true);
            animator.SetBool("IsLeftRunning", false);
        } else if (_moveDirX < 0) {
            animator.SetBool("IsLeftRunning", true);
            animator.SetBool("IsRightRunning", false);
        }

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsJumping) {
                IsJumping = true;
                animator.SetBool("IsJumping", IsJumping);
                myRigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
            else {
                return;
            }
        }
    }

    IEnumerator leftclick() {
        isSkillAction = true;
        IsLeftClick = true;
        animator.SetBool("IsLeftClick", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("IsLeftClick", false);
        IsLeftClick = false;
        isSkillAction = false;
    }

    private void LeftClickAction() {
        if (Input.GetKey(KeyCode.Mouse0) && playerStat.magicPower >= 30) {
            playerStat.magicPower -= 30;
            StartCoroutine(leftclick());
        }
    }

    IEnumerator rightclick() {
        isSkillAction = true;
        countOfArrow = 0;
        GameObject temp = Instantiate(charging, transform.position + new Vector3(0,1,0), transform.rotation);
        temp.transform.SetParent(transform);
        while (Input.GetKey(KeyCode.Mouse1)) {
            animator.SetBool("IsRightClick", true);
            countOfArrow++;
            yield return new WaitForSeconds(0.5f);
        }
        animator.SetBool("IsRightClick", false);
        Destroy(temp);
        yield return new WaitForSeconds(1.2f);
        isSkillAction = false;
    }

    private void RightClick() {
        if (Input.GetKey(KeyCode.Mouse1) && playerStat.magicPower >= 100) {
            playerStat.magicPower -= 100;
            StartCoroutine(rightclick());
        }
    }

    IEnumerator shiftclick() {
        isSkillAction = true;
        GameObject temp = Instantiate(manaRestore, transform.position + new Vector3(0,1,0), transform.rotation);
        temp.transform.SetParent(transform);
        animator.SetBool("IsShiftClick", true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("IsShiftClick", false);
        playerStat.magicPower += 600;
        yield return new WaitForSeconds(0.5f);
        Destroy(temp);
        isSkillAction = false;
    }

    private void ShiftClick() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerStat.healthPower >= 100) {
            playerStat.healthPower -= 100;
            StartCoroutine(shiftclick());
        }
    }

    IEnumerator eclick() {
        isSkillAction = true;
        GameObject temp = Instantiate(waterEssence, transform.position + new Vector3(0,1,0), transform.rotation);
        temp.transform.SetParent(transform);
        animator.SetBool("IsEClick", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("IsEClick", false);
        playerStat.healthPower += 200;
        Destroy(temp);
        isSkillAction = false;
    }

    private void EClick() {
        if (Input.GetKeyDown(KeyCode.E) && playerStat.magicPower >= 200) {
            playerStat.magicPower -= 200;
            StartCoroutine(eclick());
        }
    }

    private void raycast() {
        Vector3 origin = new Vector3(0, 1.6f, 1);
        //Debug.DrawRay(transform.TransformPoint(origin), transform.forward, Color.red, 5);
        //다이렉션을 로컬 좌표로 해보았다.
        Ray ray = new Ray(transform.TransformPoint(origin), theCamera.transform.forward);
        RaycastHit hitData;
        monster = null;

        if(Physics.Raycast(ray, out hitData, 40)) {
            rayHitPos = hitData.point;
            if(hitData.collider.tag == "Monster") {
                monster = hitData.transform.gameObject;
            }
        } else {
            rayHitPos = transform.TransformPoint(new Vector3(0, 1, 40));
        }
    }

    IEnumerator qclick() {
        isSkillAction = true;
        raycast();
        GameObject temp = Instantiate(purpleBomb, rayHitPos, transform.rotation);
        if(monster) {
            temp.transform.SetParent(monster.transform);
        }
        animator.SetBool("IsQClick", true);
        yield return new WaitForSeconds(1.6f);
        animator.SetBool("IsQClick", false);
        if(monster) {
            BearController bearController = monster.GetComponent<BearController>();
            bearController.IsTookDamage = true;
            BearStat bearStat = monster.GetComponent<BearStat>();
            bearStat.healthPower -= 300;
        }
        yield return new WaitForSeconds(1.6f);
        Destroy(temp);
        isSkillAction = false;
    }

    private void QClick() {
        if (Input.GetKeyDown(KeyCode.Q) && playerStat.magicPower >= 200) {
            playerStat.magicPower -= 200;
            StartCoroutine(qclick());
        }
    }

    private void SkillWindow() {
        if (Input.GetKeyDown(KeyCode.K) && !IsWindow) {
            IsWindow = true;
            Cursor.visible = true;
            skillWindow.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.K)) {
            IsWindow = false;
            Cursor.visible = false;
            skillWindow.SetActive(false);
        }
    }

    private void CameraRotation() {
        float _xRotation = Input.GetAxisRaw("Mouse Y"); 
        float _cameraRotationX = _xRotation * lookSensitivity;
        
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation() {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myTrans.rotation = myTrans.rotation * Quaternion.Euler(_characterRotationY); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
            animator.SetBool("IsJumping", IsJumping);
        }
    }

}
