using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public float flySpeed = 5f; // Скорость полета
    private bool isFlying = false;
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    private void Start() {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Flashlight") && !hasPlayedSound) {
            isFlying = true;

            if (audioSource != null) {
                hasPlayedSound = true;
                audioSource.Play();
            }
        }
    }

    private void Update() {
        if (isFlying) {
            Fly();
        }
    }

    private void Fly() {
        // Логика полета птицы
        transform.Translate(Vector2.left * flySpeed * Time.deltaTime);
        // Здесь можно добавить дополнительные эффекты, такие как анимация или звуки
    }
}
