using PoC_Demo.Model;
using PoC_Demo.Repository.Interface;
using System.Data.SqlClient;
using System.Data;

namespace PoC_Demo.Repository
{
    public class PropertyRepository : Connection, IPropertyRepository
    {
        protected readonly IConfiguration Configuration;

        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> AddProperty(Property property)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("InsertProperty", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@name", property.Name);
                    command.Parameters.AddWithValue("@title", property.Title);
                    command.Parameters.AddWithValue("@description", property.Description);
                    command.Parameters.AddWithValue("@search_code", property.SearchCode);
                    command.Parameters.AddWithValue("@seller_id", property.SellerId);
                    command.Parameters.AddWithValue("@place", property.Place);
                    command.Parameters.AddWithValue("@state", property.State);
                    command.Parameters.AddWithValue("@image", property.Image);
                    command.Parameters.AddWithValue("@country", property.Country);
                    command.Parameters.AddWithValue("@property_type", property.PropertyType);
                    command.Parameters.AddWithValue("@area", property.Area);
                    command.Parameters.AddWithValue("@bedrooms", property.Bedrooms);
                    command.Parameters.AddWithValue("@facilitys", property.Facilities);
                    command.Parameters.AddWithValue("@amount", property.Amount);
                    command.Parameters.AddWithValue("@date_posted", property.DatePosted);
                    command.Parameters.AddWithValue("@created_by", property.CreatedBy);
                    command.Parameters.AddWithValue("@modified_by", property.ModifiedBy);
                    command.Parameters.AddWithValue("@created_date", property.CreatedDate);
                    command.Parameters.AddWithValue("@modified_date", property.ModifiedDate);


                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error adding property: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }

        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand("InsertUser", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@first_name", user.FirstName);
                    command.Parameters.AddWithValue("@last_name", user.LastName);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@phone", user.Phone);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@created_date", user.CreatedDate);
                    command.Parameters.AddWithValue("@modified_date", user.ModifiedDate);

                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error adding user: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public async Task<List<Property>> GetAllProperties()
        {

            try
            {
                List<Property> properties = new List<Property>();
                OpenConnection();

                using (SqlCommand command = new SqlCommand("GetAllProperties", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Property property = new Property
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Title = reader.GetString(reader.GetOrdinal("title")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            SearchCode = reader.GetString(reader.GetOrdinal("search_code")),
                            SellerId = reader.GetInt32(reader.GetOrdinal("seller_id")),
                            Place = reader.GetString(reader.GetOrdinal("place")),
                            State = reader.GetString(reader.GetOrdinal("state")),
                            Image = reader.GetString(reader.GetOrdinal("image")),
                            Country = reader.GetString(reader.GetOrdinal("country")),
                            PropertyType = reader.GetString(reader.GetOrdinal("property_type")),
                            Area = reader.GetDecimal(reader.GetOrdinal("area")),
                            Bedrooms = reader.GetInt32(reader.GetOrdinal("bedrooms")),
                            Facilities = reader.GetString(reader.GetOrdinal("facilitys")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                            DatePosted = reader.GetDateTime(reader.GetOrdinal("date_posted")),
                            CreatedBy = reader.GetInt32(reader.GetOrdinal("created_by")),
                            ModifiedBy = reader.GetInt32(reader.GetOrdinal("modified_by")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                        };

                        properties.Add(property);
                    }

                    return properties;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error getting properties: " + ex.Message);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
        public async Task<Property> GetPropertyById(int id)
        {
            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand("GetPropertyById", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        return new Property
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Title = reader.GetString(reader.GetOrdinal("title")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            SearchCode = reader.GetString(reader.GetOrdinal("search_code")),
                            SellerId = reader.GetInt32(reader.GetOrdinal("seller_id")),
                            Place = reader.GetString(reader.GetOrdinal("place")),
                            State = reader.GetString(reader.GetOrdinal("state")),
                            Image = reader.GetString(reader.GetOrdinal("image")),
                            Country = reader.GetString(reader.GetOrdinal("country")),
                            PropertyType = reader.GetString(reader.GetOrdinal("property_type")),
                            Area = reader.GetDecimal(reader.GetOrdinal("area")),
                            Bedrooms = reader.GetInt32(reader.GetOrdinal("bedrooms")),
                            Facilities = reader.GetString(reader.GetOrdinal("facilitys")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                            DatePosted = reader.GetDateTime(reader.GetOrdinal("date_posted")),
                            CreatedBy = reader.GetInt32(reader.GetOrdinal("created_by")),
                            ModifiedBy = reader.GetInt32(reader.GetOrdinal("modified_by")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                        };
                    }
                    else
                    {
                        return null; // Property not found
                    }
                }

            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error getting property: " + ex.Message);
                return null;
            }
            finally
            {
                //GetUserById
                CloseConnection();
            }
        }
        public async Task<User> GetUserById(int id)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("GetUserById", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        return new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                        };
                    }
                    else
                    {
                        return null; // User not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error fetching user: " + ex.Message);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
        public async Task<bool> AddFavorite(Favorite favorite)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("InsertFavorite", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BuyerId", favorite.BuyerId);
                    command.Parameters.AddWithValue("@SellerId", favorite.SellerId);
                    command.Parameters.AddWithValue("@PropertyId", favorite.PropertyId);

                    await command.ExecuteNonQueryAsync();

                    return true;
                }

            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error adding favorite: " + ex.Message);
                return false;
            }
        }
        public async Task<List<Favorite>> GetFavoriteByPropertyId(int propertyId)
        {
            try
            {
                List<Favorite> favorites = new List<Favorite>();
                OpenConnection();
                using (SqlCommand command = new SqlCommand("GetFavoriteByPropertyId", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PropertyId", propertyId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Favorite favorite = new Favorite
                        {
                            FavoriteId = reader.GetInt32(reader.GetOrdinal("favorite_id")),
                            BuyerId = reader.GetInt32(reader.GetOrdinal("buyer_id")),
                            SellerId = reader.GetInt32(reader.GetOrdinal("seller_id")),
                            PropertyId = reader.GetInt32(reader.GetOrdinal("property_id")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                        };

                        favorites.Add(favorite);
                    }
                }


                return favorites;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error getting favorites by PropertyId: " + ex.Message);
                return null;
            }
        }
        public async Task<List<Interest>> GetInterestsByPropertyId(int propertyId)
        {
            try
            {
                List<Interest> interests = new List<Interest>();

                OpenConnection();
                using (SqlCommand command = new SqlCommand("GetInterestsByPropertyId", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PropertyId", propertyId);

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (reader.Read())
                        {
                            Interest interest = new Interest
                            {
                                InterestId = reader.GetInt32(reader.GetOrdinal("interest_id")),
                                BuyerId = reader.GetInt32(reader.GetOrdinal("buyer_id")),
                                SellerId = reader.GetInt32(reader.GetOrdinal("seller_id")),
                                PropertyId = reader.GetInt32(reader.GetOrdinal("property_id")),
                                InterestDate = reader.GetDateTime(reader.GetOrdinal("interest_date")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                            };

                            interests.Add(interest);
                        }
                    }
                

                return interests;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error getting interests by PropertyId: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> AddInterest(Interest intrest)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("InsertInterest", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BuyerId", intrest.BuyerId);
                        command.Parameters.AddWithValue("@SellerId",intrest.SellerId );
                        command.Parameters.AddWithValue("@PropertyId",intrest.PropertyId );
                        await command.ExecuteNonQueryAsync();

                        return true;
                    }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error adding interest: " + ex.Message);
                return false;
            }
        }


    }

}

