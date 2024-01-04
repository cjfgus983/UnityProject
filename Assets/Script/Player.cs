using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public AudioSource attackSound1; // 이펙트 사운드를 재생할 AudioSource 변수 추가
    public AudioSource attackSound2; // 이펙트 사운드를 재생할 AudioSource 변수 추가

    public Slider hpbar;

    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public Camera followCamera;
    public int health = 100;
    public int maxhealth = 100;
    public int killcount = 0;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    SkinnedMeshRenderer mesh;

    bool wDown;
    bool jDown;
    bool fDown; //공격키
    bool isJump;
    bool iDown; // f 
    bool sDown; // e
    bool isFireReady;

    bool isBorder;

    bool isDamage = false;

    int weaponIdx = 0;
    float fireDelay;
    GameObject nearObject;

	private void Awake()
	{
        anim = GetComponent<Animator>();
        rigid = GetComponent< Rigidbody>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

	void Start()
    {
        hpbar.value = 1;
        killcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
 

        getInput();
        Move();
        Turn();
        Jump();
        Attack();
        Interation();
        Swap();
    }

    void getInput()
	{
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButton("Jump");
        iDown = Input.GetButton("Interation");
        sDown = Input.GetButtonDown("Swap");
        fDown = Input.GetButton("Fire1");
    }

	private void Move()
	{
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;


        if(!isBorder)
		{
            transform.position += moveVec * speed * (wDown ? 0.3f : 0.8f) * Time.deltaTime;
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        anim.SetBool("isBack", hAxis < 0);
    }

    void Turn()
	{
        //키보드에 의한 회전
        transform.LookAt(transform.position + moveVec); // 지정된 방향을 바라보는 함수

        //마우스에 의한 회전
        if(fDown)
		{
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nexrVec = rayHit.point - transform.position; //레이 닿은곳과 플레이어의 차
                nexrVec.y = 0;
                transform.LookAt(transform.position + nexrVec);
            }
        }
    }

    void Jump()
	{
        if(jDown && !isJump)
		{
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
	}
    void Swap()
    {
        if(sDown && hasWeapons[1] == true) // e 누르면
		{
            weapons[weaponIdx].SetActive(false); 
            weapons[(weaponIdx+1)%2].SetActive(true);
            weaponIdx = (weaponIdx + 1) % 2;

        }
    }

    void Attack()
	{
        fireDelay += Time.deltaTime;
        isFireReady = weapons[weaponIdx].GetComponent<Weapon>().rate < fireDelay;

        if(fDown && isFireReady)
		{
            weapons[weaponIdx].GetComponent<Weapon>().Use();
            anim.SetTrigger(weaponIdx == 0 ? "doSwing" : "doShot");
            fireDelay = 0;
            if (weaponIdx == 0)
            {
                attackSound1.enabled = true;
                attackSound1.Play(); // 이펙트 사운드를 재생합니다.
            }
            if (weaponIdx == 1)
            {
                attackSound2.enabled = true;
                attackSound2.Play(); // 이펙트 사운드를 재생합니다.
            }
        }

    }
    void Interation()
	{
        if (iDown && nearObject != null && !isJump)
		{
            if(nearObject.tag == "Weapon")
			{
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
			}
		}
	}

    void FreezRotation()
	{
        rigid.angularVelocity = Vector3.zero;
	}

    void StopTowall()
	{
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));

    }


    private void FixedUpdate()
	{
        FreezRotation();
        StopTowall();
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Floor")
		{
            anim.SetBool("isJump", false);
            isJump = false;
		}
        else if (collision.gameObject.tag == "Enemy")
        {
            if (!isDamage)
            {
                int getDamage = collision.gameObject.GetComponent<Enemy>().damage;
                health -= getDamage;
                hpbar.value = (float)health / (float)maxhealth;
                StartCoroutine(OnDamage());
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Weapon")
		{
            nearObject = other.gameObject;
		}
     
	}

    IEnumerator OnDamage()
    {
        isDamage = true;
        mesh.material.color = Color.cyan;

        yield return new WaitForSeconds(1f);

        isDamage = false;
        mesh.material.color = Color.white;
    }

    private void OnTriggerExit(Collider other)
	{
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
}
