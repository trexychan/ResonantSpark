﻿using System;
using System.Collections.Generic;
using UnityEngine;

using ResonantSpark.Character;
using ResonantSpark.Gameplay;
using ResonantSpark.Service;
using ResonantSpark.Utility;
using ResonantSpark.Input;

namespace ResonantSpark {
    namespace CharacterProperties {
        public class Attack : ScriptableObject, IInGamePerformable {
            private Action<Builder.IAttackCallbackObj> builderCallback;

            public new string name { get; private set; }
            public Orientation orientation { get; private set; }
            public GroundRelation groundRelation { get; private set; }
            public InputNotation input { get; private set; }
            public string animStateName { get; private set; }
            public List<FrameState> frames { get; private set; }
            public List<HitBox> hitBoxes { get; private set; }

            private IFightingGameService fgService;
            private IProjectileService projectServ;
            private IAudioService audioServ;

            private FightingGameCharacter fgChar;
            private Action onCompleteCallback;

            private AttackTracker tracker;

            public Attack(Action<Builder.IAttackCallbackObj> builderCallback) {
                this.builderCallback = builderCallback;
            }

            public void BuildAttack(AllServices services) {
                fgService = services.GetService<IFightingGameService>();
                projectServ = services.GetService<IProjectileService>();
                audioServ = services.GetService<IAudioService>();

                fgChar = services.GetService<IBuildService>().GetBuildingFGChar();

                AttackBuilder attackBuilder = new AttackBuilder(services);
                builderCallback(attackBuilder);

                attackBuilder.BuildAttack();

                name = attackBuilder.name;
                orientation = attackBuilder.orientation;
                groundRelation = attackBuilder.groundRelation;
                input = attackBuilder.input;
                animStateName = attackBuilder.animStateName;

                frames = attackBuilder.GetFrames();
                hitBoxes = attackBuilder.GetHitBoxes();

                tracker = new AttackTracker(frames.Count);
            }

            public void FrameCountSanityCheck(int frameIndex) {
                throw new NotImplementedException();
            }

            public bool IsCompleteRun() {
                throw new NotImplementedException();
            }

            public void SetOnCompleteCallback(Action onCompleteCallback) {
                this.onCompleteCallback = onCompleteCallback;
            }

            public void StartPerformable(int frameIndex) {
                tracker.Track(frameIndex);
                fgService.RunAnimationState(fgChar, animStateName);
            }

            public void RunFrame() {
                int frameCount = tracker.GetFrameCount();
                frames[frameCount].Perform();
            }
        }
    }
}