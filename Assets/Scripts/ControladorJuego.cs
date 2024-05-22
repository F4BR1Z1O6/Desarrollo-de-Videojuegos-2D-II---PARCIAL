using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    public delegate void OnGameStateChange(GameState newState);
    public static event OnGameStateChange gameStateChange;

    public enum GameState { Start, Playing, GameOver } // Cambiar a public
    private GameState currentState;

    private void Start()
    {
        currentState = GameState.Start;
        StartCoroutine(InicioJuego());
    }

    private IEnumerator InicioJuego()
    {
        Debug.Log("El juego está empezando");
        yield return new WaitForSeconds(3f);
        CambiarEstado(GameState.Playing);
        Debug.Log("¡Comienza la acción!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEstado(GameState.Playing);
        }

        // Operador ternario
        string estado = currentState == GameState.Playing ? "Jugando" : "No Jugando";
        Debug.Log("Estado del juego: " + estado);
    }

    private void CambiarEstado(GameState newState)
    {
        currentState = newState;
        gameStateChange?.Invoke(newState);
    }
}


