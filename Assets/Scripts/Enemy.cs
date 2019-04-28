using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Slime = 0,
    }
    public static Enemy Instance;
    private Transform Player;
    private Vector3 StartingPosition;
    #region Stats
    public EnemyType Type;
    [HideInInspector]
    public float Health;
    [HideInInspector]
    public float Speed;
    [HideInInspector]
    public float Power;
    [HideInInspector]
    public float MinDistanceToChase;
    [HideInInspector]
    public float MaxDistanceToChase;
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player = GameController.Instance.PlayerObject.transform;
        StartingPosition = transform.position;
        RollStats();
    }
    private void Update()
    {
        Chase();
    }
    private void RollStats()
    {
        switch (Type)
        {
            case EnemyType.Slime:
                Health = 3;
                Speed = Random.Range(2f, 4.5f);
                Power = 1;
                MinDistanceToChase = 0f;
                MaxDistanceToChase = 10f;
                float multi = Random.Range(0.7f, 1.3f);
                transform.localScale = new Vector3(multi, multi, 1f);
                break;

        }
    }

    private void Die()
    {
        //larga uns efeitos e particulas
        Destroy(gameObject, 0.35f);
    }

    public void TakeDamage(float amount)
    {
        KnockBack();
        Health -= amount;
        if (Health <= 0)
            Die();
    }
    private void KnockBack()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        var directionVector = transform.right;

        if(!Character.Instance.facingRight)
            directionVector = -directionVector;
            
        rb.AddForce (directionVector * 5f, ForceMode2D.Impulse);
    }

    private void Chase()
    {
        if (Player == null)
            return;

        float distanceFromPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceFromPlayer >= MinDistanceToChase && distanceFromPlayer <= MaxDistanceToChase)
        {
            RotateAndMoveTo(Player.position);
        }
        else
        {
            //se executar esse ele volta para a posição de spawn do inimigo, acho que nao precisaria nesse caso, mas ta ai 
            // RotateAndMoveTo(StartingPosition);
        }

    }
    private void RotateAndMoveTo(Vector3 targetPosition)
    {
        //rotate to the target position direction
        //Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position, transform.TransformDirection(Vector3.forward * -1));
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        //move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }

}
