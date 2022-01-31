using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpObject : MonoBehaviour
{
    [SerializeField] AnimationCurve scaleWarpX;
    [SerializeField] AnimationCurve scaleWarpY;
    [SerializeField] AnimationCurve scaleWarpZ;
    float time;
    float timeRate;
    float fullDayLength;
    Vector3 scaleMultiplier;
    Vector3 startScale;

    
    // Start is called before the first frame update
    void Start()
    {
        fullDayLength = FindObjectOfType<DayNightCycle>().fullDayLength;
        timeRate = 1.0f / fullDayLength;
        time = 0f;        
        startScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        time += timeRate * Time.deltaTime;
        scaleMultiplier = new Vector3(scaleWarpX.Evaluate(time), scaleWarpY.Evaluate(time), scaleWarpZ.Evaluate(time));
        transform.localScale = new Vector3(startScale.x * scaleMultiplier.x, startScale.y * scaleMultiplier.y, startScale.z * scaleMultiplier.z);
    }
}
