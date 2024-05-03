using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteUnmuteSound : MonoBehaviour
{
    public MusicManager musicManager; // Referencia al MusicManager que controla el AudioSource
    public Button muteButton; // Referencia al botón de silenciamiento

    private bool isMuted = false; // Estado de silenciamiento actual

    private void Start()
    {
        // Agregar un listener al botón para manejar su click
        muteButton.onClick.AddListener(ToggleMute);
    }

    private void ToggleMute()
    {
        // Cambiar el estado de silenciamiento y actualizar el icono del botón
        isMuted = !isMuted;

        // Silenciar o des-silenciar el MusicManager ajustando su volumen
        if (isMuted)
        {
            // Si está silenciado, establecer el volumen en 0
            musicManager.SetVolume(0f);
        }
        else
        {
            // Si no está silenciado, establecer el volumen al valor actual del MusicManager
            musicManager.SetVolume(musicManager.GetVolume());
        }
    }
}
