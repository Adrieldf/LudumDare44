using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Character : MonoBehaviour
{
    public static Character Instance;
    #region Stats

    public float Health;
    private float MaxHealth;

    public float Speed;
    public float GetSpeed => Speed * Time.deltaTime;

    public float AttackSpeed;
    public float AttackDamage;

    #endregion
    public GameObject playerSprite;
    public Animator anim;
    public bool facingRight = true;
    public CircleCollider2D attackCollider;
    private float hitByEnemyCooldown = 0;
    public TextMeshProUGUI wavesSurvived;
    public GameObject deathPanel;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameController.Instance.CreateHearts();
        attackCollider.enabled = false;
        MaxHealth = Health;
    }

    void Update()
    {
        if (hitByEnemyCooldown > 0)
            hitByEnemyCooldown -= Time.deltaTime;
    }
    public void FullHeal()
    {
        for (int i = 0; i < Health; i++)
            GameController.Instance.RemoveHearts(1);
        for (int i = 0; i < MaxHealth; i++)
            GameController.Instance.AddHeart(1);
        Health = MaxHealth;
    }

    private void FixedUpdate()
    {
        #region Movement & Rotation
        float speedAndTime = Speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        anim.SetBool("running", x != 0 || y != 0);
        transform.Translate(x < 0 ? (x * speedAndTime) * -1 : x * speedAndTime, y * speedAndTime, 0f);
        //  if(!(x == 0 && y == 0))//quando possuir animações setar as triggers dentro do calculate rotation em vez de rotacionar o sprite daí
        //      playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, CalculateRotation(x, y)));//Quaternion.Euler é para converter de graus para o tal do quaternion que ninguem sabe como funciona direito
        if (x < 0)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (x > 0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        #endregion

        #region Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("attack");
            AudioFXController.Instance.PlayAttackFx();
            StartCoroutine(AttackWait(true, 0.3f));
            StartCoroutine(AttackWait(false, 0.4f));
        }

        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // KnockBack(); ficou meio zuado
            if (hitByEnemyCooldown <= 0)
                TakeDamage(1);

            //Debug.Log("got hit");
            // Enemy enemy = collision.GetComponentInParent<Enemy>();
            // enemy.KnockBack(true);//como ficou zoado nele dou knockback no inimigo (foda-se é game de jam) // edit: tirei porque ficou zuadaaaasso
        }
    }
    public void TakeDamage(int amount)
    {
        hitByEnemyCooldown = 0.85f;
        Health--;
        GameController.Instance.RemoveHearts(1);
        if (Health <= 0)
            Die();
    }
    public void ChangeSpeed(int amount)
    {
        if (amount > 0)
            Speed += 0.5f;
        else
            Speed -= 0.5f;
    }
    public void ChangeHealth(int amount)
    {
        if (amount > 0)
            MaxHealth += 1f;
        else
            MaxHealth -= 1f;
    }
    public void ChangeAttack(int amount)
    {
        if (amount > 0)
            AttackDamage += 1f;
        else
            AttackDamage -= 1f;
    }
    private void Die()
    {
        //larga uns efeitos e particulas
        StartCoroutine(DeathPanel());
        //Destroy(gameObject, 0.35f);
    }
    private void KnockBack()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        var directionVector = facingRight ? -transform.right : transform.right;

        rb.AddForce(directionVector * 3f, ForceMode2D.Impulse);
    }
    private float CalculateRotation(float x, float y)
    {
        //if (Mathf.Abs(x) > Mathf.Abs(y))//Verifica qual possui o valor mais distante de zero dando preferencia para o eixo vertical(melhor nos consoles, no pc é -1,0 ou 1 então nao muda muito)
        if (x < 0)
            return 90f;//quase tudo comentado porque a rotação vai ser só para esquerda e direita
        else
            return 270f;
        //else
        //    if (y < 0)
        //    return 180f;
        //else
        //    return 0f;
    }
    IEnumerator AttackWait(bool activate, float sec)
    {
        yield return new WaitForSeconds(sec);//tempo que fica habilitado o collider para dar dano nos inimigos
        attackCollider.enabled = activate;
    }
    IEnumerator DeathPanel()
    {
        yield return new WaitForSeconds(1f);
        wavesSurvived.text = EnemiesSpawner.Instance.currentWave.ToString();
        deathPanel.SetActive(true);

    }
}
