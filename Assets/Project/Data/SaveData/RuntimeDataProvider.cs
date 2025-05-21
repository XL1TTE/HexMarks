using CMSystem;

namespace GameData{
    
    /* ################################################## */
    /*             Tracks all global game data            */
    /* ################################################## */
    public class GameDataTracker{
        private CMSEntity m_CurrentLocationModel;   
        public void SetCurrentLocation(CMSEntity locModel) => m_CurrentLocationModel = locModel;
        public CMSEntity GetCurrentLocation() => m_CurrentLocationModel; 
        
        
        
    }
}
