using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {
    public Cutscene currentCutscene;
    public Image white;

    bool beginFadeToWhite = false;
    bool beginFadeToBlack = false;
    bool beginFadeFromWhite = false;
    bool beginFadeFromBlack = false;

    #region Singleton
    public static CutsceneManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        //white = ScreenFade.instance.GetComponent<Image>();
    }

    public void OnSceneLoad() {

    }

    public void FadeToWhite() {
        white.color = new Color(1, 1, 1, 0);
        beginFadeToWhite = true;
    }
    public void FadeToBlack()
    {
        white.color = new Color(0, 0, 0, 0);
        beginFadeToBlack = true;
    }
    public void FadeFromWhite()
    {
        white.color = new Color(1, 1, 1, 1);
        beginFadeFromWhite = true;
    }
    public void FadeFromBlack()
    {
        white.color = new Color(0, 0, 0, 1);
        beginFadeFromBlack = true;
    }

    private void Update() {
        if (beginFadeFromBlack) {
            white.color = Color.Lerp(white.color, new Color(0, 0, 0, 0), Time.unscaledDeltaTime * 2);
        }
        if (beginFadeFromWhite) {
            white.color = Color.Lerp(white.color, new Color(1, 1, 1, 0), Time.unscaledDeltaTime * 2);
        }
        if (beginFadeToBlack)
        {
            white.color = Color.Lerp(white.color, new Color(0, 0, 0, 1), Time.unscaledDeltaTime * 2);
        }
        if (beginFadeToWhite)
        {
            white.color = Color.Lerp(white.color, new Color(1, 1, 1, 1), Time.unscaledDeltaTime * 2);
        }
    }
}
