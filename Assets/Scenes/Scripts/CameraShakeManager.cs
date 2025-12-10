using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{

    public static CameraShakeManager instance;
    [SerializeField] private float shakeForce = 1.0f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource source)
    {
        source.GenerateImpulseWithForce(shakeForce);
    }
}
