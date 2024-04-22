using DA_Assets.FCU.Extensions;
using DA_Assets.FCU.Model;
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

#if DABUTTON_EXISTS
using DA_Assets.DAB;
#endif

namespace DA_Assets.FCU.Drawers.CanvasDrawers
{
    [Serializable]
    public class ButtonDrawer : MonoBehaviourBinder<FigmaConverterUnity>
    {
        [SerializeField] List<SyncData> buttons;
        public List<SyncData> Buttons => buttons;

        public void Init()
        {
            buttons = new List<SyncData>();
        }

        public void Draw(FObject fobject)
        {
            bool hasCustomButtonBackgrounds = false;

            foreach (int cindex in fobject.Data.ChildIndexes)
            {
                monoBeh.CurrentProject.TryGetByIndex(cindex, out FObject child);

                if (child.ContainsAnyTag(
                    FcuTag.BtnDefault,
                    FcuTag.BtnDisabled,
                    FcuTag.BtnHover,
                    FcuTag.BtnPressed,
                    FcuTag.BtnSelected))
                {
                    hasCustomButtonBackgrounds = true;
                    break;
                }
            }

#if DABUTTON_EXISTS
            if (monoBeh.UsingDaButton() || hasCustomButtonBackgrounds)
            {
                fobject.Data.ButtonComponent = ButtonComponent.DAButton;
                fobject.Data.GameObject.TryAddComponent(out DAButton _);
            }
            else
#endif
            {
                fobject.Data.ButtonComponent = ButtonComponent.UnityButton;
                fobject.Data.GameObject.TryAddComponent(out UnityEngine.UI.Button _);
            }

            buttons.Add(fobject.Data);
        }

        public IEnumerator SetTargetGraphics()
        {
            foreach (SyncData syncData in buttons)
            {
                switch (syncData.ButtonComponent)
                {
                    case ButtonComponent.UnityButton:
                        SetupUnityButton(syncData);
                        break;
#if DABUTTON_EXISTS
                    case ButtonComponent.DAButton:
                        SetupDaButton(syncData);
                        break;
#endif
                }

                yield return null;
            }
        }

