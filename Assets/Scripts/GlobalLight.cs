using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLight : MonoBehaviour {
    private Light2D light2D;

    void Start() {
        light2D = GetComponent<Light2D>();

        light2D.intensity = 0;
    }
}
