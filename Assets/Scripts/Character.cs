using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Stats

    public float Health;
    public float MaxHealth;

    public float Speed;
    public float GetSpeed => Speed * Time.deltaTime;

    public float AttackSpeed;
    public float AttackDamage;

    #endregion

    void Start()
    {
        
    }
    
    void Update()
    {

    }
   
    private void FixedUpdate()
    {
        #region Movement
        float speedAndTime = Speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(x * speedAndTime, y * speedAndTime, 0f);
        #endregion
    }
}
