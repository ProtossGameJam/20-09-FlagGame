﻿using System;
using System.Collections;
using UnityEngine;

public class MasterNPC : NPCBase {
    [SerializeField] private CameraAssigner cameraAssigner;
    [SerializeField] private Transform cameraTarget;

    private WaitForSeconds waitSecond;

    protected override void Awake() {
        //base.Awake();
        npcType = NPCType.Master;
        
        waitSecond = new WaitForSeconds(1.0f);
    }

    public void StartDialogue() {
        StartCoroutine(StartDialoguePrint());
    }

    private IEnumerator StartDialoguePrint() {
        var tempDialogue = (DialogueViewer) dialogueModule;
        cameraAssigner.AssignTargetToUI(cameraTarget);
        do {
            tempDialogue.Interact();
            yield return new WaitUntil(() => tempDialogue.textBubble.IsEndWriteLine);
            yield return waitSecond;
        } while (tempDialogue.textBubble.BubbleEnabled);
        dialogueModule.IsInteractable = false;
        cameraAssigner.AssignTargetToPlayer();
    }
}