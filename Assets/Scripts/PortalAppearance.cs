using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTaskManager;

public class PortalAppearance : MonoBehaviour
{
    private Task transitionTask;

    [System.Serializable]
    public struct VisualConfig
    {
        public float twirlStrength;
        public float timeDampen;
        public float dissolveAmount;
        public float intensity;
        public float emission;

        public VisualConfig(float ts, float td, float da, float i, float e)
        {
            this.twirlStrength = ts;
            this.timeDampen = td;
            this.dissolveAmount = da;
            this.intensity = i;
            this.emission = e;
        }
    };
    private string cachedStatus;

    [Header("Shader Settings"), SerializeField]
    private VisualConfig activeConfig;
    [SerializeField]
    private VisualConfig inertConfig;
    private VisualConfig currentConfig;
    [SerializeField]
    private float transitionTime;

    [System.Serializable]
    public class DestinationColorMap
    {
        public string name;
        public Color portalColor;
    }
    [Header("Destination Color")]
    public Color defaultColor;
    public DestinationColorMap[] colorMap;

    // Start is called before the first frame update
    void Start()
    {
        this.transitionTask = null;
        this.InitializeParticles();
        this.InitializeVortex();
    }

    // Update is called once per frame
    void Update()
    {
        // If cached state is different than portalStatus state, invoke coroutine
        // ...if coroutine is done, cache state?
        // Float a bit? position delta
        this.TransitionStateIfChanged();
    }

    private void TransitionStateIfChanged()
    {
        if (this.cachedStatus == gameObject.GetComponent<PortalStatus>().GetStatus())
        {
            // No changes needed
            return;
        }
        // If task is running, don't spawn another
        if (this.transitionTask != null && this.transitionTask.Running)
        {
            return;
        }
        // Create task as the IEnumerator below
        this.transitionTask = new Task(TransitionAppearance());
    }

    private GameObject GetParticles()
    {
        return gameObject.transform.Find("Particle System").gameObject;
    }

    private Material GetVortex()
    {
        GameObject vortex = gameObject.transform.Find("Vortex").gameObject;
        return vortex.GetComponent<Renderer>().material;
    }

    private void InitializeParticles()
    {
        Color baseColor = this.GetColorFromDestination();
        GameObject particles = this.GetParticles();

        ParticleSystem.MainModule settings = particles.GetComponent<ParticleSystem>().main;
        // TODO - Change this to "Random between Two Colors", and modify +- delta hue
        settings.startColor = baseColor;
    }

    private void InitializeVortex()
    {
        Color baseColor = this.GetColorFromDestination();
        Material vortex = this.GetVortex();
        // Color declared with HDR: Color(0, 5.36125612, 31.9999962, 0)
        vortex.SetColor("_PortalColor", baseColor);
    }

    // Apply the settings in this.currentConfig to the components
    private void Redraw()
    {
        Material shaderMaterial = gameObject.transform.Find("Vortex").GetComponent<Renderer>().material;
        shaderMaterial.SetFloat("_TwirlStrength", this.currentConfig.twirlStrength);
        shaderMaterial.SetFloat("_DissolveAmount", this.currentConfig.dissolveAmount);
        shaderMaterial.SetFloat("_TimeDampen", this.currentConfig.timeDampen);

        Color baseColor = this.GetColorFromDestination();

        shaderMaterial.SetColor(
            "_TimeDampen",
            new Color(
                baseColor.r * this.currentConfig.intensity,
                baseColor.g * this.currentConfig.intensity,
                baseColor.b * this.currentConfig.intensity
            )
        );
        GameObject particles = this.GetParticles();

        ParticleSystem.EmissionModule settings = particles.GetComponent<ParticleSystem>().emission;
        settings.rateOverTime = this.currentConfig.emission;
    }

    private Color GetColorFromDestination()
    {
        /* Premature optimization concerns - what if the portal destination should be a mystery?
         */
        string portalDestination = GetComponent<PortalStatus>().GetDestination();
        // Find portalDestination in this.colorMap.name
        DestinationColorMap mapElement = Array.Find(colorMap, e => e.name == portalDestination);
        if (mapElement == null)
        {
            return this.defaultColor;
        }
        // TODO Check mapElement null?
        return mapElement.portalColor;
    }

    IEnumerator TransitionAppearance()
    {
        float time = 0;
        string currentStatus = GetComponent<PortalStatus>().GetStatus();
        // Record initial value for lerp function
        VisualConfig startConfig = this.currentConfig;
        VisualConfig targetConfig;
        switch (currentStatus)
        {
            case "active":
                targetConfig = this.activeConfig;
                break;
            case "inert":
            default:
                targetConfig = this.inertConfig;
                break;
        }

        while (time < this.transitionTime)
        {
            float t = time / this.transitionTime;
            t = t * t; //1 - Mathf.Cos(t * Mathf.PI * 0.5f); // coserp
            
            this.currentConfig.twirlStrength = Mathf.Lerp(startConfig.twirlStrength, targetConfig.twirlStrength, t);
            this.currentConfig.timeDampen = Mathf.Lerp(startConfig.timeDampen, targetConfig.timeDampen, t);
            this.currentConfig.dissolveAmount = Mathf.Lerp(startConfig.dissolveAmount, targetConfig.dissolveAmount, t);
            this.currentConfig.intensity = Mathf.Lerp(startConfig.intensity, targetConfig.intensity, t);

            this.currentConfig.emission = Mathf.Lerp(startConfig.emission, targetConfig.emission, t);
            // Redraw handles the color intensity fix, so we only need to scale the value
            this.Redraw();
            time += Time.deltaTime;
            yield return null;
        }
        // Once the time limit is met, set everything to the true target (allowing time to be a bit scale-y)
        this.currentConfig = targetConfig;
        this.cachedStatus = currentStatus;
        this.Redraw();
        this.transitionTask.Stop();
    }
}
