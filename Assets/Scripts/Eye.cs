using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Eye : MonoBehaviour
{

    public Transform hero;
    public float speed = 2f;
    public float detectionDistance = 5f;
    public Animator animator;

    private SpriteRenderer sprite;

    private bool isChasing = false;

    private bool onLight = true;

    private void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Flashlight") && onLight) {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Flashlight")) {
            isChasing = false;
        }
    }

    void Update() {
        if (isChasing) {
            MoveTowardsHero();
        }

        if (Input.GetMouseButtonDown(1)) {
            isChasing = false;
            onLight = !onLight;
        }
    }

    private void MoveTowardsHero() {
        // Двигаем призрака к герою
        Vector3 direction = (hero.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        sprite.flipX = direction.x < 0.0f;
        // Проверяем столкновение с героем
        if (Vector3.Distance(transform.position, hero.position) < 0.5f) // Порог для столкновения
        {
            AttackHero();
        }
    }

    private void AttackHero() {
        // Запускаем анимацию удара
        /*if (animator != null) {
            animator.SetTrigger("Attack"); // Предполагается, что у вас есть триггер "Attack" в аниматоре
        }*/

        // Логика проигрыша игрока
        SceneManager.LoadScene(0);

    }
}
