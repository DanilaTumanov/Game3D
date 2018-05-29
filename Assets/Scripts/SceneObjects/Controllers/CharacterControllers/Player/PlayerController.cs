using Game3D.Controllers;
using Game3D.Managers;
using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Controllers
{

    public class PlayerController : BaseObjectScene
    {

        public static PlayerController Instance { get; private set; }

        public ObjectManager ObjectManager { get; private set; }

        public FlashLightController FlashLightController { get; private set; }
        public InputController InputController { get; private set; }
        public SelectionController SelectionController { get; private set; }
        public WeaponController WeaponController { get; private set; }
        public InteractionController InteractionController { get; private set; }
        public MasterSoundController MasterSoundController { get; private set; }



        void Start()
        {
            Instance = this;

            ObjectManager = gameObject.AddComponent<ObjectManager>();

            FlashLightController = gameObject.AddComponent<FlashLightController>();
            InputController = gameObject.AddComponent<InputController>();
            SelectionController = gameObject.AddComponent<SelectionController>();
            WeaponController = gameObject.AddComponent<WeaponController>();
            InteractionController = gameObject.AddComponent<InteractionController>();
            MasterSoundController = gameObject.AddComponent<MasterSoundController>();

            EnableDefaults();
        }


        private void EnableDefaults()
        {
            FlashLightController.Enable();
            WeaponController.Enable();
            SelectionController.Enable();
            InteractionController.Enable();
            MasterSoundController.Enable();
        }

    }

}