using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextAnimation : MonoBehaviour
{
    TextMeshProUGUI text1;
    float offset;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeGlow());
        text1 = GetComponent<TextMeshProUGUI>();
        text1.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOffset, 0f);
    }
    IEnumerator ChangeGlow()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (!toggle)
            {
                offset += 0.1f;
            }
            else
            {
                offset -= 0.1f;
            }
            if (offset >= 0.3f)
            {
                toggle = true;
            }
            else if (offset <= -0.5f)
            {
                toggle = false;
            }
            text1.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOffset, offset);
            //text1.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, offset - 0.3f);
        }


    }
    // Update is called once per frame
    void Update()
    {

    }
}
