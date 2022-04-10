using BepInEx;
using System;
using UnityEngine;
using Utilla;
using UnityEngine.XR;

namespace SpaceTag
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode("CasInSpaceBeta", "SPACE CASUAL (BETA)", Utilla.Models.BaseGamemode.Casual)]
    [ModdedGamemode("TagInSpaceBeta", "SPACE INFECTION (BETA)", Utilla.Models.BaseGamemode.Infection)]
    [ModdedGamemode("HUNInSpaceBeta", "SPACE HUNT (BETA)", Utilla.Models.BaseGamemode.Hunt)]// make the lobby so u can play Tag in SPACE!!!
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        int Gametype;

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }

        void Update()
        {
            /* Code here runs every frame when the mod is enabled */
            if (inRoom)
            {
                print("SpaceGamemodeDebug: " + inRoom.ToString());
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, 360 * Time.deltaTime, 0), ForceMode.Acceleration);
                print(89 * Time.deltaTime);
                bool Groundpound;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out Groundpound);
                if (Groundpound)
                {
                    if (Gametype == 1) // infection
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        GorillaLocomotion.Player.Instance.transform.position = GorillaLocomotion.Player.Instance.transform.position + new Vector3(0, -13 * Time.deltaTime, 0);
                    }
                    if (Gametype == 2) // casual
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, 360 * Time.deltaTime, 0), ForceMode.Acceleration);
                    }
                    if (Gametype == 3) // hunt
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(GorillaLocomotion.Player.Instance.headCollider.transform.forward.x * 360 * Time.deltaTime, -0.7f, GorillaLocomotion.Player.Instance.headCollider.transform.forward.z * 360 * Time.deltaTime);
                    }
                }
            }
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            print(gamemode);
            if (gamemode == "forestDEFAULTMODDED_TagInSpaceBetaINFECTION")
            {
                Gametype = 1;
                inRoom = true;
            }
            if (gamemode == "canyonDEFAULTMODDED_TagInSpaceBetaINFECTION")
            {
                Gametype = 1;
                inRoom = true;
            }
            if (gamemode == "caveDEFAULTMODDED_TagInSpaceBetaINFECTION")
            {
                Gametype = 1;
                inRoom = true;
            }
            // casual
            if (gamemode == "forestDEFAULTMODDED_CasInSpaceBetaCASUAL")
            {
                Gametype = 2;
                inRoom = true;
            }
            if (gamemode == "canyonDEFAULTMODDED_CasInSpaceBetaCASUAL")
            {
                Gametype = 2;
                inRoom = true;
            }
            if (gamemode == "caveDEFAULTMODDED_CasInSpaceBetaCASUAL")
            {
                Gametype = 2;
                inRoom = true;
            }
            // hunt
            if (gamemode == "forestDEFAULTMODDED_HUNInSpaceBetaHUNT")
            {
                Gametype = 3;
                inRoom = true;
            }
            if (gamemode == "canyonDEFAULTMODDED_HUNInSpaceBetaHUNT")
            {
                Gametype = 3;
                inRoom = true;
            }
            if (gamemode == "caveDEFAULTMODDED_HUNInSpaceBetaHUNT")
            {
                Gametype = 3;
                inRoom = true;
            }
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            inRoom = false;
        }
    }
}
