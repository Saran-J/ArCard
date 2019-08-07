using UnityEngine;
using Vuforia;
using System.Collections;

public class MyPrefabInstantiator : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public ImageTargetBehaviour imgBehavior;
    public CameraController cameraController;
    public ChartController chartController;
    // Use this for initialization
    void Start()
    {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnTrackableStateChanged(
     TrackableBehaviour.Status previousStatus,
     TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }

        if (newStatus == TrackableBehaviour.Status.NO_POSE) {
            onTrackingLost();
        }
    }
    private void OnTrackingFound()
    {
        
        if (imgBehavior != null)
        {
            Debug.Log(imgBehavior.ImageTarget.Name);
            chartController.updateProtein(10);
            chartController.updateCarb(20);
            chartController.updateFat(30);
            

        }

    }

    private void onTrackingLost()
    {
        chartController.updateProtein(0);
        chartController.updateCarb(0);
        chartController.updateFat(0);
        //cameraController.onTrackingLost();
    }
}