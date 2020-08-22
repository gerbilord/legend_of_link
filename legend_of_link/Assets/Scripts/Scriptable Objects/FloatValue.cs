using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    [NonSerialized]
    public float RuntimeValue;
    public void OnBeforeSerialize() {}
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
}
