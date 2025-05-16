using UnityEngine;

[ExecuteInEditMode]
public class FPSCap : MonoBehaviour
{
    [SerializeField] private int frameRate = 60;

    private void Start()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
#endif
    }
}
