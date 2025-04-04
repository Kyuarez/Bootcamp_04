using Unity.Cinemachine;
using UnityEngine;
using System.Collections;


public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;

    #region Legacy
    private Camera cam;
    private CinemachineBrain cine;
    private Vector3 camOriginPos;

    private Coroutine coroutine;
    #endregion

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cine = GetComponent<CinemachineBrain>();
    }

    #region Cinemachine
    public void GenerateCameraImpulse()
    {
        if(impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
    
    #endregion

    #region Legacy
    public void OnCameraShake(float duration, float magnitude)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(CameraShakeCo(duration, magnitude));
    }


    private IEnumerator CameraShakeCo(float duration, float magnitude)
    {
        camOriginPos = transform.localPosition;
        if (cine != null)
        {
            cine.enabled = false;
        }

        if (cam == null)
        {
            yield break;
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(transform.localPosition.x, camOriginPos.y + y, -10);

            elapsedTime += Time.deltaTime;

            yield return null;

        }
        Camera.main.transform.localPosition = camOriginPos;
        if (cine != null)
        {
            cine.enabled = true;
        }
    }
    #endregion
}
