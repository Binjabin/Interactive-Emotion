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

    float changeTime;

    [SerializeField] Material grassMaterial;

    [SerializeField] AudioSource thunderAudio;
    [SerializeField] AudioSource rainAudio;
    [SerializeField] AudioSource birdsAudio;
    [SerializeField] AudioSource musicAudio;

    bool playedEndingTheme;
    bool endedGame;

    // Start is called before the first frame update
    void Start()
    {
        playedEndingTheme = false;
        endedGame = false;
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        timeSinceLastChange = 0.251f;
        isStormy = false;
        changeTime = Random.Range(100f, 1000f);

        skyboxMaterial.SetColor("_SunColor",  skyColor.Evaluate(0f));
        sun.intensity = 0.3f;
        skyboxMaterial.SetColor("_Horizon", skyColor.Evaluate(0f));
        skyboxMaterial.SetColor("_Zenith", skyColor.Evaluate(0f));

        skyboxMaterial.SetFloat("_CloudOpacity", cloudOpacity.Evaluate(0f));
        skyboxMaterial.SetFloat("_StarOpacity", starOpacity.Evaluate(0f));
        skyboxMaterial.SetFloat("_CloudColorIntensity", 1f);
        skyboxMaterial.SetColor("_CloudColor", cloudColor);
    }

    // Update is called once per frame
    void Update()
    {
        skyboxMaterial.SetFloat("_StarOpacity", starOpacity.Evaluate(time));
        //time loop
        time += timeRate * Time.deltaTime;
        if(time >= 0.9f)
        {
            if(!playedEndingTheme)
            {
                StartCoroutine(FindObjectOfType<OpeningSequence>().EndTheme());
                playedEndingTheme = true;
            }
        }
        if(time >= 1.0f)
        {
            if(!endedGame)
            {
                FindObjectOfType<OpeningSequence>().EndGame();
                endedGame = true;  
            }
        }

        sun.transform.eulerAngles = new Vector3((time * 500f * 0.375f) - 3.75f, 0, 0);

        

        if(!isStormy)
        {
            //set rotation;
            

            //light intensity;
            sun.intensity = sunIntensity.Evaluate(time);
            sun.color = sunColor.Evaluate(time);
            grassMaterial.SetFloat("_WindSpeed", 30f);
            grassMaterial.SetFloat("_WindStrength", 0.1f);


            //color;
            
            if(timeSinceLastChange < 0.10001f)
            {
                skyboxMaterial.SetColor("_SunColor",  Color.Lerp( stormSkyColor, sunColor.Evaluate(time), timeSinceLastChange * 10));
                sun.intensity = Mathf.Lerp(0.3f, sunIntensity.Evaluate(time), timeSinceLastChange * 10);
                skyboxMaterial.SetColor("_Horizon", Color.Lerp(stormSkyColor, horizonColor.Evaluate(time),  timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_Nadir", Color.Lerp(stormSkyColor, horizonColor.Evaluate(time),  timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_Zenith", Color.Lerp(stormSkyColor, skyColor.Evaluate(time),  timeSinceLastChange * 10));

                skyboxMaterial.SetFloat("_CloudOpacity", Mathf.Lerp( 1f, cloudOpacity.Evaluate(time), timeSinceLastChange * 10));
                skyboxMaterial.SetFloat("_CloudColorIntensity", Mathf.Lerp(10f, 1f,  timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_CloudColor", Color.Lerp(stormCloudColor, cloudColor, timeSinceLastChange * 10));
            }
            else
            {
                skyboxMaterial.SetColor("_SunColor", sunColor.Evaluate(time));
                sun.intensity = sunIntensity.Evaluate(time);
                skyboxMaterial.SetColor("_Horizon", horizonColor.Evaluate(time));
                skyboxMaterial.SetColor("_Nadir", horizonColor.Evaluate(time));
                skyboxMaterial.SetColor("_Zenith", skyColor.Evaluate(time));

                skyboxMaterial.SetFloat("_CloudOpacity", cloudOpacity.Evaluate(time));
                skyboxMaterial.SetFloat("_CloudColorIntensity", 1f);
                skyboxMaterial.SetColor("_CloudColor", cloudColor);
            }
            

            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
            Shader.SetGlobalVector("_SunDirection", transform.forward);
            rainEffect.SetActive(false);
            timeSinceLastChange += Time.deltaTime;
            if(time < 0.9f)
            {
                if(timeSinceLastChange > changeTime)
                {
                    timeSinceLastChange = 0f;
                    isStormy = true;
                    birdsAudio.Stop();
                    rainAudio.Play();
                    thunderAudio.Play();
                    FindObjectOfType<OpeningSequence>().Storm();
                }
            }
            
        }
        else
        {

            grassMaterial.SetFloat("_WindSpeed", 150f);
            grassMaterial.SetFloat("_WindStrength", 0.2f);

            if(timeSinceLastChange < 0.10001f)
            {
                skyboxMaterial.SetColor("_SunColor", Color.Lerp(sunColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 10));
                sun.intensity = Mathf.Lerp(1f, 0.3f, timeSinceLastChange * 10);
                skyboxMaterial.SetColor("_Horizon", Color.Lerp(horizonColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_Nadir", Color.Lerp(horizonColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_Zenith", Color.Lerp(skyColor.Evaluate(time), stormSkyColor, timeSinceLastChange * 10));

                skyboxMaterial.SetFloat("_CloudOpacity", Mathf.Lerp(cloudOpacity.Evaluate(time), 2f, timeSinceLastChange * 10));
                skyboxMaterial.SetFloat("_CloudColorIntensity", Mathf.Lerp(1f, 5f, timeSinceLastChange * 10));
                skyboxMaterial.SetColor("_CloudColor", Color.Lerp(cloudColor, stormCloudColor, timeSinceLastChange * 10));
            } 
            
            
            
            rainEffect.SetActive(true);
            timeSinceLastChange += Time.deltaTime;

            if(timeSinceLastChange > 6f)
            {
                timeSinceLastChange = 0f;
                changeTime = Random.Range(30f, 150f);
                isStormy = false;
                birdsAudio.Play();
                rainAudio.Stop();
                thunderAudio.Stop();
                FindObjectOfType<OpeningSequence>().Unstorm();
            }
            
        }
        
    }


    public void GoStormy()
    {
        timeSinceLastChange = 0f;
        isStormy = true;
        birdsAudio.Stop();
        rainAudio.Play();
        thunderAudio.Play();
        FindObjectOfType<OpeningSequence>().Storm();
    }
}
