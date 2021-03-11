using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Postprocessing : MonoBehaviour
{
    private ColorAdjustments colorAdjustments;
    public Volume volume;
    public float speed;
    float value;
    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.sharedProfile.TryGet<ColorAdjustments>(out colorAdjustments);
    }
    void Update()
    {
        //volume.sharedProfile.TryGet<ColorAdjustments>();
        value += Time.deltaTime * speed;
        colorAdjustments.hueShift.SetValue(new NoInterpMinFloatParameter(value, -180, true));
    }
}
