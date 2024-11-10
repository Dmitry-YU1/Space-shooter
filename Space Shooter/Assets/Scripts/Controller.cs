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
        // ������������� ��������� ��������� � ������
        slider.value = AudioListener.volume;
        UpdateAudioIcon();

        // ������������� �� ��������� ��������
        slider.onValueChanged.AddListener(SetVolume);
    }

    // ����� ��� ��������� ��������� ����� �������
    private void SetVolume(float volume)
    {
        if (!isMuted) // �������� ��������� ������ ���� ���� �������
        {
            AudioListener.volume = volume;
        }
    }

    // ����� ��� ������������ ����� (��������/���������)
    public void OnOffAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : slider.value;
        UpdateAudioIcon();
    }

    // ����� ��� ���������� ������ ������
    private void UpdateAudioIcon()
    {
        buttonAudio.GetComponent<Image>().sprite = isMuted ? audioOff : audioOn;
    }

    // ����� ��� ������������ �����
    public void PlaySound()
    {
        audio.PlayOneShot(clip);
    }
}

///audio.PlayOneShot(clip);
/// audio.Play();