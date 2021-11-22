using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

    private QuestManager theQM;

    public int questNumber;

    public bool startQuest;
    public bool endQuest;

	// Use this for initialization
	void Start () {
        theQM = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!theQM.questCompleted[questNumber])
            {
                if (startQuest && !theQM.quest[questNumber].gameObject.activeSelf)
                {
                    theQM.quest[questNumber].gameObject.SetActive(true);
                    theQM.quest[questNumber].StartQuest();
                }

                if (endQuest && theQM.quest[questNumber].gameObject.activeSelf)
                {
                    theQM.quest[questNumber].EndQuest();
                }
            }
        }
    }
}
