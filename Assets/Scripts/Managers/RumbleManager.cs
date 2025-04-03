using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using System;

public class RumbleManager : MonoSingleton<RumbleManager>
{
    private Gamepad _gamepad;
    private Coroutine _stopRumbleAfterTimeCoroutine;
    public void RumblePulse(float lowFrequncy,float highFrequncy, float duration)
    {
        _gamepad = Gamepad.current;

        if(_gamepad == null) return;

        _gamepad.SetMotorSpeeds(lowFrequncy,highFrequncy);

        _stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumple(duration, _gamepad));
    }

    private IEnumerator StopRumple(float duration, Gamepad gamepad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gamepad.SetMotorSpeeds(0f,0f);
    }
}
