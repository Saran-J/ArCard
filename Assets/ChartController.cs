using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartController : MonoBehaviour
{
    float cValue = 0;
    float pValue = 0;
    float fValue = 0;
    public GameObject chart;
    public GameObject protein;
    public GameObject carb;
    public GameObject fat;
    public TextMesh pText;
    public TextMesh cText;
    public TextMesh fText;

    float pScalePoint = 1;
    float cScalePoint = 1;
    float fScalePoint = 1;

    Vector3 pScale = new Vector3(1, 1, 1);
    Vector3 cScale = new Vector3(1, 1, 1);
    Vector3 fScale = new Vector3(1, 1, 1);

    const float factorPlus = 1f;
    const float factorMinus = -1f;


    // Start is called before the first frame update
    void Start()
    {
        pScale = protein.transform.localScale;
        cScale = carb.transform.localScale;
        fScale = fat.transform.localScale;

        pScalePoint = pScale.z;
        cScalePoint = cScale.z;
        fScalePoint = fScale.z;

    }

    // Update is called once per frame
    void Update()
    {
        if (System.Math.Abs(pScale.z - pScalePoint) > 0) {
            var factor = pScale.z > pScalePoint ? factorMinus : factorPlus;
            pScale.z += factor;
            
        }
        

        if (System.Math.Abs(cScale.z - cScalePoint) > 0)
        {
            var factor = cScale.z > cScalePoint ? factorMinus : factorPlus;
            cScale.z += factor;
            cValue += factor;
        }
        

        if (System.Math.Abs(fScale.z - fScalePoint) > 0)
        {
            var factor = fScale.z > fScalePoint ? factorMinus : factorPlus;
            fScale.z += factor;
            fValue += factor;
        }
        pValue = pScalePoint;
        cValue = cScalePoint;
        fValue = fScalePoint;
        updateUI();
    }

    void updateUI() {
        protein.transform.localScale = pScale;
        carb.transform.localScale = cScale;
        fat.transform.localScale = fScale;

        pText.text = pValue + " G";
        cText.text = cValue + " G";
        fText.text = fValue + " G";
    }

    public void updateProtein(float val) {
        pScalePoint = val;
    }

    public void updateCarb(float val)
    {
        cScalePoint = val;
    }

    public void updateFat(float val)
    {
        fScalePoint = val;
    }
}
