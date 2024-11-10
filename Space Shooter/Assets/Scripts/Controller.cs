using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("UI Components")]
    public Sprite audioOn;
    public Sprite audioOff;
    public GameObject buttonAudio;
    public Slider slider;

    [Header("Audio Components")]
    public AudioClip clip;
    public AudioSource audio;

    private bool isMuted = false;

    private void Start()
    {
        // Устанавливаем начальную громкость и иконку
        slider.value = AudioListener.volume;
        UpdateAudioIcon();

        // Подписываемся на изменения слайдера
        slider.onValueChanged.AddListener(SetVolume);
    }

    // Метод для изменения громкости через слайдер
    private void SetVolume(float volume)
    {
        if (!isMuted) // Изменяем громкость только если звук включен
        {
            AudioListener.volume = volume;
        }
    }

    // Метод для переключения звука (включить/выключить)
    public void OnOffAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : slider.value;
        UpdateAudioIcon();
    }

    // Метод для обновления иконки кнопки
    private void UpdateAudioIcon()
    {
        buttonAudio.GetComponent<Image>().sprite = isMuted ? audioOff : audioOn;
    }

    // Метод для проигрывания звука
    public void PlaySound()
    {
        audio.PlayOneShot(clip);
    }
}

///audio.PlayOneShot(clip);
/// audio.Play();