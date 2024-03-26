using examApi.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace examApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<string> RegisterUserAsync(User user);
        Task<string> LoginUserAsync(string username, string password);

        Task<User> GetByUsernameAsync(string username);
    }
    public class UserService : IUserService

    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly string _jwtSecret;

        public UserService(IMongoClient mongoClient, string databaseName, string collectionName, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _usersCollection = database.GetCollection<User>(collectionName);
            _jwtSecret = configuration["Jwt:Secret"];
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _usersCollection.Find(Builders<User>.Filter.Empty).ToListAsync();
        }

        public async Task<string> RegisterUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            var token = GenerateJwtToken(user);
            return token;

        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _usersCollection.Find(u => u.username == username).FirstOrDefaultAsync();
        }

        public async Task<string> LoginUserAsync(string username, string password)
        {
            var user = await _usersCollection.Find(u => u.username == username).FirstOrDefaultAsync();

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                // Authentication successful, generate JWT token
                var token = GenerateJwtToken(user);
                return token;
            }

            return null; // Authentication failed
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("ID", user.ID),
            new Claim(ClaimTypes.Role, user.role),
            new Claim("username", user.username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "http://localhost:50182",
                Issuer = "http://localhost:50182",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
