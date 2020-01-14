﻿using UnityEngine;
using UnityEngine.UI;

using ResonantSpark.Builder;
using ResonantSpark.Gameplay;
using ResonantSpark.Gamemode;
using ResonantSpark.Input;

public class TestBuilder : MonoBehaviour {
    public OneOnOneRoundBased gameManager;
    public GameObject male0Builder;

    public Text charVelocity;
    public InputBuffer inputBuffer;

    public void Start() {
        ICharacterBuilder builder = male0Builder.GetComponent<ICharacterBuilder>();
        GameObject char0 = builder.CreateCharacter();

        gameManager.char0 = char0;

        // Temporary code
        char0.GetComponent<FightingGameCharacter>().charVelocity = charVelocity;
        char0.GetComponent<FightingGameCharacter>().SetInputBuffer(inputBuffer);

        Debug.Log("Trigger Fighting Game Chars Ready");

        EventManager.TriggerEvent<ResonantSpark.Events.FightingGameCharsReady>();
    }
}
