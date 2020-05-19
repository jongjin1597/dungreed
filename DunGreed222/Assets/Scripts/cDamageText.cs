﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class cDamageText : MonoBehaviour
{
    float _MoveSpeed=1;
    float _DestroyTime =2;
    TextMeshPro _Text;
    Color _Alpha;
    float _AlphaSpeed=1;
    int _Damage;


    void Awake()
    {
        _Text = GetComponent<TextMeshPro>();
    
        _Alpha = _Text.color;
        Invoke("DestroyObject", _DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, _MoveSpeed * Time.deltaTime, 0));
        _Alpha.a = Mathf.Lerp(_Alpha.a, 0, Time.deltaTime * _AlphaSpeed);
        _Text.color = _Alpha;
 
     }  
    public void SetDamage(int dam,bool isCritical)
    {
        _Damage = dam;
        if (isCritical)
        {
            _Text.faceColor = Color.yellow;
           
        }
        else if (!isCritical)
        {
            _Text.faceColor = Color.white;
      
        }
        _Text.text = _Damage.ToString();
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
