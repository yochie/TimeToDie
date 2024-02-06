using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    [SerializeField]
    GameObject fillBar;

    [SerializeField]
    TextMeshProUGUI textLabel;

    public void SetVal(int val, int outOf)
    {
        this.fillBar.transform.localScale = new Vector3((float) val / outOf, 1, 1);
        this.textLabel.text = string.Format("{0} / {1}", val, outOf);
    }
}
