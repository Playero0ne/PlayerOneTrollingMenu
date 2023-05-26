using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using HarmonyLib;
using Player1;
using Player1.Main;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Object = UnityEngine.Object;
using ModMenuPatch.HarmonyPatches;
using GorillaNetworking;
using BepInEx;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine.InputSystem;
using Player1.Main.Helpers;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;
using Player1.Components;

namespace Player1.Main
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", 0)]
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
    [HarmonyPatch("FixedUpdate", 0)]
    //[HarmonyPatch(typeof(GorillaLocomotion.Player))]
    //[HarmonyPatch("Update", 0)]
    internal class MenuPatch
    {
        static GorillaTagManager tagManager;
        static float batlvl;
        public static TagAllBecauseihaveto tagall = new TagAllBecauseihaveto();
        public static Vector3 MenuPos = GorillaLocomotion.Player.Instance.leftHandTransform.position;
        public static Quaternion MenuRot = GorillaLocomotion.Player.Instance.leftHandTransform.rotation;
        public static void Prefix(GorillaLocomotion.Player __instance)
        {
            try
            {
                /*List<UnityEngine.XR.InputDevice> list2 = new List<UnityEngine.XR.InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list2);
                list2[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.batteryLevel, out batlvl);
                int FramesPerSecond = (int)(1f / Time.deltaTime);*/
                
                if (once)
                {
                    Debug.Log("menu spawing");
                    PageNum = 0;
                    CheckPages = true;
                    Anti_Ban.StartAntiBan();
                    Debug.Log("menu Spawned");
                    once = false;
                }
                if (UnityInput.Current.GetKey(KeyCode.Y))
                {
                    PageNum = 0;
                    CheckPages = true;
                }
                if (UnityInput.Current.GetKey(KeyCode.U))
                {
                    PageNum = 1;
                    CheckPages = true;
                }
                if (UnityInput.Current.GetKey(KeyCode.I))
                {
                    PageNum = 2;
                    CheckPages = true;
                }
                if (UnityInput.Current.GetKey(KeyCode.O))
                {
                    PageNum = 3;
                    CheckPages = true;
                }
                if (CheckPages == true)
                {
                    if (MenuPatch.PageNum == 0)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.menu);
                        MenuPatch.menu = null;
                        PageManager.AddPage(0, "Leave", "Join Lobby", "Fly", "Speed Boost [D]", "Platforms", "ESP");
                        Draw();
                        Debug.Log("Page Num: "+PageNum.ToString());
                        CheckPages = false;
                    }
                    if (MenuPatch.PageNum == 1)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.menu);
                        MenuPatch.menu = null;
                        PageManager.AddPage(1, "Tag Gun", "Tag All", "Ghost Monkey", "Invis Monkey", "SlingShot [Fly]", "Give SlingShot");
                        Draw();
                        Debug.Log("Page Num: " + PageNum.ToString());
                        CheckPages = false;
                    }
                    if (MenuPatch.PageNum == 2)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.menu);
                        MenuPatch.menu = null;
                        PageManager.AddPage(2, "No Clip", "Right Hand", "Kick Gun [Stump]", "Kick All [Stump]", "Slow All", "Vibrate All");
                        Draw();
                        Debug.Log("Page Num: " + PageNum.ToString());
                        CheckPages = false;
                    }
                    if (MenuPatch.PageNum == 3)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.menu);
                        MenuPatch.menu = null;
                        PageManager.AddPage(3, "Random Mat [TESTING]", "Hunt Fucker [TESTING]", "Break Gamemode [TESTING]", "Rope Up", "Break Mod Checker", "Made By PLAYERONE");
                        Draw();
                        Debug.Log("Page Num: " + PageNum.ToString());
                        CheckPages = false;
                    }
                }
                bool flag = MenuPatch.maxJumpSpeed == null;
                if (flag)
                {
                    MenuPatch.maxJumpSpeed = new float?(GorillaLocomotion.Player.Instance.maxJumpSpeed);
                }
                List<UnityEngine.XR.InputDevice> list = new List<UnityEngine.XR.InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
                list[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out MenuPatch.gripDown);
                bool flag2 = MenuPatch.gripDown && MenuPatch.menu == null;
                if (flag2)
                {
                    CheckPages = true;
                    MenuPatch.Draw();
                    bool flag3 = MenuPatch.referance == null;
                    if (flag3)
                    {
                        MenuPatch.referance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Object.Destroy(MenuPatch.referance.GetComponent<MeshRenderer>());
                        MenuPatch.referance.transform.parent = GorillaLocomotion.Player.Instance.rightHandTransform;
                        MenuPatch.referance.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                        MenuPatch.referance.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    }
                }
                else
                {
                    bool flag4 = !MenuPatch.gripDown && MenuPatch.menu != null;
                    if (flag4)
                    {
                        Object.Destroy(MenuPatch.menu);
                        MenuPatch.menu = null;
                        Object.Destroy(MenuPatch.referance);
                        MenuPatch.referance = null;
                    }
                }
                bool flag5 = MenuPatch.gripDown && MenuPatch.menu != null;
                if (flag5)
                {
                    MenuPatch.menu.transform.position = MenuPos;
                    MenuPatch.menu.transform.rotation = MenuRot;
                }
                if (PageNum == 0)
                {
                    if (buttonsActive[0] == true)
                    {
                        PhotonNetwork.Disconnect();
                    }
                    if (buttonsActive[1] == true)
                    {
                        GorillaNetworkJoinTrigger trigger = new GorillaNetworkJoinTrigger();
                        PhotonNetwork.JoinRandomRoom();
                    }
                    if (buttonsActive[2] == true)
                    {
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out flying);
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out gravtog);
                        if (flying)
                        {
                            __instance.GetComponent<Rigidbody>().velocity += __instance.bodyCollider.transform.forward * 10f;
                        }
                        if (gravtog)
                        {
                            gravityToggled = !gravityToggled;
                        }
                        if (gravityToggled)
                        {
                            __instance.GetComponent<Rigidbody>().useGravity = false;
                        }
                    }
                    if (buttonsActive[3] == true)
                    {
                        __instance.maxJumpSpeed = 999;
                        __instance.jumpMultiplier = 100;
                    }
                    else
                    {
                        __instance.maxJumpSpeed = 1;
                        __instance.jumpMultiplier = 100;
                    }
                    if (buttonsActive[4] == true)
                    {
                        ProcessPlatformMonke();
                    }
                    if (buttonsActive[5] == true)
                    {
                        foreach (VRRig vrrig in (VRRig[])UnityEngine.Object.FindObjectsOfType(typeof(VRRig)))
                        {
                            if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer && !vrrig.photonView.IsMine && vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                                vrrig.mainSkin.material.color = new Color(9f, 0f, 0f);
                            }
                            else if (!vrrig.photonView.IsMine)
                            {
                                vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                                vrrig.mainSkin.material.color = Color.blue;
                            }
                        }
                    }
                    if (buttonsActive[6] == true)
                    {
                        PageManager.ChangePage(3);
                    }
                    if (buttonsActive[7] == true)
                    {
                        PageManager.ChangePage(1);
                    }
                }
                else if (PageNum == 1)
                {
                    if (buttonsActive[0] == true)
                    {
                        TagGun();
                    }
                    if (buttonsActive[1] == true)
                    {
                        tagall.startstuff();
                    }
                    if (buttonsActive[2] == true)
                    {
                        bool bool1 = false;
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out bool1);
                        if (bool1)
                        {
                            ghostToggled = !ghostToggled;
                        }
                        if (ghostToggled)
                        {
                            GorillaTagger.Instance.myVRRig.enabled = false;
                        }
                        else
                        {
                            GorillaTagger.Instance.myVRRig.enabled = true;
                        }
                    }
                    if (buttonsActive[3] == true)
                    {
                        bool bool1 = false;
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out bool1);
                        if (bool1)
                        {
                            ghostToggled = !ghostToggled;
                        }
                        if (ghostToggled)
                        {
                            GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = Vector3.zero;
                        }
                        else
                        {
                            GorillaTagger.Instance.myVRRig.enabled = true;
                        }
                    }
                    if (buttonsActive[4]==true)
                    {
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out flying);
                        if (flying)
                        {
                            __instance.GetComponent<Rigidbody>().AddForce(GorillaLocomotion.Player.Instance.headCollider.transform.forward * 70f, ForceMode.Acceleration);
                        }
                    }
                    if (buttonsActive[5] == true)
                    {
                        if (PhotonNetwork.InRoom)
                        {
                            VRRig offlineVRRig = GorillaTagger.Instance.offlineVRRig;
                            if (offlineVRRig != null && !Slingshot.IsSlingShotEnabled())
                            {
                                CosmeticsController instance = CosmeticsController.instance;
                                CosmeticsController.CosmeticItem itemFromDict = instance.GetItemFromDict("Slingshot");
                                instance.ApplyCosmeticItemToSet(offlineVRRig.cosmeticSet, itemFromDict, true, false);
                            }
                            PageManager.ResetPage(5);
                        }

                    }
                    if (buttonsActive[6] == true)
                    {
                        PageManager.ChangePage(0);
                    }
                    if (buttonsActive[7] == true)
                    {
                        PageManager.ChangePage(2);
                    }
                }
                else if (PageNum == 2)
                {
                    if (buttonsActive[0] == true)
                    {
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out Noclip);
                        if (Noclip)
                        {
                            MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
                            foreach (MeshCollider meshCollider in array)
                            {
                                meshCollider.transform.localScale = meshCollider.transform.localScale / 10000f;
                            }
                        }
                        else
                        {
                            MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
                            foreach (MeshCollider meshCollider in array)
                            {
                                meshCollider.transform.localScale = meshCollider.transform.localScale / 10000f;
                            }
                        }
                    }
                    if (buttonsActive[1] == true)
                    {
                        MenuPos = GorillaLocomotion.Player.Instance.rightHandTransform.position;
                        MenuRot = GorillaLocomotion.Player.Instance.rightHandTransform.rotation;
                    }
                    if (buttonsActive[2] == true)
                    {
                        KickGun();
                    }
                    if (buttonsActive[3] == true)
                    {
                        foreach (Photon.Realtime.Player own in PhotonNetwork.PlayerList)
                        {
                            var x = GorillaGameManager.instance.FindVRRigForPlayer(own);
                            GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Add(x.Owner.UserId);
                            x.RPC("JoinPubWithFreinds", x.Owner, Array.Empty<object>());
                        }

                    }
                    if (buttonsActive[4] == true)
                    {
                        Anti_Ban.StartAntiBan();
                        GorillaGameManager instance2 = GorillaGameManager.instance;
                        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                        {
                            instance2.FindVRRigForPlayer(player).RPC("SetTaggedTime", RpcTarget.All, null);
                        }
                    }
                    if (buttonsActive[5] == true)
                    {
                        Anti_Ban.StartAntiBan();
                        GorillaGameManager instance3 = GorillaGameManager.instance;
                        foreach (Photon.Realtime.Player player2 in PhotonNetwork.PlayerList)
                        {
                            instance3.FindVRRigForPlayer(player2).RPC("SetJoinTaggedTime", RpcTarget.All, null);
                        }
                    }
                    if (buttonsActive[6] == true)
                    {
                        PageManager.ChangePage(1);
                    }
                    if (buttonsActive[7] == true)
                    {
                        PageManager.ChangePage(3);
                    }
                }
                else if (PageNum == 3)
                {
                    if (buttonsActive[0]==true)
                    {
                        foreach (GorillaTagManager tag in UnityEngine.Object.FindObjectsOfType<GorillaTagManager>())
                        {
                            tag.photonView.RequestOwnership();
                            Anti_Ban.setOwnership(tag.photonView);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                            tag.currentInfected.Add(PhotonNetwork.LocalPlayer);
                            tag.UpdateTagState();

                        }
                    }
                    if (buttonsActive[1]==true)
                    {
                        foreach (GorillaHuntManager hunt in UnityEngine.Object.FindObjectsOfType<GorillaHuntManager>())
                        {
                            hunt.photonView.RequestOwnership();
                            Anti_Ban.setOwnership(hunt.photonView);
                            hunt.waitingToStartNextHuntGame = true;
                            hunt.countDownTime = 99999999;
                            hunt.timeHuntGameEnded = 9999999999999;
                        }
                    }
                    if (buttonsActive[2] == true)
                    {
                        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
                        {
                            foreach (GorillaTagManager tag in UnityEngine.Object.FindObjectsOfType<GorillaTagManager>())
                            {
                                tag.photonView.RequestOwnership();
                                Anti_Ban.setOwnership(tag.photonView);
                                tag.currentInfected.Remove(p);
                                tag.UpdateTagState();

                            }
                        }
                    }

                    if (buttonsActive[6] == true)
                    {
                        PageManager.ChangePage(0);
                    }
                    if (buttonsActive[7] == true)
                    {
                        PageManager.ChangePage(2);
                    }
                }
                else
                {
                    PageNum = 0;
                }
                /*if (MenuPatch.verified)
                {
                    if (Time.frameCount % 4000 == 0)
                    {
                        MenuPatch.verified = true;
                    }
                }*/

            }
            catch (Exception ex)
            {
                File.WriteAllText("KmanMenu-error.log", ex.ToString());
            }
        }
        #region Draw
        public static void AddButton(float offset, string text)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = MenuPatch.menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.35f - offset);
            gameObject.AddComponent<BtnCollider>().relatedText = text;
            int num = -1;
            for (int i = 0; i < MenuPatch.buttons.Length; i++)
            {

                bool flag = text == MenuPatch.buttons[i];
                if (flag)
                {
                    num = i;
                    break;
                }
            }
            Text text2 = new GameObject
            {
                transform =
                {
                    parent = MenuPatch.canvasObj.transform
                }
            }.AddComponent<Text>();
            text2.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text2.text = text;
            text2.fontSize = 1;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component = text2.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0f, 0.134f - offset / 2.55f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (buttonsActive[num] == true)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            text2.name = text;
            Debug.Log(text);
            gameObject.name = text;
        }
        public static void Draw()
        {
            MenuPatch.menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(MenuPatch.menu.GetComponent<Rigidbody>());
            Object.Destroy(MenuPatch.menu.GetComponent<BoxCollider>());
            Object.Destroy(MenuPatch.menu.GetComponent<Renderer>());
            menu.name = "Player1's Menu";
            MenuPatch.menu.transform.localScale = new Vector3(0.1f, 0.35f, 0.4f);
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(gameObject.GetComponent<Rigidbody>());
            Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = MenuPatch.menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 1f, 1.2f);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            gameObject.transform.position = new Vector3(0.05f, 0f, -0.025f);
            MenuPatch.canvasObj = new GameObject();
            MenuPatch.canvasObj.transform.parent = MenuPatch.menu.transform;
            Canvas canvas = MenuPatch.canvasObj.AddComponent<Canvas>();
            CanvasScaler canvasScaler = MenuPatch.canvasObj.AddComponent<CanvasScaler>();
            MenuPatch.canvasObj.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
            Text text = new GameObject
            {
                transform =
                {
                    parent = MenuPatch.canvasObj.transform
                }
            }.AddComponent<Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.text = "Player1 Trolling Menu";
            text.fontSize = 1;
            text.name = "Title";
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.18f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            for (int i = 0; i < MenuPatch.buttons.Length; i++)
            {
                MenuPatch.AddButton((float)i * 0.13f, MenuPatch.buttons[i]);
            }
            
        }
        public static void Toggle(string relatedText)
        {
            int num = -1;
            for (int i = 0; i < MenuPatch.buttons.Length; i++)
            {
                bool flag = relatedText == MenuPatch.buttons[i];
                if (flag)
                {
                    num = i;
                    break;
                }
            }
            bool flag2 = MenuPatch.buttonsActive[num] != null;
            if (flag2)
            {
                MenuPatch.buttonsActive[num] = !MenuPatch.buttonsActive[num];
                Object.Destroy(MenuPatch.menu);
                MenuPatch.menu = null;
                MenuPatch.Draw();
            }
        }

        public static int PageNum = 0;
        public static string[] buttons = new string[]
        {
            "NULL",
            "NULL",
            "NULL",
            "NULL",
            "NULL",
            "NULL",
            ">>>",
            "<<<"
        };
        public static bool?[] buttonsActive = new bool?[]
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
        };
