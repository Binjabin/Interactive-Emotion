using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float time;
    public float fullDayLength;
    [SerializeField] float startTime = 0.0f;
    float timeRate;
    [SerializeField] Vector3 noon;

    [Header("Sun")]
    [SerializeField] Light sun;
    [SerializeField] Gradient sunColor;
    [SerializeField] AnimationCurve sunIntensity;

    [Header("Moon")]
    [SerializeField] Light moon;
    [SerializeField] Gradient moonColor;
    [SerializeField] AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    [SerializeField] AnimationCurve lightingIntensityMultiplier;
    [SerializeField] AnimationCurve reflectionsIntensityMultiplier;
    [SerializeField] Material skyboxMaterial;

    [Header("Sky")]
    [SerializeField] Gradient horizonColor;
    [SerializeField] Gradient skyColor;
    [SerializeField] AnimationCurve cloudOpacity;
    [SerializeField] AnimationCurve starOpacity;


    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        //time loop
        time += timeRate * Time.deltaTime;
        if(time >= 1.0f)
        {
            Debug.Log("End Of Day");
        }

        //set rotation;
        sun.transform.eulerAngles = (time) * noon * 2.0f;
        moon.transform.eulerAngles = (time) * noon * 2.0f;

        //light intensity;
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        //color;
        sun.color = sunColor.Evaluate(time);
        skyboxMaterial.SetColor("_SunColor", sunColor.Evaluate(time));
        skyboxMaterial.SetColor("_Horizon", horizonColor.Evaluate(time));
        skyboxMaterial.SetColor("_Nadir", horizonColor.Evaluate(time));
        skyboxMaterial.SetColor("_Zenith", skyColor.Evaluate(time));
        skyboxMaterial.SetFloat("_CloudOpacity", cloudOpacity.Evaluate(time));
        skyboxMaterial.SetFloat("_StarOpacity", starOpacity.Evaluate(time));
        moon.color = moonColor.Evaluate(time);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
        Shader.SetGlobalVector("_SunDirection", transform.forward);
    }
}
