namespace Server.Interfaces
{
    public interface IService
    {
        public IUnitOfWork UnitOfWork { get; set; }
    }
}
