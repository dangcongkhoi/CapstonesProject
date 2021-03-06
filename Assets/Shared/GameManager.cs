using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameManager 
{
    public event System.Action<PhotonPlayer> OnLocalPlayerJoined;
    private GameObject gameObject;
    
    private static GameManager m_Instance;


    
    public static GameManager Instance
    {

        get
        {

            if (m_Instance == null)
            {

                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<InputController>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respawner>();
            }
            return m_Instance;
        }
    }

    private InputController m_InputController;


    public InputController InputController
    {
        get
        {

            if (m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();

            }
            return m_InputController;
        }
    }

    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private Respawner m_Respawner;
    public Respawner Respawner
    {
        get
        {
            if (m_Respawner == null)
                m_Respawner = gameObject.GetComponent<Respawner>();
            return m_Respawner;
        }

    }

    private PhotonPlayer m_LocalPlayer;
    public PhotonPlayer LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value;
            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(m_LocalPlayer);
        }
    }



}
