using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour {

    public QuestObject[] quest;
    public bool[] questCompleted;

    public DialogueManager theDM;

    public string itemCollected;

    public string enemysKilled;

 

    	// Use this for initialization
	void Start () {
		
        questCompleted = new bool[quest.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string questText)
    {
        theDM.dialogueLines = new string[1];
        theDM.dialogueLines[0] = questText;

        theDM.currentLine = 0;
        theDM.ShowDialogue();
    }
}
