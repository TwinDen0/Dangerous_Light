using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeLight : MonoBehaviour {

    private Light2D light2D;
    private bool onLight = true;
    private Color nowColor;
    public float fadeDuration = 1f;


    void Start() {
        light2D = GetComponent<Light2D>();
        nowColor = light2D.color;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (onLight) {
                if (nowColor == Color.white) {
                    light2D.color = Color.green; // Меняем на зеленый
                } else {
                    light2D.color = Color.white; // Меняем обратно на оригинальный цвет
                }
                nowColor = light2D.color;

                StartCoroutine(FadeLight());
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            if (onLight) {
                light2D.intensity = 0;
            }
            else {
                light2D.intensity = 1;
            }
            onLight = !onLight;
        }

    }

    private IEnumerator FadeLight() {
        float startIntensity;
        float targetIntensity;

        startIntensity = 0;
        targetIntensity = 1;

        float time = 0;

        while (time < fadeDuration) {
            if (Input.GetMouseButtonDown(1)) {
                light2D.intensity = 0;
                break;
            }
            time += Time.deltaTime;
            light2D.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / fadeDuration); // Интерполируем интенсивность
            yield return null;
        }
    }
}
