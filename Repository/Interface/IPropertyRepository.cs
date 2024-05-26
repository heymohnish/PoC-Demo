using PoC_Demo.Model;

namespace PoC_Demo.Repository.Interface
{
    public interface IPropertyRepository
    {
        Task<bool> AddProperty(Property property);
        Task<bool> AddUser(User user);
        Task<List<Property>> GetAllProperties();
        Task<Property> GetPropertyById(int id);
        Task<User> GetUserById(int id);
        Task<List<Favorite>> GetFavoriteByPropertyId(int propertyId);
        Task<bool> AddFavorite(Favorite favorite);
        Task<List<Interest>> GetInterestsByPropertyId(int propertyId);
        Task<bool> AddInterest(Interest intrest);
    }
}
