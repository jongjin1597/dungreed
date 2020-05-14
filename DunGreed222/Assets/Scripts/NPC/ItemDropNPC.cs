﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDropNPC : cNPC
{
    //아이템 하나만 떨어뜨리기용 변수
    private int _ItemCount;
    protected override void Awake()
    {
        base.Awake();
        _ItemCount = 0;

    }
    
    
    private void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(_ItemCount != 1)
                ItemDrop();

            }
        }
    }
    //아이템 드랍
    void ItemDrop()
    {
        int _RandNum = Random.Range(0, 6);         // 0 ~ 6 랜덤값 초기화
        if (_RandNum >= 0 && _RandNum < 6)
        {
            GameObject _DropNode = null;
            _DropNode = (GameObject)Instantiate(Resources.Load("Prefabs/Item/DropNode"));
            _DropNode.transform.position = this.transform.position;

            Item _ItemValue = cDataBaseManager.GetInstance._ItemList[_RandNum];
            ItemDrop a_RefItemInfo = _DropNode.GetComponent<ItemDrop>();
            if (a_RefItemInfo != null)
            {
           
                a_RefItemInfo.SetItem(_ItemValue);
                a_RefItemInfo.transform.Rotate(new Vector3(0, 0, 90));
                //a_RefItemInfo.StartItem();
            }
            // 동적으로 텍스쳐 이미지 바꾸기
            SpriteRenderer a_RefRender = _DropNode.GetComponent<SpriteRenderer>();
            a_RefRender.sprite = _ItemValue._ItemIcon;

            _ItemCount = 1;
        }
    }


    //플레이어 충돌시 F버튼 보이기
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ItemCount != 1)
        {
            if (collision.gameObject.tag == "Player")
            {
              _ButtonF.SetActive(true);
                _isPlayer = true;
            }
        }
    }
    //플레이어 퇴장시 F버튼끄기
    private void OnTriggerExit2D(Collider2D collision)
    {
    
            if (collision.gameObject.tag == "Player")
            {
                _ButtonF.SetActive(false);
                _isPlayer = false;
            }
        
    }
}
