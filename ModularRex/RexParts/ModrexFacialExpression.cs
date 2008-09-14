﻿using System.Collections.Generic;
using ModularRex.RexNetwork;
using Nini.Config;
using OpenSim.Framework;
using OpenSim.Region.Environment.Interfaces;
using OpenSim.Region.Environment.Scenes;



namespace ModularRex.RexParts
{
    class ModrexFacialExpression : IRegionModule
    {
        public void Initialise(Scene scene, IConfigSource source)
        {
            scene.EventManager.OnNewClient += EventManager_OnNewClient;
        }

        static void EventManager_OnNewClient(IClientAPI client)
        {
            // Check if the client was insubstantiated as a RexClientView.
            if (client is RexClientView)
            {
                // It was? Subscribed to our special events...
                RexClientView mcv = (RexClientView)client;

                mcv.OnRexFaceExpression += mcv_OnRexFaceExpression;
            }
        }

        static void mcv_OnRexFaceExpression(RexClientView sender, List<string> vParams)
        {
            // OpenSim BUG: IScene contains insufficient properties for handling agents.
            // FIXME Then return.
            Scene x = (Scene) sender.Scene;
            x.ForEachScenePresence(delegate(ScenePresence scenePresence)
                                       {
                                           if (scenePresence.ControllingClient is RexClientView)
                                               ((RexClientView) scenePresence.ControllingClient).
                                                   SendRexFaceExpression(vParams);
                                       }
                );

        }

        public void PostInitialise()
        {
            
        }

        public void Close()
        {
            
        }

        public string Name
        {
            get { return "RealXtendFacialModule"; }
        }

        public bool IsSharedModule
        {
            get { return true; }
        }
    }
}
