using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
	public int damage;

	public GameObject dropWeapon; // 생성할 프리팹

	public Transform target;

    Rigidbody rigid;
    BoxCollider boxcollider;

	Material mat;
	NavMeshAgent nav;
	Animator anim;


	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		boxcollider = GetComponent<BoxCollider>();
		mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		nav.SetDestination(target.position);
	}

	void FreezRotation()
	{
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
	}

	private void FixedUpdate()
	{
		FreezRotation();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Melee")
		{
			Weapon weapon = other.GetComponent<Weapon>();
			curHealth -= weapon.damage;
			StartCoroutine(OnDamage());
		}
		else if(other.tag == "Bullet")
		{
			Bullet bullet = other.GetComponent<Bullet>();
			curHealth -= bullet.damage;

			Debug.Log("Range : " + curHealth);
			StartCoroutine(OnDamage());
		}
	}

	IEnumerator OnDamage()
	{
		mat.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		if(curHealth > 0)
		{
			mat.color = Color.white;
		}
		else
		{
			mat.color = Color.gray;
			target.GetComponent<Player>().killcount++;
			if(target.GetComponent<Player>().killcount == 5)
			{
				Instantiate(dropWeapon, transform.position, transform.rotation);

			}
			Destroy(gameObject, 0.01f);
		}

	}
}
