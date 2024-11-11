using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrokeFleshlight : MonoBehaviour
{
    private Light2D light2D;

    void Start() {
        light2D = GetComponent<Light2D>();
        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight() {
        // Ждем 2 секунды перед началом мигания
        yield return new WaitForSeconds(0.5f);

        // Количество миганий
        int blinkCount = 3;

        for (int i = 0; i < blinkCount; i++) {
            // Включаем свет
            light2D.intensity = 1;
            yield return new WaitForSeconds(0.2f); // Задержка включения

            // Выключаем свет
            light2D.intensity = 0;
            yield return new WaitForSeconds(0.2f); // Задержка выключения
        }

        // После завершения мигания выключаем свет окончательно
        light2D.intensity = 0;
    }
}
