﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladius : Shortrange
{
    private void Awake()
    {

        _ItemID = 4;
        _ItemName = "글라디우스";//아이템이름
        _ItemDescrIption = "밀집된 곳에서 전투를 하기 위해 고안된 검";//아이템설명
        _ItemIcon = Resources.Load<Sprite>("Itemp/Gladius");//아이템이미지
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 10;//최소데미지
        _MaxAttackDamage = 13;//최대데미지
        _AttackSpeed = 2.5f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급
        _ItemPrice = 1000;//아이템가격
        _SkillText = "15초 동안 위력 증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_WillpowerOfGladiator");//아이템 이미지
        _SkillCollTime = 20;

    }
    public override void Attack(cMonsterBase Monster)
    {
        base.Attack(Monster);

    }
    public override void Skill()
    {
        if (_Skill)
        {
            StartCoroutine(SkillCorutin());
        }
     }
    IEnumerator SkillCorutin()
    {
        _Skill = false;
        Player.GetInstance._Buff.SetTrigger("PowerBuff");
        Player.GetInstance._Power += 20;
        yield return new WaitForSeconds(15.0f);
        if (!_Skill)
        {

            Player.GetInstance._Power -= 20;

        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }
    }

}