#endregion
        #region Vars
        public static bool gripDown;
        public static GameObject menu = null;
        public static GameObject canvasObj = null;
        public static bool cangrapple = true;
        public static bool canleftgrapple = true;
        public static float RG;
        public static float LG;
        public static bool TGripHeld = false;
        public static bool Tagging = false;
        public static Vector3 CurHandPos;
        public static bool once = true;
        public static bool once2 = true;
        public static bool once3 = true;
        public static bool CheckPages = false;
        public static bool flytog = false;
        public static bool gravtog = false;
        public static bool ghostToggled = false;
        public static Color color = new Color(0f, 0f, 0f);
        public static int layers = 512;
        public static bool RGrabbing;
        public static bool LGrabbing;
        public static ConfigEntry<bool> InAir;
        public static bool AirMode;
        public static bool OnStart;
        public static GameObject referance = null;
        public static int framePressCooldown = 0;
        public static bool verified = false;
        public static GameObject pointer = null;
        public static bool gravityToggled = false;
        public static bool flying = false;
        public static int btnCooldown = 0;
        public static float? maxJumpSpeed = null;
        public static object index;
        public static Color coloresp;
        public static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);
        public static bool gripDown_left;
        public static bool gripDown_right;
        public static bool once_left;
        public static bool once_right;
        public static bool Noclip = false;
        public static bool once_left_false;
        public static bool once_right_false;
        public static bool once_networking;
        public static GameObject[] jump_left_network = new GameObject[9999];
        public static GameObject[] jump_right_network = new GameObject[9999];
        public static GameObject jump_left_local = null;
        public static GameObject jump_right_local = null;
        public static GradientColorKey[] colorKeys = new GradientColorKey[4];
