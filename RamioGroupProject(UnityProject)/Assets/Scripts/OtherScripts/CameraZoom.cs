using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    #region VARIABLES
    [Header("Camera Settings")]
    public float zoomed = 1.6f;
    public float normal = 4f;
    public float smooth = 5;
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (GetComponentInParent<PlayerCollision>().camZoom == true)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoomed, Time.deltaTime * smooth);
        else
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, normal, Time.deltaTime * smooth);
    }
    #endregion
}
