using UnityEngine;

public class CameraTweening : MonoBehaviour
{
    [Header("Tweening")]
    [SerializeField] TweenTransform start = new TweenTransform(new Vector3(-0.05f, 1.06f, -0.25f), new Vector3(0, 0, 0));
    [SerializeField] TweenTransform end = new TweenTransform(new Vector3(-1.5f, 1.65f, -3.15f), new Vector3(13, 34, 0));

    [Space]

    [Header("Scrolling")]
    [Range(0, 100)] [SerializeField] float scrollSensivity = 32;
    [Range(0, 10)]  [SerializeField] float scrollSmoothing = 2;

    [Space]

    [Header("Looking")]
    [Range(0, 10)][SerializeField] float lookSensivity = 0.16f;
    [Range(0, 10)][SerializeField] float lookSmoothing = 4;
    [Range(0, 10)][SerializeField] float lookClamp = 2;

    // Shared
    [SerializeField] public float completed;

    // Private
    bool travel;
    Vector3 mouse;
    float velocity;
    float pitch, yaw, roll;

    void Start() { travel = true; }

    public void PauseTraveling() { travel = false; }
    public void ResumeTraveling() { travel = true; }

    public void TravelTo(float value) { velocity = (completed - value) / 16; }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * lookSensivity;
        float y = Input.GetAxis("Mouse Y") * lookSensivity;

        pitch -= y;
        pitch = Mathf.Clamp(pitch, -lookClamp, lookClamp);

        yaw += x;
        yaw = Mathf.Clamp(yaw, -lookClamp, lookClamp);

        roll += 0;
        roll = Mathf.Clamp(roll, -lookClamp, lookClamp);

        Vector3 rotation = new Vector3(pitch, yaw, roll);
        mouse = Vector3.Lerp(mouse, rotation, Time.deltaTime * lookSmoothing);

        if (Input.mouseScrollDelta.y == 0) { velocity = Mathf.Lerp(velocity, 0, Time.deltaTime * scrollSmoothing); }
        else if(travel) { velocity = Input.mouseScrollDelta.y; }

        completed -= velocity * scrollSensivity * Time.deltaTime;
        completed = Mathf.Clamp(completed, 0, 100);

        transform.position = Vector3.Lerp(start.position, end.position, completed / 100);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(start.rotation), Quaternion.Euler(end.rotation + mouse), completed / 100);
    }
}