﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSniper : Longrange
{

    //float shootDelay = 0.5f;

    private void Awake()
    {

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
            _Bullet._BulletState = BulletState.Player;
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
        _SkillText = "5초간 공격딜레이 감소";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_WindForce");//아이템 이미지
        _SkillCollTime = 10;
    }

    public override void Skill()
    {
        if (_Skill)
        {
            StartCoroutine(SkillCourutin());
        }
    }
    IEnumerator SkillCourutin()
    {
        _Skill = false;
        Player.GetInstance._Buff.SetTrigger("AttackSpeedBuff");
        _Delay = 0.5f;
        yield return new WaitForSeconds(5.0f);
        if (!_Skill)
        {

            _Delay = 1f;
        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }

    }
    //총알 발사
    public override  void FireBulet(Vector2 Position, float _angle)
    {
        base.FireBulet(Position, _angle);

    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }

}