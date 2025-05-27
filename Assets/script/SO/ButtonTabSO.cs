using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ButtonTabSO : ScriptableObject
{
    public ButtonsFactorySO Factory;

    [Header("Tap")]
    public Material normal;
    public Material star;

    [Header("Flick SC")]
    public Material SC_LV1;
    public Material SC_LV2;
    public Material SC_LV3;
    public Material SC_LV4;
    public Material SC_LV5;
    public Material SC_LV6;
    public Material SC_LV7;

    [Header("Flick Trash")]
    public Material Trash;

    [Header("Hold")]
    public Material Hold;

    [Header("Unknow")]
    public Material Unknow;
}
