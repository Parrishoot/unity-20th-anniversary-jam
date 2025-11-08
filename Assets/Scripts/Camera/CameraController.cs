using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum ShakeType
    {
        LIGHT,
        HEAVY
    }

    public struct ShakeProfile
    {
        public ShakeProfile(float shakeAmount, float shakeTime)
        {
            ShakeAmount = shakeAmount;
            ShakeTime = shakeTime;
        }

        public float ShakeAmount { get; }

        public float ShakeTime { get; }
    }

    private Dictionary<ShakeType, ShakeProfile> shakeMap = new Dictionary<ShakeType, ShakeProfile>()
    {
        { ShakeType.HEAVY, new ShakeProfile(.4f, .25f)},
        { ShakeType.LIGHT, new ShakeProfile(.125f, .125f)},
    };

    private Tween currentShake;

    public void Shake(ShakeType shakeType)
    {
        if (currentShake != null)
        {
            currentShake.Complete();
        }

        ShakeProfile shakeProfile = shakeMap[shakeType];

        transform.DOShakePosition(duration: shakeProfile.ShakeTime, strength: shakeProfile.ShakeAmount, vibrato: 100);
    }
}
