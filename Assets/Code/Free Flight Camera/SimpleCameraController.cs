using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    CameraState _targetCameraState        = new CameraState();
    CameraState _interpolatingCameraState = new CameraState();

    [Header("Movement Settings")]
    [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
    [SerializeField] float _boost = 3.5f;

    [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
    [SerializeField] float _positionLerpTime = 0.2f;

    [Header("Rotation Settings")]
    [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
    [SerializeField] AnimationCurve _mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    [SerializeField] float _rotationLerpTime = 0.01f;

    bool _frozen = true;

    public void Freeze()   => _frozen = true;
    public void UnFreeze() => _frozen = false;



    void OnEnable()
    {
        _targetCameraState.SetFromTransform(transform);
        _interpolatingCameraState.SetFromTransform(transform);
    }

    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(KeyCode.W))
            direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;
        if (Input.GetKey(KeyCode.Q))
            direction += Vector3.down;
        if (Input.GetKey(KeyCode.E))
            direction += Vector3.up;

        return direction;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _frozen = !_frozen;

        if (_frozen)
            return;

        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

        float mouseSensitivityFactor = _mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

        _targetCameraState.yaw   += mouseMovement.x * mouseSensitivityFactor;
        _targetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;

        Vector3 translation = GetInputTranslationDirection() * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            ApplySprintMultiplier(ref translation);

        HandleBoostFactor(ref translation);

        _targetCameraState.Translate(translation);

        // Framerate-independent interpolation
        // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
        float positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / _positionLerpTime) * Time.deltaTime);
        float rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / _rotationLerpTime) * Time.deltaTime);

        _interpolatingCameraState.LerpTowards(_targetCameraState, positionLerpPct, rotationLerpPct);

        _interpolatingCameraState.UpdateTransform(transform);
    }

    void ApplySprintMultiplier(ref Vector3 inTranslation) =>
        inTranslation *= 10.0f;

    void HandleBoostFactor(ref Vector3 inTranslation)
    {
        _boost += Input.mouseScrollDelta.y * 0.2f;
        inTranslation *= Mathf.Pow(2.0f, _boost);
    }



    class CameraState
    {
        public float yaw;
        public float pitch;
        public float roll;

        public float x;
        public float y;
        public float z;

        public void SetFromTransform(Transform inTransform)
        {
            pitch = inTransform.eulerAngles.x;
            yaw = inTransform.eulerAngles.y;
            roll = inTransform.eulerAngles.z;

            x = inTransform.position.x;
            y = inTransform.position.y;
            z = inTransform.position.z;
        }

        public void Translate(Vector3 inTranslation)
        {
            Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * inTranslation;

            x += rotatedTranslation.x;
            y += rotatedTranslation.y;
            z += rotatedTranslation.z;
        }

        public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

            x = Mathf.Lerp(x, target.x, positionLerpPct);
            y = Mathf.Lerp(y, target.y, positionLerpPct);
            z = Mathf.Lerp(z, target.z, positionLerpPct);
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
            t.position = new Vector3(x, y, z);
        }
    }
}
