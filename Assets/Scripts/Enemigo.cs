using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : Personaje
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            // Ejemplo de daño al jugador si es necesario
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.RecibirDaño(10);
            }
        }
    }

    public override void RecibirDaño(int cantidad)
    {
        base.RecibirDaño(cantidad);
        Debug.Log("El enemigo ha recibido daño");
    }

    protected override void Morir()
    {
        base.Morir();
        // Implementa comportamiento adicional para cuando un enemigo muere, p. ej., destruir el objeto
        Destroy(gameObject);
    }
}


