using Microsoft.EntityFrameworkCore;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Dictionary<int, int>> GetRegistrationCountByMonthAsync()
        {
            var registrationsByMonth = await _dbContext.Contacts
            .Where(c => c.CreatedOn.Year == DateTime.UtcNow.Year) // Lọc theo năm hiện tại
            .GroupBy(c => c.CreatedOn.Month) // Nhóm theo tháng
            .Select(g => new { Month = g.Key, Count = g.Count() }) // Chọn số lượng đăng ký trong mỗi tháng
            .ToDictionaryAsync(x => x.Month, x => x.Count); // Chuyển kết quả sang Dictionary

            // Tạo một Dictionary để chứa số lượng đăng ký cho mỗi tháng từ 1 đến 12
            var registrationCountByMonth = new Dictionary<int, int>();
            for (int i = 1; i <= 12; i++)
            {
                // Nếu không có dữ liệu cho tháng đó, đặt số lượng đăng ký là 0
                if (!registrationsByMonth.ContainsKey(i))
                {
                    registrationCountByMonth.Add(i, 0);
                }
                else
                {
                    registrationCountByMonth.Add(i, registrationsByMonth[i]);
                }
            }

            return registrationCountByMonth;
        }
    }
}
