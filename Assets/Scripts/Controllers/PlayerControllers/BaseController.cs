using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers {

    public abstract class BaseController : MonoBehaviour
    {

        public bool Enabled { get; private set; }


        

        public virtual void Enable()
        {
            Enabled = true;
        }

        public virtual void Disable()
        {
            Enabled = false;
        }

    }

}