        private bool TryGetNumberBeforeDash(string input, out int number)
        {
            string pattern = @"\d+(?=\s*-)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                number = int.Parse(match.Value);
                return true;
            }
            else
            {
                number = 0;
                return false;
            }
        }
#if DABUTTON_EXISTS
        private void SetupDaButton(SyncData btnSyncData)
        {
            DAButton daButton = btnSyncData.GameObject.GetComponent<DAButton>();
            daButton.BlendMode = monoBeh.Settings.DabSettings.BlendMode;
            daButton.BlendIntensity = monoBeh.Settings.DabSettings.BlendIntensity;

            SyncHelper[] syncHelpers = btnSyncData.GameObject.GetComponentsInChildren<SyncHelper>(true).Skip(1).ToArray();
            Dictionary<int, List<SyncHelper>> graphics = new Dictionary<int, List<SyncHelper>>();

            foreach (SyncHelper syncHelper in syncHelpers)
            {
                bool exists = syncHelper.TryGetComponent(out Graphic gr);

                if (!exists)
                    continue;

                bool success = TryGetNumberBeforeDash(syncHelper.Data.NewName, out int number);

                if (success)
                {
                    if (!graphics.ContainsKey(number))
                    {
                        graphics[number] = new List<SyncHelper>();
                    }

                    graphics[number].Add(syncHelper);
                }
            }

            daButton.TargetGraphics.Clear();

            foreach (var item in graphics)
            {
                DATargetGraphic tg = DAButtonDefaults.Instance.CopyTargetGraphic(monoBeh.Settings.DabSettings.DefaultTargetGraphic);

                foreach (SyncHelper sh in item.Value)
                {
                    sh.TryGetComponent(out Graphic gr);

                    bool sprite = sh.gameObject.TryGetComponent(out Image img) && img.sprite != null;

                    if (sh.Data.Tags.Contains(FcuTag.BtnDefault))
                    {
                        tg.TargetGraphic = gr;
                        DaAnimationItem n = tg.NormalState;

                        if (sprite)
                        {
                            TProp<Sprite> sp = n.Sprite;
                            sp.Value = img.sprite;
                            n.Sprite = sp;
                        }
                        else
                        {
                            TProp<Color> sp = n.Color;
                            if (daButton.BlendMode == DAG.DAColorBlendMode.Overlay)
                            {
                                sp.Value = gr.color;
                            }
                            else
                            {
                                sp.Value = Color.white;
                            }
                            n.Color = sp;
                        }

                        tg.NormalState = n;
                    }
                    else
                    {
                        if (sh.Data.Tags.Contains(FcuTag.BtnDisabled))
                        {
                            UpdateAnimationColor(DAPointerEvent.OnDisable);
                        }
                        else if (sh.Data.Tags.Contains(FcuTag.BtnHover))
                        {
                            UpdateAnimationColor(DAPointerEvent.PointerEnter);
                        }
                        else if (sh.Data.Tags.Contains(FcuTag.BtnPressed))
                        {
                            UpdateAnimationColor(DAPointerEvent.PointerClick);
                        }
                        else if (sh.Data.Tags.Contains(FcuTag.BtnLooped))
                        {
                            UpdateAnimationColor(DAPointerEvent.OnLoopStart);
                        }
                        else if (sh.Data.Tags.Contains(FcuTag.BtnSelected))
                        {
                            //TODO: add BtnSelected
                        }

                        void UpdateAnimationColor(DAPointerEvent pointerEvent)
                        {
                            for (int i = 0; i < tg.Animations.Count; i++)
                            {
                                if (tg.Animations[i].Event != pointerEvent)
                                    continue;

                                DAButtonAnimation anim = tg.Animations[i];

                                for (int j = 0; j < anim.AnimationItems.Count; j++)
                                {
                                    DaAnimationItem tempItem = anim.AnimationItems[j];

                                    if (sprite)
                                    {
                                        TProp<Sprite> sp = tempItem.Sprite;
                                        sp.Value = img.sprite;
                                        tempItem.Sprite = sp;
                                    }
                                    else
                                    {
                                        TProp<Color> sp = tempItem.Color;
                                        sp.Value = gr.color;
                                        tempItem.Color = sp;
                                    }

                                    anim.AnimationItems[j] = tempItem;
                                }

                                tg.Animations[i] = anim;
                            }
                        }

                        gr.gameObject.Destroy();
                    }
                }

                daButton.TargetGraphics.Add(tg);
            }
        }
#endif
        private void SetupUnityButton(SyncData btnSyncData)
        {
            SyncHelper[] syncHelpers = btnSyncData.GameObject.GetComponentsInChildren<SyncHelper>(true).Skip(1).ToArray();

            Button btn = btnSyncData.GameObject.GetComponent<Button>();

            SetButtonTargetGraphic();

            void SetButtonTargetGraphic()
            {
                Graphic gr1 = null;
                bool exists = !syncHelpers.IsEmpty() && syncHelpers.First().TryGetComponent(out gr1);

                //If the first element of the hierarchy can be used as a target graphic.
                if (exists)
                {
                    btn.targetGraphic = gr1;
                }
                else
                {
                    //If there is at least some image, assign it to the targetGraphic.
                    foreach (SyncHelper meta in syncHelpers)
                    {
                        if (meta.TryGetComponent(out Image gr2))
                        {
                            btn.targetGraphic = gr2;
                            return;
                        }
                    }

                    //If there is at least some graphic, assign it to the targetGraphic.
                    foreach (SyncHelper meta in syncHelpers)
                    {
                        if (meta.TryGetComponent(out Graphic gr3))
                        {
                            btn.targetGraphic = gr3;
                            return;
                        }
                    }

                    //If there is a graphic on the button itself, assign it to the targetGraphic.
                    if (btn.TryGetComponent(out Graphic gr4))
                    {
                        btn.targetGraphic = gr4;
                    }
                }
            }
        }
    }
}
