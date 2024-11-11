using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightFinal : MonoBehaviour {
    private Light2D light2D;

    void Start() {
        light2D = GetComponent<Light2D>();
        light2D.intensity = 0;

        // Запускаем корутину
        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight() {
        // Ждем 3 секунды перед началом мигания
        yield return new WaitForSeconds(2f);

        while (true) {
            // Мигаем: включаем свет
            light2D.intensity = 1;
            yield return new WaitForSeconds(1f); // Задержка включения

            // Выключаем свет
            light2D.intensity = 0;
            yield return new WaitForSeconds(3f); // Задержка выключения
        }
    }
}
