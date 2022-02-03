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
    [SerializeField] GameObject rainEffect;
    float timeSinceLastChange;



    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        timeSinceLastChange = 1.1f;
        isStormy = false;

        skyboxMaterial.SetColor("_SunColor",  skyColor.Evaluate(0f));
        sun.intensity = 0.3f;
        skyboxMaterial.SetColor("_Horizon", skyColor.Evaluate(0f));
        skyboxMaterial.SetColor("_Zenith", skyColor.Evaluate(0f));

        skyboxMaterial.SetFloat("_CloudOpacity", cloudOpacity.Evaluate(0f));
        skyboxMaterial.SetFloat("_CloudColorIntensity", 1f);
        skyboxMaterial.SetColor("_CloudColor", cloudColor);
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
            
            if(timeSinceLastChange < 0.251f)
            {
                skyboxMaterial.SetColor("_SunColor",  Color.Lerp(stormSkyColor, skyColor.Evaluate(time),  timeSinceLastChange * 4));
                sun.intensity = Mathf.Lerp(1f, 0.3f, timeSinceLastChange * 4);
                skyboxMaterial.SetColor("_Horizon", Color.Lerp(stormSkyColor, horizonColor.Evaluate(time),  timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_Nadir", Color.Lerp(stormSkyColor, horizonColor.Evaluate(time),  timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_Zenith", Color.Lerp(stormSkyColor, skyColor.Evaluate(time),  timeSinceLastChange * 4));

                skyboxMaterial.SetFloat("_CloudOpacity", Mathf.Lerp( 1f, cloudOpacity.Evaluate(time), timeSinceLastChange * 4));
                skyboxMaterial.SetFloat("_CloudColorIntensity", Mathf.Lerp(10f, 1f,  timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_CloudColor", Color.Lerp(stormCloudColor, cloudColor, timeSinceLastChange * 4));
            }

            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
            Shader.SetGlobalVector("_SunDirection", transform.forward);
            rainEffect.SetActive(false);
            timeSinceLastChange += Time.deltaTime;


            if(timeSinceLastChange > 10f)
            {
                if(Random.Range(0f, 1000f) == 1f)
                {
                    timeSinceLastChange = 0f;
                    isStormy = true;
                }
                if(timeSinceLastChange > 10f)
                {
                    timeSinceLastChange = 0f;
                    isStormy = true;
                }
                
            }

        }
        else
        {
            if(timeSinceLastChange < 0.251f)
            {
                skyboxMaterial.SetColor("_SunColor", Color.Lerp(skyColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 4));
                sun.intensity = Mathf.Lerp(1f, 0.3f, timeSinceLastChange * 4);
                skyboxMaterial.SetColor("_Horizon", Color.Lerp(horizonColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_Nadir", Color.Lerp(skyColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_Zenith", Color.Lerp(skyColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 4));

                skyboxMaterial.SetFloat("_CloudOpacity", Mathf.Lerp(cloudOpacity.Evaluate(time), 1f, timeSinceLastChange * 4));
                skyboxMaterial.SetFloat("_CloudColorIntensity", Mathf.Lerp(1f, 10f, timeSinceLastChange * 4));
                skyboxMaterial.SetColor("_CloudColor", Color.Lerp(cloudColor, stormCloudColor, timeSinceLastChange * 4));
            } 
            
            
            
            rainEffect.SetActive(true);
            timeSinceLastChange += Time.deltaTime;

            if(timeSinceLastChange > 3f)
            {
                timeSinceLastChange = 0f;
                isStormy = false;
            }
            
        }
        
    }
}
