using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saturn_UI : MonoBehaviour
{
    public static Saturn_UI Instance;

    public Slider Energy;
    public Slider Angry;

    public GameObject EmotionPanel;
    public GameObject FindWaterCan;
    public GameObject NotFoundWaterCan;
    public GameObject FullWaterCan;
    public GameObject WaterCan;
    public GameObject Flowers;
    public GameObject Sleeping;

    private Saturn saturn;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        saturn = gameObject.GetComponent<Saturn>();
    }

    // Update is called once per frame
    void Update()
    {
        Energy.value = saturn.Energy / saturn.DefaultEnergy;
        Angry.value = saturn.Angry;
    }

    public void SelectEmotion(GameObject emotion)
    {
        for (int i = 0; i < EmotionPanel.transform.childCount; i++)
            EmotionPanel.transform.GetChild(i).gameObject.SetActive(false);

        emotion.SetActive(true);
    }
}
