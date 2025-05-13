using PopcornHub.Domain.Common;

namespace PopcornHub.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public User () {}
    
    public User(string userName, string email, string passwordHash, string passwordSalt, int bcryptCost)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        BcryptCost = bcryptCost;
    }
    
    public ICollection<MovieComment> Comments { get; set; } =  [];
    
    public string UserName { get; }
    
    public string Email { get; }
    
    public string PasswordHash { get; }
    
    public string PasswordSalt { get; }
    
    public int BcryptCost { get;  }
}
