namespace Users_api.Service
{
    /*
     * T:  Get DTO Class
     * TI: Insert DTO Class
     * TU: Update DTO Class
     */
    public interface ICRUD<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Create(TI insertDTO);
        Task<T> Update(int id, TU updateDTO);
        Task<T> Delete(int id);

    }
}
