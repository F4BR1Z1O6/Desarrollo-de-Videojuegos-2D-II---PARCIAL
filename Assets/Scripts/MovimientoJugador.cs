using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;
    public Transform sueloVerificador;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private Animator anim;
    private bool mirandoDerecha = true;
    private bool enSuelo = false;

    public int dañoAtaque = 20;  // Cantidad de daño que el jugador inflige

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movimiento = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.velocity.y);
        rb.velocity = movimiento;

        if ((movimientoHorizontal > 0 && !mirandoDerecha) || (movimientoHorizontal < 0 && mirandoDerecha))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }

        anim.SetFloat("Velocidad", Mathf.Abs(movimientoHorizontal));
        anim.SetBool("EnSuelo", enSuelo);

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Atacar");
            Atacar();
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapCircle(sueloVerificador.position, 0.2f, capaSuelo);
    }

    private void Flip()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Atacar()
    {
        // Implementa la lógica de ataque (p. ej., detectar enemigos en el rango de ataque y aplicar daño)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mirandoDerecha ? Vector2.right : Vector2.left, 1.0f, LayerMask.GetMask("Enemigo"));
        if (hit.collider != null)
        {
            Enemigo enemigo = hit.collider.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(dañoAtaque);
            }
        }
    }
}
