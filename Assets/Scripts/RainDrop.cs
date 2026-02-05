using UnityEngine;

public class RainDrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando encostar em qualquer coisa, destrói a gota
        Destroy(gameObject);
    }
}