#endregion
        #region Bomb
        public static GameObject C4 = null;
        public static bool BoomGrip;
        public static bool spawned = false;
        public static bool SpawnGrip;
        #endregion
        #region Helpers
        public static void KickGun()
        {
            RaycastHit raycastHit;
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandTransform.position - GorillaLocomotion.Player.Instance.rightHandTransform.up, -GorillaLocomotion.Player.Instance.rightHandTransform.up, out raycastHit);
            MenuPatch.pointer.transform.position = raycastHit.point;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out TGripHeld);
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out Tagging);

            if (TGripHeld)
            {
                if (!pointer.GetComponent<Renderer>())
                {
                    pointer.AddComponent<Renderer>();
                }
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Object.Destroy(pointer.GetComponent<Rigidbody>());
                pointer.GetComponent<Renderer>().material.color = Color.black;
                Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                if (Tagging)
                {
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        Anti_Ban.StartAntiBan();
                    }
                    PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
                    GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Add(componentInParent.Owner.UserId);
                    componentInParent.RPC("JoinPubWithFreinds", componentInParent.Owner, Array.Empty<object>());
                    return;
                }
                else
                {
                }
            }
            else
            {
                UnityEngine.Object.Destroy(pointer);
                pointer.GetComponent<Renderer>().enabled = false;
                pointer = null;
                return;
            }
        }
        public static void TagGun()
        {
            RaycastHit raycastHit;
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandTransform.position - GorillaLocomotion.Player.Instance.rightHandTransform.up, -GorillaLocomotion.Player.Instance.rightHandTransform.up, out raycastHit);
            MenuPatch.pointer.transform.position = raycastHit.point;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out TGripHeld);
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out Tagging);

            if (TGripHeld)
            {
                if (!pointer.GetComponent<Renderer>())
                {
                    pointer.AddComponent<Renderer>();
                }
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Object.Destroy(pointer.GetComponent<Rigidbody>());
                pointer.GetComponent<Renderer>().material.color = Color.black;
                Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                if (Tagging)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = raycastHit.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = MenuPatch.pointer.transform.position;
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        PhotonView.Get(GorillaGameManager.instance.GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", RpcTarget.MasterClient, new object[]
                        {
                    player
                        });
                    }
                    return;
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(pointer);
                pointer.GetComponent<Renderer>().enabled = false;
                pointer = null;
                return;
            }
        }
        public static void ProcessPlatformMonke()
        {
            bool flag = !MenuPatch.once_networking;
            if (flag)
            {
                PhotonNetwork.NetworkingClient.EventReceived += PlatformNetwork;
                MenuPatch.once_networking = true;
            }
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out MenuPatch.gripDown_left);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out MenuPatch.gripDown_right);
            bool flag2 = MenuPatch.gripDown_right;
            if (flag2)
            {
                bool flag3 = !MenuPatch.once_right;
                if (flag3)
                {
                    bool flag4 = MenuPatch.jump_right_local == null;
                    if (flag4)
                    {
                        MenuPatch.jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        MenuPatch.jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        MenuPatch.jump_right_local.transform.localScale = MenuPatch.scale;
                        MenuPatch.jump_right_local.transform.position = new Vector3(0f, -0.0075f, 0f) + GorillaLocomotion.Player.Instance.rightHandTransform.position;
                        MenuPatch.jump_right_local.transform.rotation = GorillaLocomotion.Player.Instance.rightHandTransform.rotation;
                        object[] eventContent = new object[]
                        {
                                new Vector3(0f, -0.0075f, 0f) + GorillaLocomotion.Player.Instance.rightHandTransform.position,
                            GorillaLocomotion.Player.Instance.rightHandTransform.rotation
                        };
                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                        {
                            Receivers = ReceiverGroup.Others
                        };
                        PhotonNetwork.RaiseEvent(70, eventContent, raiseEventOptions, SendOptions.SendReliable);
                        MenuPatch.once_right = true;
                        MenuPatch.once_right_false = false;
                    }
                }
            }
            else
            {
                bool flag5 = !MenuPatch.once_right_false;
                if (flag5)
                {
                    bool flag6 = MenuPatch.jump_right_local != null;
                    if (flag6)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.jump_right_local);
                        MenuPatch.jump_right_local = null;
                        MenuPatch.once_right = false;
                        MenuPatch.once_right_false = true;
                        RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
                        {
                            Receivers = ReceiverGroup.Others
                        };
                        PhotonNetwork.RaiseEvent(72, null, raiseEventOptions2, SendOptions.SendReliable);
                    }
                }
            }
            bool flag7 = MenuPatch.gripDown_left;
            if (flag7)
            {
                bool flag8 = !MenuPatch.once_left;
                if (flag8)
                {
                    bool flag9 = MenuPatch.jump_left_local == null;
                    if (flag9)
                    {
                        MenuPatch.jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        MenuPatch.jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        MenuPatch.jump_left_local.transform.localScale = MenuPatch.scale;
                        MenuPatch.jump_left_local.transform.position = GorillaLocomotion.Player.Instance.leftHandTransform.position;
                        MenuPatch.jump_left_local.transform.rotation = GorillaLocomotion.Player.Instance.leftHandTransform.rotation;
                        object[] eventContent2 = new object[]
                        {
                            GorillaLocomotion.Player.Instance.leftHandTransform.position,
                            GorillaLocomotion.Player.Instance.leftHandTransform.rotation
                        };
                        RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
                        {
                            Receivers = ReceiverGroup.Others
                        };
                        PhotonNetwork.RaiseEvent(69, eventContent2, raiseEventOptions3, SendOptions.SendReliable);
                        MenuPatch.once_left = true;
                        MenuPatch.once_left_false = false;
                    }
                }
            }
            else
            {
                bool flag10 = !MenuPatch.once_left_false;
                if (flag10)
                {
                    bool flag11 = MenuPatch.jump_left_local != null;
                    if (flag11)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.jump_left_local);
                        MenuPatch.jump_left_local = null;
                        MenuPatch.once_left = false;
                        MenuPatch.once_left_false = true;
                        RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
                        {
                            Receivers = ReceiverGroup.Others
                        };
                        PhotonNetwork.RaiseEvent(71, null, raiseEventOptions4, SendOptions.SendReliable);
                    }
                }
            }
            bool flag12 = !PhotonNetwork.InRoom;
            if (flag12)
            {
                for (int i = 0; i < MenuPatch.jump_right_network.Length; i++)
                {
                    UnityEngine.Object.Destroy(MenuPatch.jump_right_network[i]);
                }
                for (int j = 0; j < MenuPatch.jump_left_network.Length; j++)
                {
                    UnityEngine.Object.Destroy(MenuPatch.jump_left_network[j]);
                }
            }
        }
        private static void PlatformNetwork(EventData eventData)
        {
            byte code = eventData.Code;
            bool flag = code == 69;
            if (flag)
            {
                object[] array = (object[])eventData.CustomData;
                MenuPatch.jump_left_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                MenuPatch.jump_left_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                MenuPatch.jump_left_network[eventData.Sender].transform.localScale = MenuPatch.scale;
                MenuPatch.jump_left_network[eventData.Sender].transform.position = (Vector3)array[0];
                MenuPatch.jump_left_network[eventData.Sender].transform.rotation = (Quaternion)array[1];
            }
            else
            {
                bool flag2 = code == 70;
                if (flag2)
                {
                    object[] array2 = (object[])eventData.CustomData;
                    MenuPatch.jump_right_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    MenuPatch.jump_right_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    MenuPatch.jump_right_network[eventData.Sender].transform.localScale = MenuPatch.scale;
                    MenuPatch.jump_right_network[eventData.Sender].transform.position = (Vector3)array2[0];
                    MenuPatch.jump_right_network[eventData.Sender].transform.rotation = (Quaternion)array2[1];
                }
                else
                {
                    bool flag3 = code == 71;
                    if (flag3)
                    {
                        UnityEngine.Object.Destroy(MenuPatch.jump_left_network[eventData.Sender]);
                        MenuPatch.jump_left_network[eventData.Sender] = null;
                    }
                    else
                    {
                        bool flag4 = code == 72;
                        if (flag4)
                        {
                            UnityEngine.Object.Destroy(MenuPatch.jump_right_network[eventData.Sender]);
                            MenuPatch.jump_right_network[eventData.Sender] = null;
                        }
                    }
                }
            }
        }
        #endregion
        [BepInPlugin("org.ivy.gtag.gripmonke", "GripMonke", "2.1.1")]
        public class Plugin : BaseUnityPlugin
        {
            public void OnEnable()
            {
                MenuPatch.Plugin.harmony = new Harmony("com.ivy.gtag.gripmonke");
                MenuPatch.Plugin.harmony.PatchAll(Assembly.GetExecutingAssembly());
            }

            public static Harmony harmony;

            [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
            public class slidepatch
            {
                public static void Postfix(ref float __result)
                {
                    if (PageNum == 1)
                    {
                        if (MenuPatch.buttonsActive[2] == true)
                        {
                            __result = 0.03f;
                        }
                    }
                }
            }
        }
        [BepInPlugin("null", "null", "1.0.0")]
        public class thing : BaseUnityPlugin
        {
            public void Update()
            {
                GorillaNot.instance = this;
            }

            public static implicit operator GorillaNot(thing v)
            {
                return null;
            }
        }
        public class TimedBehaviour : MonoBehaviour
        {
            public virtual void Start()
            {
                this.startTime = Time.time;
            }
            public virtual void Update()
            {
                bool flag = this.complete;
                if (!flag)
                {
                    this.progress = Mathf.Clamp((Time.time - this.startTime) / this.duration, 0f, 1.5f);
                    bool flag2 = (double)Time.time - (double)this.startTime > (double)this.duration;
                    if (flag2)
                    {
                        bool flag3 = this.loop;
                        if (flag3)
                        {
                            this.OnLoop();
                        }
                        else
                        {
                            this.complete = true;
                        }
                    }
                }
            }
            public virtual void OnLoop()
            {
                this.startTime = Time.time;
            }
            public bool complete = false;
            public bool loop = true;
            public float progress = 0f;
            protected bool paused = false;
            protected float startTime;
            protected float duration = 2f;
        }
        public class ColorChanger : MenuPatch.TimedBehaviour
        {
            public override void Start()
            {
                base.Start();
                this.gameObjectRenderer = base.GetComponent<Renderer>();
            }
            public override void Update()
            {
                base.Update();
                bool flag = this.colors == null;
                if (!flag)
                {
                    bool flag2 = this.timeBased;
                    if (flag2)
                    {
                        this.color = this.colors.Evaluate(this.progress);
                    }
                    this.gameObjectRenderer.material.SetColor("_Color", this.color);
                    this.gameObjectRenderer.material.SetColor("_EmissionColor", this.color);
                }
            }
            public Renderer gameObjectRenderer;
            public Gradient colors = null;
            public Color color;
            public bool timeBased = true;
        }

    }
}
