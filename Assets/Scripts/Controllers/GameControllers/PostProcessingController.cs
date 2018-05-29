using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


namespace Game3D {

	public class PostProcessingController : BaseObjectScene {
        
        private const float DOF_ENABLE_DISTANCE = 2f;
        private const int   DOF_MIN_FOCAL_DISTANCE = 80;
        private const int   DOF_MAX_FOCAL_DISTANCE = 200;


        public LayerMask DOFLayers;
        

        private PostProcessingProfile _postProcStack;
        
        private DOFInterpolateSettings _DOFFocusSettings = new DOFInterpolateSettings()
        {
            aperture = 26f,
            focalLength = 100,
            focusDistance = 0.5f,
            apertureSpeed = 5f,
            focalLengthSpeed = 5f,
            focusDistanceSpeed = 100f
        };
        private DOFInterpolateSettings _DOFNormalizeSettings = new DOFInterpolateSettings()
        {
            aperture = 0.1f,
            focalLength = 1,
            focusDistance = DOF_ENABLE_DISTANCE,
            apertureSpeed = 2f,
            focalLengthSpeed = 2f,
            focusDistanceSpeed = 100f
        };

        private Transform _cam;



        private void Start()
        {
            _cam = Camera.main.transform;
            _postProcStack = GetComponent<PostProcessingBehaviour>().profile;
        }



        private void Update()
        {
            ProcessDOF();
        }


        private void ProcessDOF()
        {
            RaycastHit hit;

            Debug.DrawRay(_cam.position, _cam.forward * DOF_ENABLE_DISTANCE, Color.red);
            bool isHit = Physics.Raycast(_cam.position, _cam.forward, out hit, DOF_ENABLE_DISTANCE, DOFLayers);

            if (isHit)
            {
                _DOFFocusSettings.focusDistance = hit.distance;
                _DOFFocusSettings.focalLength = Mathf.Clamp(Mathf.RoundToInt(hit.distance * 100), DOF_MIN_FOCAL_DISTANCE, DOF_MAX_FOCAL_DISTANCE);
                InterpolateDOFSettings(_postProcStack, _DOFFocusSettings, Time.deltaTime);
            }
            else
            {
                InterpolateDOFSettings(_postProcStack, _DOFNormalizeSettings, Time.deltaTime);
            }
            
        }



        public void InterpolateDOFSettings(PostProcessingProfile stack, DOFInterpolateSettings settings, float deltaTime = 1)
        {
            DepthOfFieldModel.Settings DOFNewSettings = stack.depthOfField.settings;

            DOFNewSettings.aperture = Mathf.Lerp(DOFNewSettings.aperture, settings.aperture, settings.apertureSpeed * deltaTime);
            DOFNewSettings.focalLength = Mathf.Lerp(DOFNewSettings.focalLength, settings.focalLength, settings.focalLengthSpeed * deltaTime);
            DOFNewSettings.focusDistance = Mathf.Lerp(DOFNewSettings.focusDistance, settings.focusDistance, settings.focusDistanceSpeed * deltaTime);

            stack.depthOfField.settings = DOFNewSettings;

        }


	}
	
}



public struct DOFInterpolateSettings
{
    public float aperture;
    public int focalLength;
    public float focusDistance;
    public float apertureSpeed;
    public float focalLengthSpeed;
    public float focusDistanceSpeed;
}