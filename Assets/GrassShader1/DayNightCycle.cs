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
    [SerializeField] Color cloudColor;

    public bool isStormy;
    [SerializeField] Color stormSkyColor;
    [SerializeField] Color stormCloudColor;



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

        sun.transform.eulerAngles = new Vector3(210f * time - 20, 0f, 0f);
        moon.transform.eulerAngles = (time) * noon * 2.0f;

        if(!isStormy)
        {
            //set rotation;
            

            //light intensity;
            sun.intensity = sunIntensity.Evaluate(time);
            moon.intensity = moonIntensity.Evaluate(time);
            sun.color = sunColor.Evaluate(time);

            //color;
            
            skyboxMaterial.SetColor("_SunColor", sunColor.Evaluate(time));
            skyboxMaterial.SetColor("_Horizon", horizonColor.Evaluate(time));
            skyboxMaterial.SetColor("_Nadir", horizonColor.Evaluate(time));
            skyboxMaterial.SetColor("_Zenith", skyColor.Evaluate(time));
            skyboxMaterial.SetFloat("_CloudOpacity", cloudOpacity.Evaluate(time));
            skyboxMaterial.SetFloat("_StarOpacity", starOpacity.Evaluate(time));
            skyboxMaterial.SetFloat("_CloudColorIntensity", 1f);
            moon.color = moonColor.Evaluate(time);
            skyboxMaterial.SetColor("_CloudColor", cloudColor);

            RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
            Shader.SetGlobalVector("_SunDirection", transform.forward);
        }
        else
        {
            skyboxMaterial.SetColor("_SunColor", stormSkyColor);
            sun.intensity = 0.2f;
            skyboxMaterial.SetColor("_Horizon", stormSkyColor);
            skyboxMaterial.SetColor("_Nadir", stormSkyColor);
            skyboxMaterial.SetColor("_Zenith", stormSkyColor);
            skyboxMaterial.SetFloat("_CloudOpacity", 1f);
            skyboxMaterial.SetFloat("_CloudColorIntensity", 10f);
            skyboxMaterial.SetColor("_CloudColor", stormCloudColor);
        }
        
    }
}
