using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using WebShop.Areas.Identity.Data;
using WebShop.Models;

namespace WebShop.Data;

public class WebShopContext : IdentityDbContext<WebShopUser>
{
	public DbSet<Category> categorys { set; get; }

	public WebShopContext(DbContextOptions<WebShopContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
        // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
        // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }


        // seeding data
        string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
        string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

		//seed admin role
		builder.Entity<IdentityRole>().HasData(new IdentityRole
		{
			Name = "Admin",
			NormalizedName ="SUPERADMIN",
			Id = ROLE_ID,
			ConcurrencyStamp = ROLE_ID
		});

		//create user
		var appUser = new WebShopUser
		{
			Id = ADMIN_ID,
			Email = "vantungitss@gmail.com",
			FullName = "Supper Admin",
			EmailConfirmed = true,
			UserName = "vantungitss@gmail.com",
			NormalizedUserName = "vantungitss@gmail.com"
		};

		//set user password
		PasswordHasher<WebShopUser> ph = new PasswordHasher<WebShopUser>();
		appUser.PasswordHash = ph.HashPassword(appUser, "Ab@123456");

		//seed user
		builder.Entity<WebShopUser>().HasData(appUser);

		//set user role to admin
		builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
		{
			RoleId = ROLE_ID,
			UserId = ADMIN_ID
		});

	}
}
