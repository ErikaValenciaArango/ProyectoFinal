using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProsessing : MonoBehaviour
{

    public float intensity = 0;

    PostProcessVolume _volume;
    Vignette _vignette;

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<Vignette>(out _vignette);

        if (!_vignette)
        {
            print("error,vignette empty");
        }

        else
        {
            _vignette.enabled.Override(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
    public void DamageProsessing (int actualHealth)
    {
        _vignette.enabled.Override(true);
        _vignette.color.Override(new Color(128, 12, 12,255));

        switch (actualHealth)
        {
            case int health when health > 79 && health <= 99:
                _vignette.intensity.Override(0.05f);
                _vignette.smoothness.Override(0.2f);
                break;

            case int health when health > 59 && health <= 79:
                _vignette.intensity.Override(0.07f);
                _vignette.smoothness.Override(0.3f);
                break;

            case int health when health > 39 && health <= 59:
                _vignette.intensity.Override(0.09f);
                _vignette.smoothness.Override(0.4f);
                break;

            case int health when health > 19 && health <= 39:
                _vignette.intensity.Override(0.11f);
                _vignette.smoothness.Override(0.5f);
                break;

            case int health when health <= 19:
                _vignette.intensity.Override(0.13f);
                _vignette.smoothness.Override(0.6f);
                break;

            default:
                // Opcional: si el valor no coincide con ningún caso
                Debug.LogWarning("Health fuera de rango esperado.");
                break;
        }

    }
    
    public void HealthProsessing ()
    {
        StartCoroutine(TakeHealdEffect());
    }


    private IEnumerator TakeHealdEffect()
    {
        intensity = 0.2f;
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.2f);
        _vignette.smoothness.Override(0.09f);
        _vignette.color.Override(new Color(28,144,59));
        yield return new WaitForSeconds(0.2f);

        while( intensity > 0 )
        {
            intensity -= 0.01f;
            if ( intensity < 0 ) intensity = 0;
            _vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;

    }
}
