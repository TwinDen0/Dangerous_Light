using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(0);

        }
    }

    // Update is called once per frame
    private void Update() {
        Run();
    }

    private void Run() {
        // Логика полета птицы
        transform.Translate(Vector2.right * 3.5f * Time.deltaTime);
        // Здесь можно добавить дополнительные эффекты, такие как анимация или звуки
    }
}
