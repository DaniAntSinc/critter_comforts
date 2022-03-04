using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalStatus : MonoBehaviour
{
    // Range values
    private const float ACTIVE_STRENGTH = 24.06f;
    private const float ACTIVE_DISSOLVE = 0.5f;
    private const float ACTIVE_INTENSITY = 1.2f;
    private const float ACTIVE_TIME = 0.22f;

    private const float INERT_STRENGTH = 9.74f;
    private const float INERT_DISSOLVE = 0.02f;
    private const float INERT_INTENSITY = 1f;
    private const float INERT_TIME = 0.07f;

    // Current, absolute state of the portal
    private string status;
    private Color portalColor;

    // Used for transitory adjustments
    private float currentStrength;
    private float currentDissolve;

    // Start is called before the first frame update
    void Start()
    {
        this.RerollColor();
        this.UpdateColor();
        this.SetStatus("inert");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Test method to see color variants. Remove once color is associated with destination
     */
    public void RerollColor()
    {
        float factor = this.GetStatus() == "active" ? ACTIVE_STRENGTH : INERT_STRENGTH;
        float hue = Random.Range(0f, 1f);
        float saturation = Random.Range(0.5f, 1f);
        float value = Random.Range(0.5f, 1f);

        Color rgb = Color.HSVToRGB(hue, saturation, value);
        this.portalColor = new Color(rgb.r * factor, rgb.g * factor, rgb.b * factor);
        this.UpdateColor();
    }

    /*
     * Just testing right now. I do see the issue with only calling this once, in that the intensity is never resolved.
     */
    void UpdateColor()
    {
        Color baseColor = this.GetPortalColor();
        GameObject particles = GameObject.Find("Particle System");
        ParticleSystem.MainModule settings = particles.GetComponent<ParticleSystem>().main;

        // TODO - Change this to "Random between Two Colors", and modify +- delta hue
        settings.startColor = baseColor;
        // TODO Change Portal color
        GameObject vortex = GameObject.Find("Vortex");
        Material shaderMaterial = vortex.GetComponent<Renderer>().material;
        // Color declared with HDR: Color(0, 5.36125612, 31.9999962, 0)
        shaderMaterial.SetColor("_PortalColor", baseColor);
    }

    protected Color GetPortalColor()
    {
        return this.portalColor;
    }

    public void SetStatus(string status)
    {
        this.status = status;
        GameObject vortex = GameObject.Find("Vortex");
        Material shaderMaterial = vortex.GetComponent<Renderer>().material;
        if (this.status == "active")
        {
            shaderMaterial.SetFloat("_TwirlStrength", ACTIVE_STRENGTH);
            shaderMaterial.SetFloat("_DissolveAmount", ACTIVE_DISSOLVE);
            shaderMaterial.SetFloat("_TimeDampen", ACTIVE_TIME);
        }
        else
        {
            shaderMaterial.SetFloat("_TwirlStrength", INERT_STRENGTH);
            shaderMaterial.SetFloat("_DissolveAmount", INERT_DISSOLVE);
            shaderMaterial.SetFloat("_TimeDampen", INERT_TIME);
        }
        this.UpdateColor();
    }

    // Should be used externally 
    public string GetStatus()
    {
        return this.status;
    }

    /* Maybe IEnumerator for SendEntityThrough that handles animation warping?
     * Set state to "consuming", then when the animation is done (change quad mesh to be "sucked in", along with any other neat UI things?) return to active
     */
}
