﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현제 장착중인무기
public class cWeaPon : MonoBehaviour
{

    //무기 이미지 그리기용 랜더러
    private SpriteRenderer _SpriteRend;
    //현제 아이템
    private Item _NowWeaPon;
    //무기 애니매이션 재생용 애니메이터
    private Animator _Ani;
    //검 애니메이션
    private RuntimeAnimatorController _SwardAni;
    //창 애니매이션
    private RuntimeAnimatorController _SpearAni;
    private RuntimeAnimatorController _GunAni;
    private cAttack _AttackMotion;
    void Awake()
    {
        _AttackMotion = FindObjectOfType<cAttack>();
          _Ani =transform.GetComponent<Animator>();
          _SpriteRend = transform.GetComponent<SpriteRenderer>();
        _SwardAni =Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Sward");
        _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/Spear");
        _GunAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Gun/Gun");

    }
    //현제 무기 세팅(무기장착시 세팅)
    public void SetWeaPon(Item _WeaPonNum)
    {
        _NowWeaPon = _WeaPonNum;
        if (_WeaPonNum._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _SpriteRend.sortingOrder = 4;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else if (_WeaPonNum._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _SpriteRend.sortingOrder = 10;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));

        }
        else if (_WeaPonNum._Type == ItemType.Gun)
        {
            _Ani.runtimeAnimatorController = _GunAni;
            _SpriteRend.sortingOrder = 4;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0,0));

        }
        _Ani.speed = _NowWeaPon._AttackSpeed;
        _SpriteRend.sprite = _NowWeaPon._ItemIcon;

        Player.GetInstance._MinDamage = 0;
        Player.GetInstance._MaxDamage = 0;


        Player.GetInstance._MinDamage += _NowWeaPon._MinAttackDamage;
        Player.GetInstance._MaxDamage += _NowWeaPon._MaxAttackDamage;
        _AttackMotion.SetItemMotion(_NowWeaPon);
       
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_NowWeaPon._Type != ItemType.Gun)
                {
                    _Ani.SetTrigger("AttackCheck");
                    StartCoroutine("Attack");
                }
                if (_NowWeaPon._Type == ItemType.Gun)
                {
                    _Ani.SetTrigger("AttackCheck");
                }
            }
        }

       
        //WeaPon.transform.position = rotateCenter + mousePos;
    }
    public void AnimationEvent()
    {
       
        Vector3 _mousePos = Input.mousePosition; //마우스 좌표 저장
        Vector3 _oPosition = transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 dir = (target - _oPosition);
        float rotateDegree = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        ((Longrange)_NowWeaPon).FireBulet(_oPosition, rotateDegree);
    }
    IEnumerator Attack()
    {
        if (_NowWeaPon._Type == ItemType.Sword)
        {
            if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                yield return new WaitForSeconds(0.1f);
                _AttackMotion._Attack();
            }
            else if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                yield return new WaitForSeconds(0.1f);
                _AttackMotion._Attack();
            }
        }
        else if (_NowWeaPon._Type == ItemType.Spear)
        {
            if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                _AttackMotion._Attack();
                yield return null;
            }
        }
    }
}
