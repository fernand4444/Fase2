using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float focaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;
    public bool pulando = false;
    public bool atacando = false;
    public bool parado = true;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
            pulando = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
            pulando = true;
        }
    }

    void Update()
    {
        andando = false;
        atacando = false;
        parado = true;

        // Movimento para a esquerda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = true;

            if (noChao)
            {
                andando = true;
                parado = false;
            }
        }

        // Movimento para a direita
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;

            if (noChao)
            {
                andando = true;
                parado = false;
            }
        }

        // Pulo
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && noChao)
        {
            _rigidbody2D.AddForce(Vector2.up * focaPulo, ForceMode2D.Impulse);
            pulando = true;
            parado = false;
        }

        // Ataque (exemplo: tecla J)
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            atacando = true;
            parado = false;
        }

        // Atualiza animações
        _animator.SetBool("Andando", andando);
        _animator.SetBool("Pulando", pulando);
        _animator.SetBool("Atacando", atacando);
        _animator.SetBool("Parado", parado);
    }
}
