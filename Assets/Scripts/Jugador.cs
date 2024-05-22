using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool mirandoDerecha = true;
    private bool enSuelo = false;

    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;
    public Transform sueloVerificador;
    public LayerMask capaSuelo;

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

    protected override void Morir()
    {
        base.Morir();
    }

    // Propiedades
    public int PlayerHealth
    {
        get { return salud; }
        set { salud = Mathf.Clamp(value, 0, 100); }
    }

    // Sobrecarga de método
    public void RecibirDaño(int cantidad, string fuente)
    {
        RecibirDaño(cantidad);
        Debug.Log("Daño recibido de: " + fuente);
    }
}

