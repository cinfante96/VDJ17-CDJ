﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBarUI : MonoBehaviour
{

    protected float value;
    public float speed = 1;

    public bool turningOn;
    public bool turningOff;
    private bool display;

    public float rightmost;
    public float leftmost;

    private Slider bar;
    public InputControl input;

    public Image ball;
    public Image slider;
    public Image zoneAcceptation;

    public Color ballColor;
    public Color sliderColor;
    public Color zoneAcceptationColor;

    private FadeMethods fade = new FadeMethods();

    private void Start()
    {
        bar = GetComponent<Slider>();
    }

    public void Update()
    {
        if (turningOn) {
            StartCoroutine(TurningOn());
        }

        if (display) {

            float progress = Mathf.Cos(speed * Time.time);
            value = Mathf.Clamp(-progress, -1, 1);

            bar.value = value;

            if (bar.value >= rightmost && bar.value <= leftmost)
            {
                if (input.action2WasPress && !turningOff)
                {
                    print("bien");
                    StartCoroutine(TurnOff());
                }
            }

            if (bar.value < rightmost || bar.value > leftmost)
            {
                if (input.action2WasPress && !turningOff)
                {
                    print("mal");
                    StartCoroutine(TurnOff());
                }
            }
        }
    }

    private IEnumerator TurningOn() {

        display = true;
        StartCoroutine(fade.ImageFadeIn(ball, ballColor));
        StartCoroutine(fade.ImageFadeIn(slider, sliderColor));
        StartCoroutine(fade.ImageFadeIn(zoneAcceptation, zoneAcceptationColor));

        yield return new WaitUntil(() => ball.color.a >= 0.95f);

        turningOn = false;
        yield break;
    }

    private IEnumerator TurnOff() {

        turningOff = true;

        StartCoroutine(fade.ImageFadeOut(ball));
        StartCoroutine(fade.ImageFadeOut(slider));
        StartCoroutine(fade.ImageFadeOut(zoneAcceptation));

        yield return new WaitUntil(() => ball.color.a <= 0.05f);

        display = false;
        turningOff = false;

        yield break;
    }
}
