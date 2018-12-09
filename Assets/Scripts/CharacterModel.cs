using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GENDER {
    MALE,
    FEMALE
}

public class CharacterModel : MonoBehaviour {
    public GENDER gender;

    public AnimationSet[] animSet;
    public bool UI;

    // Use this for initialization
    public void ChangeGender()
    {
        for (int i = 0; i < animSet[(int)gender].idles.Length; i++)
        {
            //if(UI)
                //GetComponent<TinkerAnimator1>().animations[i] = animSet[(int)gender].idles[i];
        }
        for (int i = 0; i < animSet[(int)gender].runs.Length; i++)
        {
            //if (UI)
                //GetComponent<TinkerAnimator1>().animations[i] = animSet[(int)gender].runs[i];
        }
    }

    public void Male() {
        gender = GENDER.MALE;
        ChangeGender();
    }

    public void Female()
    {
        gender = GENDER.FEMALE;
        ChangeGender();
    }

    private void Start()
    {
        ChangeGender();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
