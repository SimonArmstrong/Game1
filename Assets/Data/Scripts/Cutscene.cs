using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SceneShot {
    public GameObject actor;
    public List<GameObject> props = new List<GameObject>();

    public List<Transform> positions = new List<Transform>();
    public List<float> transitionTimes = new List<float>();
    public List<float> holdTimes = new List<float>();
    public List<bool> doesHold = new List<bool>();
    public List<Color> screenColors = new List<Color>();
    public List<Dialogue> dialogues = new List<Dialogue>();
}

public enum CutsceneType {
    Secret,
    Notification
}

public class Cutscene : MonoBehaviour {
    public SceneShot cam;
    public List<SceneShot> actors = new List<SceneShot>();
    public int currentShot = 0;
    public float timer;
    public bool timerBegun;

    public bool fadeIn;
    public bool fadeOut;

    public Item reward;

    private Vector3 target;

    public AudioMixerSnapshot snapshot;
    public List<AudioMixerGroup> affectedChannels = new List<AudioMixerGroup>();

    private GameStates prev_GameState;

    private void Start()
    {
        prev_GameState = GameManager.instance.state;
    }

    private void Update()
    {
        if (currentShot >= cam.positions.Count)
        {
            if (reward != null) Instantiate(GameManager.instance.genericItemDropObject, transform.position, Quaternion.identity);
            for (int i = 0; i < actors.Count; i++) {
                Destroy(actors[i].actor);
            }
            if (fadeOut)
                CutsceneManager.instance.FadeToBlack();

            Destroy(gameObject);
            currentShot = 0;
        }

        GameManager.instance.ChangeState(GameStates.cutscene);

        if (fadeIn)
        {
            ScreenFade.instance.color = cam.screenColors[currentShot];
        }
            //CutsceneManager.instance.FadeFromBlack();

        target = cam.positions[currentShot].position;
        target.z = -10;
        cam.actor.transform.localPosition = Vector3.Lerp(
            cam.actor.transform.localPosition, target,
            Time.unscaledDeltaTime * cam.transitionTimes[currentShot]);

        if (!timerBegun) {
            timerBegun = true;
            timer = cam.holdTimes[currentShot];
        }

        if (timerBegun)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0) {
                currentShot++;
                timerBegun = false;
            }
        }

        for (int i = 0; i < affectedChannels.Count; i++) {
        }
    }

    private void OnDestroy()
    {
        ScreenFade.instance.color = new Color(0, 0, 0, 0);
        GameManager.instance.ChangeState(prev_GameState);
    }
}
