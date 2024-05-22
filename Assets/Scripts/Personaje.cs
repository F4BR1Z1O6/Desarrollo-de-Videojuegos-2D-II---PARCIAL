using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public int salud = 100;

    public virtual void RecibirDaño(int cantidad)
    {
        salud -= cantidad;
        if (salud <= 0)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        Debug.Log("El Personaje ha muerto");
    }
}

