using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toast : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] float lifetime = 3.5f;

    [Header("References")]
    [SerializeField] GameObject toast;

    // Shared
    public static Toast instance;
    [HideInInspector] public bool displaying;

    // Private
    GameObject currentToast;

    void Awake() { instance = this; }

    void Start(){ UIManager.instance.Show(UIManager.Type.toast); }

    public void SendInfo(string message){ StartCoroutine(SendToast(message, Color.white)); }
    public void SendWarning(string message) { StartCoroutine(SendToast(message, Color.red)); }
    public void SendCustom(string message, Color color) { StartCoroutine(SendToast(message, color)); }

    IEnumerator SendToast(string text, Color color)
    {
        // Destroy toast already on display if needed
        DestroyCurrentToast();

        // Instanciate & Reference components
        GameObject toastInstance = Instantiate(toast, transform);
        Text toastText = toastInstance.GetComponentInChildren<Text>();
        GroupFader toastFader = toastInstance.GetComponentInChildren<GroupFader>();

        // Configure Toast
        toastText.text = text;
        toastText.color = color;
        currentToast = toastInstance;

        yield return new WaitForSeconds(0.16f);
        
        toastFader.FadeIn();
        displaying = true;

        yield return new WaitForSeconds(toastFader.fadeTime + lifetime);

        toastFader.FadeOut();

        yield return new WaitForSeconds(toastFader.fadeTime);

        Destroy(toastInstance);
        displaying = false;
    }

    #region Utils

    void DestroyCurrentToast() { if (currentToast != null) { Destroy(currentToast.gameObject); } displaying = false; }

    #endregion
}
