using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public interface ICameraController {
    void onTrackingLost();
}

public class CameraController : MonoBehaviour
{

    
    #region PRIVATE_MEMBERS

    private bool cameraInitialized;

   
    //private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.UNKNOWN_FORMAT;
    private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.UNKNOWN_FORMAT;
    private bool mAccessCameraImage = true;
    private bool mFormatRegistered = false;
    

    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS

    void Start()
    {

        
        StartCoroutine(InitializeCamera());

//#if UNITY_EDITOR
//        mPixelFormat = PIXEL_FORMAT.GRAYSCALE; // Need Grayscale for Editor
//#else
        mPixelFormat = PIXEL_FORMAT.GRAYSCALE; // Use RGB888 for mobile
//#endif

        // Register Vuforia life-cycle callbacks:
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPause);

    }

    #endregion // MONOBEHAVIOUR_METHODS

    #region PRIVATE_METHODS

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);
        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    void OnVuforiaStarted()
    {

        // Try register camera image format


        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());

            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError(
                "\nFailed to register pixel format: " + mPixelFormat.ToString() +
                "\nThe format may be unsupported by your device." +
                "\nConsider using a different pixel format.\n");

            mFormatRegistered = false;
        }

    }
    void OnTrackablesUpdated()
    {

        if (mFormatRegistered)
        {
            if (mAccessCameraImage)
            {
                

            }
        }
    }

    
    void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }

    void RegisterFormat()
    {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }

   
    void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }

    #endregion //PRIVATE_METHODS
}
