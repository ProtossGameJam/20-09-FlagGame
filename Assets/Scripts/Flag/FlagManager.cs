﻿using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum FlagColor {
    RED, ORANGE, YELLOW, GREEN, BLUE, NAVY, PURPLE, WHITE, BLACK
}

[Serializable]
public class Flag {
    public FlagColor color;
    public bool isTaken;

    public Flag(FlagColor color) {
        this.color = color;
        isTaken = false;
    }

    public static implicit operator Flag(FlagColor flag) {
        return new Flag(flag);
    }
}

public class FlagManager : MonoBehaviourPun {
    [SerializeField] private UIFlagDisplay uiFlagDisplay;

    [ReadOnly, SerializeField] private List<Flag> flagList;
    [SerializeField] private int flagStoreSize;

    public bool IsAllFlagCollected => !flagList.Exists(flag => flag.isTaken == false);

    private void Awake() {
        flagList = new List<Flag>(flagStoreSize);
    }

    private void Start() {
        RandomFlagPick(flagStoreSize);
    }

    private void RandomFlagPick(int count) {
        var tempColorArr = ShuffleUtility<FlagColor>.GetShuffledArray((FlagColor[]) Enum.GetValues(typeof(FlagColor)));
        Array.Resize(ref tempColorArr, count);

        for (var i = 0; i < count; i++) flagList.Add(tempColorArr[i]);
        uiFlagDisplay.FlagUIInitialize(tempColorArr);
    }

    public void GetFlag(FlagColor color) {
        var tempFlag = flagList.Find(flag => flag.color == color);
        if (tempFlag != null) {
            tempFlag.isTaken = true;
            uiFlagDisplay.ShowFlagUI(color);
            //photonView.RPC("GetFlagAnnounce", RpcTarget.Others, PhotonNetwork.NickName, color);
        }
    }

    [PunRPC]
    public void GetFlagAnnounce(string nick, FlagColor color) {
        print($"[DEBUG] RPC : GetFlagAnnounce() - Nickname : {nick}, Color : {color.ToString()}");
    }
}