using Game3D.Managers;

namespace Game3D.Drivers {

	public abstract class ExternalDataDriver {

        public abstract void Save(SaveableSceneObjects saveableSceneObjects, string path);

        public abstract SaveableSceneObjects Load(string path);

    }
	
}