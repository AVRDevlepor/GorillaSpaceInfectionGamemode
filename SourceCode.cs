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
    [ModdedGamemode("TagInSpace", "SPACE INFECTION", Utilla.Models.BaseGamemode.Infection)] // make the lobby so u can play Tag in SPACE!!!
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

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
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, 360 * Time.deltaTime, 0), ForceMode.Acceleration);
                print(89 * Time.deltaTime);
                bool Groundpound;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out Groundpound);
                if (Groundpound)
                {
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    GorillaLocomotion.Player.Instance.transform.position = GorillaLocomotion.Player.Instance.transform.position + new Vector3(0, -13 * Time.deltaTime, 0);
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
            if (gamemode == "forestDEFAULTMODDED_TagInSpaceINFECTION")
            {
                inRoom = true;
            }
            if (gamemode == "canyonDEFAULTMODDED_TagInSpaceINFECTION")
            {
                inRoom = true;
            }
            if (gamemode == "caveDEFAULTMODDED_TagInSpaceINFECTION")
            {
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
