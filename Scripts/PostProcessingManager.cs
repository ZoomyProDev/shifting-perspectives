using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{

    public Volume volume;
    private ChromaticAberration c;
    private Vignette v;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out c);
        volume.profile.TryGet(out v);

    }

    // Update is called once per frame
    void Update()
    {

    }



    public IEnumerator Transfer()
    {

        var t = 0.1f;


        while (t < 1)
        {
            c.intensity.value = Mathf.Lerp(0, 1f, t);
            v.intensity.value += 0.025f;

            yield return new WaitForSecondsRealtime(0.1f);

            t += 0.1f;

        }
        

        yield return null;
    }


    public IEnumerator UndoTransfer()
    {

        var i = 0.1f;


        while (i < 1)
        {

            c.intensity.value -= 0.1f;
            v.intensity.value -= 0.025f;

            yield return new WaitForSecondsRealtime(0.1f);

            i += 0.1f;


        }


        yield return null;
    }
}
