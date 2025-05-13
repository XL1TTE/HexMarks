using CMSystem;
using Project.Factories;
using Zenject;

namespace SaveData{
    public class RuntimeDataProvider{
        private CMSEntity m_CurrentLocationModel;   
        public void SetCurrentLocation(CMSEntity locModel) => m_CurrentLocationModel = locModel;
        public CMSEntity GetCurrentLocation() => m_CurrentLocationModel; 
    }
}
