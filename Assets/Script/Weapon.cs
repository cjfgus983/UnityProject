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
            StopCoroutine("Swing"); //�������� �ڷ�ƾ�� ������ ����� 
            StartCoroutine("Swing");
		}
        else if(type == Type.Range)
		{
            StopCoroutine("Shot"); //�������� �ڷ�ƾ�� ������ ����� 
            StartCoroutine("Shot");
        }
	}

    IEnumerator Swing() //�ڷ�ƾ
	{
        //�̺κ� ����

        //����� �����ϴ� Ű���� //0.1�� ���
        yield return new WaitForSeconds(0.1f);
        //�̺κ� ����
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //����� �����ϴ� Ű���� //1������ ���
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation); // ������ ���� 
        Rigidbody bulletrigid = intantBullet.GetComponent<Rigidbody>();
        bulletrigid.velocity = bulletPos.forward * 50;
        yield return null;
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation); // ������ ���� 
        Rigidbody Caserigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        Caserigid.AddForce(caseVec, ForceMode.Impulse);
        Caserigid.AddTorque(Vector3.up*10, ForceMode.Impulse);
    }

}
