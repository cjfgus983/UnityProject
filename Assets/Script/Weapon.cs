using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
	{
        if(type == Type.Melee)
		{
            StopCoroutine("Swing"); //동작중인 코루틴이 잇으면 꺼줘야 
            StartCoroutine("Swing");
		}
        else if(type == Type.Range)
		{
            StopCoroutine("Shot"); //동작중인 코루틴이 잇으면 꺼줘야 
            StartCoroutine("Shot");
        }
	}

    IEnumerator Swing() //코루틴
	{
        //이부분 실행

        //결과를 전달하는 키워드 //0.1초 대기
        yield return new WaitForSeconds(0.1f);
        //이부분 실행
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //결과를 전달하는 키워드 //1프레임 대기
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation); // 프리펩 생성 
        Rigidbody bulletrigid = intantBullet.GetComponent<Rigidbody>();
        bulletrigid.velocity = bulletPos.forward * 50;
        yield return null;
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation); // 프리펩 생성 
        Rigidbody Caserigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        Caserigid.AddForce(caseVec, ForceMode.Impulse);
        Caserigid.AddTorque(Vector3.up*10, ForceMode.Impulse);
    }

}
