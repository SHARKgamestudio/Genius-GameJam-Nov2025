using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Setup")]
    [Range(1, 10)]
    [SerializeField] float transition = 2.6f;

    [Header("References")]
    [SerializeField] HUD[] huds;

    #region Classes
    public enum Type
    {
        main,
        toast,
        inspect,
        dialogue,
        inventory
    }

    [Serializable]
    class HUD
    {
        public GroupFader panel;
        public Type type;
    }
    #endregion

    // Shared
    public static UIManager instance;

    void Awake() { instance = this; Show(Type.main); }

    void FixedUpdate()
    { for (int i = 0; i < huds.Length; i++) { huds[i].panel.fadeTime = transition; } }


    public void Show(Type type)
    {
        for (int i = 0; i < huds.Length; i++)
        {
            if (huds[i].type == type) { huds[i].panel.FadeIn(); }
        }
    }
    public void Hide(Type type)
    {
        for (int i = 0; i < huds.Length; i++)
        {
            if (huds[i].type == type) { huds[i].panel.FadeOut(); }
        }
    }
    public void ShowExclusive(Type type)
    {
        for (int i = 0; i < huds.Length; i++)
        {
            if (huds[i].type == type) { huds[i].panel.FadeIn(); }
            if (huds[i].type != type) { huds[i].panel.FadeOut(); }
        }
    }
}
