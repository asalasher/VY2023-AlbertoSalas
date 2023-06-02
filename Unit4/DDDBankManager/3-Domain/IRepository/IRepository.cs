namespace DDDBankManager._4_IntrastructureData
{
    public interface IRepository<T> where T : class
    {
        T GetById(int accountNumber);
        bool Set(T entity);
    }
}