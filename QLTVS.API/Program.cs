using Microsoft.EntityFrameworkCore;
using QLTVS.BUS;          // Gọi project BUS
using QLTVS.DAO;          // Gọi project DAO
using QLTVS.DAO.Models;   // Gọi folder Models chứa LibraryContext (nếu bạn scaffold vào đây)
// Lưu ý: Nếu LibraryContext của bạn nằm bên project DAO, hãy đổi thành: using QLTVS.DAO.Models;

namespace QLTVS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==================================================================
            // 1. CẤU HÌNH KẾT NỐI DATABASE (POSTGRESQL)
            // ==================================================================
            // Lấy chuỗi kết nối từ file appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("LibraryDbConnection");

            // Đăng ký DbContext
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseNpgsql(connectionString));

            // ==================================================================
            // 2. ĐĂNG KÝ DEPENDENCY INJECTION (DI) CHO BUS VÀ DAO
            // ==================================================================
            // Nguyên tắc: Interface/Class nào được dùng trong Constructor thì phải đăng ký ở đây
            builder.Services.AddScoped<MemberDAO>(); // Đăng ký DAO
            builder.Services.AddScoped<MemberBUS>(); // Đăng ký BUS

            // ==================================================================
            // 3. CÁC CẤU HÌNH CƠ BẢN KHÁC
            // ==================================================================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}