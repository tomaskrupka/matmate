using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatMate.Api;

public class MatMateDbContext : IdentityDbContext<IdentityUser>
{
    public MatMateDbContext(DbContextOptions<MatMateDbContext> options) : base(options)
    {
    }
}