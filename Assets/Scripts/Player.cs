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
    private AudioSource _audioSource;

    [Header("Áudios")]
    public AudioClip somAndar;
    public AudioClip somPulo;
    public AudioClip somAtaque;
    public AudioClip somItem; // exemplo: pegar item

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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

        // Movimento esquerda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = true;

            if (noChao)
            {
                andando = true;
                parado = false;
                PlayLoopSound(somAndar);
            }
        }

        // Movimento direita
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;

            if (noChao)
            {
                andando = true;
                parado = false;
                PlayLoopSound(somAndar);
            }
        }

        // Para som de andar quando parado
        if (!andando && _audioSource.clip == somAndar)
        {
            _audioSource.Stop();
        }

        // Pulo
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && noChao)
        {
            _rigidbody2D.AddForce(Vector2.up * focaPulo, ForceMode2D.Impulse);
            pulando = true;
            parado = false;
            PlayOneShot(somPulo);
        }

        // Ataque (tecla Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            atacando = true;
            parado = false;
            PlayOneShot(somAtaque);
        }

        // Atualiza animações
        _animator.SetBool("Andando", andando);
        _animator.SetBool("Pulando", pulando);
        _animator.SetBool("Atacando", atacando);
        _animator.SetBool("Parado", parado);
    }

    // 🔊 Toca sons únicos (pulo, ataque, item)
    void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }

    // 🔁 Toca sons contínuos (andar)
    void PlayLoopSound(AudioClip clip)
    {
        if (_audioSource.clip != clip)
        {
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    // 🎒 Exemplo: pegar item
    public void PlayItemSound()
    {
        PlayOneShot(somItem);
    }
}
