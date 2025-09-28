using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;
    [SerializeField]
    private LightingPreset Preset;

    [SerializeField, Range(0, 240)]
    private float TimeOfDay;



    private void Update()
    {
        if (Preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 240; //Clamp between 0-24
            UpdateLighting(TimeOfDay / 240f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 240f);
        }
    }






    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);


        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

        }
    }






    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {

            return;

        }
        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {

            DirectionalLight = RenderSettings.sun;

        }
        else
        {
            //Search scene for light that fits criteria (directional)
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }

        }

    }

}