﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGhost : cShortMonster
{

    Rigidbody2D _rigid;

    public float attackDelay = 4f; //어택 딜레이
    float attackTimer = 0; //어택 타이머
    float speed = 0.5f;
    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("LittleGhostAttack"))
        {
            speed = 3f;
        }
        else
        {
            speed = 1.5f;
        }
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);

        _rigid.velocity = new Vector2(dir.normalized.x * speed, dir.y * speed);

        attackTimer += Time.deltaTime;

        if (attackTimer > attackDelay) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Attack");

            attackTimer = 0; //쿨타임 초기화
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        GameObject Dam = Instantiate(_Damage);
        Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Dam.GetComponent<cDamageText>().SetDamage(dam, isCritical);
        if (_currnetHP > 0)
        {
            _currnetHP -= dam;
        }
        else if (_currnetHP <= 0)
        {
            Destroy(this);
        }
    }
}