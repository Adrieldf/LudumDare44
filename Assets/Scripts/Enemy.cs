using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Slime = 0,
        Ghost = 1,
        Zombie = 2,
        Spider = 3
    }
    public static Enemy Instance;
    private Transform Player;
    private Vector3 StartingPosition;

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
            default:
                Health = 2;
                Speed = 3;
                Power = 1;
                MinDistanceToChase = 0f;
                MaxDistanceToChase = 5f;
                break;
            case EnemyType.Slime:
                Health = 4;
                Speed = Random.Range(2f, 4.5f);
                Power = 1;
                MinDistanceToChase = 0f;
                MaxDistanceToChase = 4f;
                break;
            case EnemyType.Ghost:
                Health = 2;
                Speed = Random.Range(4f, 5.5f);
                Power = 1;
                MinDistanceToChase = 1f;
                MaxDistanceToChase = 5f;
                break;
            case EnemyType.Zombie:
                Health = 3;
                Speed = Random.Range(1.5f, 4f);
                Power = 2;
                MinDistanceToChase = 0f;
                MaxDistanceToChase = 3f;
                break;
            case EnemyType.Spider:
                Health = 1;
                Speed = Random.Range(5f, 6.5f);
                Power = 1;
                MinDistanceToChase = 0f;
                MaxDistanceToChase = 1f;
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
        Health -= amount;
        if (Health <= 0)
            Die();
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
        {//se executar esse ele volta para a posição de spawn do inimigo, acho que nao precisaria nesse caso, mas ta ai 
           // RotateAndMoveTo(StartingPosition);
        }

    }
    private void RotateAndMoveTo(Vector3 targetPosition)
    {
        //rotate to the target position direction
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position, transform.TransformDirection(Vector3.forward * -1));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        //move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }
}
