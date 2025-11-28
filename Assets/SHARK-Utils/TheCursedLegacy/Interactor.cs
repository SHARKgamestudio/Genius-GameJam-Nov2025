using UnityEngine;
using UnityEngine.Events;

// Declare interfaces
interface IInteractable
{
    /// <summary>
    /// Called each frames when the player can interact with the object
    /// </summary>
    /// <param name="interactor"></param>
    void OnInteractable(Interactor interactor);

    /// <summary>
    /// Called once when the player starts interacting with the object
    /// </summary>
    /// <param name="interactor"></param>
    void OnInteractBegin(Interactor interactor);
    /// <summary>
    /// Called each frames when the player is interacting with the object
    /// </summary>
    /// <param name="interactor"></param>
    void OnInteract(Interactor interactor);
    /// <summary>
    /// Called once when the player stops interacting with the object
    /// </summary>
    /// <param name="interactor"></param>
    void OnInteractEnd(Interactor interactor);
}

public class Interactor : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("The key used by player to interact.")]
    [SerializeField] KeyCode interactKey = KeyCode.E;

    [Range(0f, 8f)]
    [Tooltip("The maximum distance on which the player can interact.")]
    [SerializeField] float maxDistance = 2.6f;

    [Tooltip("The layer(s) that will be used to interact.")]
    [SerializeField] LayerMask interactMask;

    [Tooltip("Can the interaction system work with 'trigger' colliders ? (not recommended to change).")]
    [SerializeField] QueryTriggerInteraction interactWithTriggers = QueryTriggerInteraction.Ignore;

    [Space]

    [Header("Events")]
    [Tooltip("This event executes when the player can interact with an object.")]
    [SerializeField] UnityEvent OnInteractable;

    [Tooltip("This event executes when the player starts to interact with an object")]
    [SerializeField] UnityEvent OnInteractBegin;

    [Tooltip("This event executes when the player is interacting with an object.")]
    [SerializeField] UnityEvent OnInteract;

    [Tooltip("This event executes when the player stops to interact with an object.")]
    [SerializeField] UnityEvent OnInteractEnd;

    // Shared Vars
    public static Interactor instance;
    [HideInInspector] public Vector3 placeholder;
    [HideInInspector] public bool interactable, interacting;

    // Privates
    RaycastHit hit;
    IInteractable interfaceComponent;

    void Awake(){  instance = this; }

    void Update()
    {
        // Check if we hit interactable object in view
        LayerMask excluder = new LayerMask().Exclude(LayerMask.NameToLayer("Ignore Raycast"));

        interactable = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, excluder, interactWithTriggers)
            && !interacting && interactMask.Contains(hit.transform.gameObject.layer);
        
        #region Handle Interaction
        if (interactable)
        {
            // Get component from interactable that possess 'IInteractable'
            hit.transform.gameObject.TryGetComponent<IInteractable>(out interfaceComponent);

            // Check for user inputs
            if (Input.GetKeyDown(interactKey))
            {
                // Execute abstract method using the interface
                interfaceComponent.OnInteractBegin(instance);

                // Execute unity-event
                OnInteractBegin.Invoke();

                // Update state
                interacting = true;
            }

            // Execute abstract method using the interface
            if (interfaceComponent != null) interfaceComponent.OnInteractable(instance);

            // Execute unity-event
            OnInteractable.Invoke();
        }

        if(interacting)
        {
            // Execute abstract method using the interface
            if (interfaceComponent != null) interfaceComponent.OnInteract(instance);

            // Execute unity-event
            OnInteract.Invoke();
        }

        if(Input.GetKeyUp(interactKey))
        {
            // Execute abstract method using the interface
            if(interfaceComponent != null) interfaceComponent.OnInteractEnd(instance);

            // Execute unity-event
            OnInteractEnd.Invoke();

            // Update state
            interacting = false;
        }
        #endregion

        // Update placeholder position
        placeholder = transform.position + transform.forward * 0.5f;
    }

    void OnDrawGizmos()
    {
        Vector3 direction;

        // Change displayed data depending on editor/game mode ( to avoid errors in editor )
        if (Application.isPlaying && interactable)
        { Gizmos.color = Color.red; direction = hit.point; }
        else
        { Gizmos.color = Color.green; direction = transform.position + transform.forward * maxDistance; }

        // Display gizmos
        Gizmos.DrawLine(transform.position, direction);
        Gizmos.DrawWireSphere( direction, 0.1f);

    }
}