// MYAT MON THU

using UnityEngine;

public class TrafficLightCombined : MonoBehaviour
{
    [Header("Bulb Renderers")]
    public Renderer redBulb;
    public Renderer yellowBulb;
    public Renderer greenBulb;

    [Header("Point Lights")]
    public Light redLight;
    public Light yellowLight;
    public Light greenLight;

    [Header("Timings")]
    public float redTime = 3f;
    public float yellowTime = 1f;
    public float greenTime = 3f;

    private void Start()
    {
        StartCoroutine(TrafficCycle());
    }

    private System.Collections.IEnumerator TrafficCycle()
    {
        while (true)
        {
            // RED
            SetLightState(true, false, false);
            yield return new WaitForSeconds(redTime);

            // GREEN
            SetLightState(false, false, true);
            yield return new WaitForSeconds(greenTime);

            // YELLOW
            SetLightState(false, true, false);
            yield return new WaitForSeconds(yellowTime);
        }
    }

    private void SetLightState(bool red, bool yellow, bool green)
    {
        // Toggle point lights
        redLight.enabled = red;
        yellowLight.enabled = yellow;
        greenLight.enabled = green;

        // Set emission glow
        SetEmission(redBulb, red ? Color.red : Color.black);
        SetEmission(yellowBulb, yellow ? Color.yellow : Color.black);
        SetEmission(greenBulb, green ? Color.green : Color.black);
    }

    private void SetEmission(Renderer bulb, Color color)
    {
        if (bulb != null)
        {
            bulb.material.SetColor("_EmissionColor", color);
            if (color != Color.black)
                DynamicGI.SetEmissive(bulb, color); // Makes GI react instantly
        }
    }
}
