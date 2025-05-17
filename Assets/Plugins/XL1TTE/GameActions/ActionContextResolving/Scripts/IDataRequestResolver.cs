namespace XL1TTE.GameActions{
    public interface IDataRequestResolver{
        bool CanResolve(DataRequest req);
        object Resolve(DataRequest req);
    } 
    
}
