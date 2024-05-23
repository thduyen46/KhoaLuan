using AutoMapper;
using OfficeOpenXml;
using WebsiteTinhThanFoundation.AutoMapperProfiles.Contacts;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<bool> AcceptContact(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Contact model)
        {
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            _unitOfWork.ContactRepository.Add(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> CountAsync()
            => await _unitOfWork.ContactRepository.CountAsync();

        public async Task<MemoryStream> ExportData()
        {
            await Task.Yield();
            var list = _mapper.Map<List<ContactDTO>>(await GetAllAsync());
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true, OfficeOpenXml.Table.TableStyles.Light16);
                workSheet.Cells.AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        public async Task<ICollection<Contact>> GetAllAsync()
            => await _unitOfWork.ContactRepository.GetAllAsync();

        public async Task<Contact?> GetByIdAsync(Guid Id)
            => await _unitOfWork.ContactRepository.GetAsync(x => x.Id == Id);
    }
}