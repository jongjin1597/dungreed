﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSniper : Longrange
{

    //float shootDelay = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 1;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 100.0f;
            _Bullet._Damage = Random.Range(30,41);
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
        _Delay = 1;
        _ItemID = 3;
        _ItemName = "화승총";//아이템이름
        _ItemDescrIption = "비가 오면 사용할수 없는총";//아이템설명
        _Type = ItemType.OneShot;//아이템타입
        _MinAttackDamage = 30;//최소데미지
        _MaxAttackDamage = 41;//최대데미지
        _AttackSpeed = 1f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/MatchlockGun");//아이템 이미지
        _ItemPrice = 700;//아이템가격


    }

    public override void Skill()
    { }

    //총알 발사
   public override  void FireBulet(Vector2 Position, float _angle)
    {
        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }


        _BulletPoll[_CurBulletIndex].transform.position = Position;

            _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);

            _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
             StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
            if (_CurBulletIndex >= _MaxBullet - 1)
            {
            _CurBulletIndex = 0;
            }
            else
            {
            _CurBulletIndex++;
            }


    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
}
