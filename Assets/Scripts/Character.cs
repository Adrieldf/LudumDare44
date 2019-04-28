using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance;
    #region Stats

    public float Health;
    public float MaxHealth;

    public float Speed;
    public float GetSpeed => Speed * Time.deltaTime;

    public float AttackSpeed;
    public float AttackDamage;

    #endregion
    public GameObject playerSprite;
    public Animator anim;
    private bool facingRight = true;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameController.Instance.CreateHearts();
    }

    void Update()
    {

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
        else
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
        }

        #endregion
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
}
